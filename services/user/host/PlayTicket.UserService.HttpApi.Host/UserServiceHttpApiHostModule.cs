using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.CognitoIdentityProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PlayTicket.Hosting.Shared;
using PlayTicket.Hosting.Shared.Options;
using Serilog;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

using HealthStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;

namespace PlayTicket.UserService;

[DependsOn(
    typeof(PlayTicketHostingModule),
    typeof(UserServiceApplicationModule),
    typeof(UserServiceInfrastructureModule),
    typeof(UserServiceHttpApiModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public sealed class UserServiceHttpApiHostModule : AbpModule
{
    private const string FIRSTSYSTEM = "FirstSystem";
    private const string SECONDSYSTEM = "SecondSystem";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        ConfigureOptions(context, configuration);

        using var scope = context.Services.BuildServiceProvider().CreateScope();
        var authOptions = scope.ServiceProvider.GetRequiredService<IOptions<AuthOptions>>().Value;
        var redisOptions = scope.ServiceProvider.GetRequiredService<IOptions<RedisOptions>>().Value;

        if (!bool.TryParse(authOptions.DisablePII, out var disablePii) || !disablePii)
        {
            IdentityModelEventSource.ShowPII = true;
            IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        if (!bool.TryParse(authOptions.RequireHttpsMetadata, out var requireHttps) || !requireHttps)
        {
            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
        }

        ConfigureLogger(context);
        ConfigureSwagger(context);
        ConfigureAuthentication(context, authOptions);
        ConfigureAuthorization(context);
        ConfigureAmazonServices(context);
        ConfigureDistributedCache();
        ConfigureDataProtection(context, redisOptions);
        ConfigureCors(context);
        ConfigureHealthCheck(context);
        ConfigureControllers(context);
    }
    private static void ConfigureOptions(
        ServiceConfigurationContext context, IConfiguration configuration)
    {
        var optionTypesWithSections = new (Type OptionType, string SectionName)[]
        {
            (typeof(AuthOptions), "Auth"),
            (typeof(RedisOptions), "Redis"),
        };

        foreach (var (optionType, sectionName) in optionTypesWithSections)
        {
            var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                .GetMethods()
                .FirstOrDefault(m =>
                    m.Name is "Configure" &&
                    m.IsGenericMethodDefinition &&
                    m.GetParameters().Length is 2 &&
                    m.GetParameters()[1].ParameterType == typeof(IConfiguration));

            if (configureMethod == null)
            {
                continue;
            }
            var genericMethod = configureMethod.MakeGenericMethod(optionType);
            genericMethod.Invoke(null, [context.Services, configuration.GetSection(sectionName)]);
        }
    }

    private static void ConfigureLogger(ServiceConfigurationContext context)
    {
        context.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
        });
    }

    private static void ConfigureAmazonServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<AmazonCognitoIdentityProviderClient>();
    }

    private static void ConfigureSwagger(ServiceConfigurationContext context)
    {
        context.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "UserService API",
                Version = "v1",
                Description = "API for UserService with multiple JWT authentication schemes"
            });

            options.DocInclusionPredicate((_, _) => true);
            options.CustomSchemaIds(type => type.FullName);

            void AddSecurity(string scheme, string description)
            {
                options.AddSecurityDefinition(scheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = description,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
            }

            AddSecurity(FIRSTSYSTEM, "Enter JWT token prefixed with Bearer for FirstSystem authentication");
            AddSecurity(SECONDSYSTEM, "Enter JWT token prefixed with Bearer for SecondSystem authentication");

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = FIRSTSYSTEM
                        }
                    },
                    Array.Empty<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SECONDSYSTEM
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlPath = Path.Combine(
                AppContext.BaseDirectory, "PlayTicket.UserService.HttpApi.xml");
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });
    }

    private static void ConfigureAuthentication(
        ServiceConfigurationContext context, AuthOptions authOptions)
    {
        if (!context.Services.Any(s => s.ServiceType == typeof(AmazonCognitoIdentityProviderClient)))
        {
            context.Services.AddSingleton<AmazonCognitoIdentityProviderClient>();
        }

        context.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = FIRSTSYSTEM;
            options.DefaultChallengeScheme = FIRSTSYSTEM;
        })
        .AddJwtBearer(FIRSTSYSTEM, options =>
        {
            ConfigureToken(
                options, authOptions.TckPoolId, authOptions.Region, authOptions.TckClientId);
        })
        .AddJwtBearer(SECONDSYSTEM, options =>
        {
            ConfigureToken(
                options, authOptions.KskPoolId, authOptions.Region, authOptions.KskClientId);
        });
    }
    private static void ConfigureToken(
        JwtBearerOptions options, string poolId, string region, string clientId)
    {
        ConfigureJwtBearer(options, poolId, region);
        options.TokenValidationParameters.ValidAudience = clientId;
    }

    private static void ConfigureJwtBearer(
        JwtBearerOptions options, string poolId, string region)
    {
        var authority = $"https://cognito-idp.{region}.amazonaws.com/{poolId}";
        options.Authority = authority;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = authority,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };
    }

    private static void ConfigureAuthorization(ServiceConfigurationContext context)
    {
        context.Services.AddAuthorizationBuilder()
            .AddPolicy("RequireFirstSystem", policy =>
            {
                policy.AuthenticationSchemes.Add(FIRSTSYSTEM);
                policy.RequireClaim("SystemName", FIRSTSYSTEM);
            })
            .AddPolicy("RequireSecondSystem", policy =>
            {
                policy.AuthenticationSchemes.Add(SECONDSYSTEM);
                policy.RequireClaim("SystemName", SECONDSYSTEM);
            })
            .AddPolicy("RequireAnySystem", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(ctx =>
                    ctx.User.HasClaim(c =>
                        c.Type is "SystemName" &&
                        (string.Equals(c.Value, FIRSTSYSTEM, StringComparison.Ordinal) ||
                         string.Equals(c.Value, SECONDSYSTEM, StringComparison.Ordinal))
                    )
                );
            });
    }

    private void ConfigureDistributedCache()
    {
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "UserService:";
            options.GlobalCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
        });
    }
    private static void ConfigureControllers(ServiceConfigurationContext context)
    {
        context.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                var serializerOptions = options.JsonSerializerOptions;
                serializerOptions.Converters.Add(new JsonStringEnumConverter());
                serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                serializerOptions.PropertyNameCaseInsensitive = true;
                serializerOptions.WriteIndented = false;
                serializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            });
    }

    private static void ConfigureDataProtection(ServiceConfigurationContext context, RedisOptions redisOptions)
    {
        var builder = context.Services.AddDataProtection().SetApplicationName(FIRSTSYSTEM);

        if (bool.TryParse(redisOptions.IsEnabled, out var enabled) && enabled && !string.IsNullOrWhiteSpace(redisOptions.Configuration))
        {
            var multiplexer = ConnectionMultiplexer.Connect(redisOptions.Configuration);
            context.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            builder.PersistKeysToStackExchangeRedis(multiplexer, "UserService-Protection-Keys");
        }
    }
    private static void ConfigureCors(ServiceConfigurationContext context)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithAbpExposedHeaders()
                    .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
            });
        });
    }

    private static void ConfigureHealthCheck(ServiceConfigurationContext context)
    {
        context.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        IdentityModelEventSource.ShowPII = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        var logger = context.ServiceProvider.GetRequiredService<ILogger<UserServiceHttpApiHostModule>>();

        logger.LogInformation("Starting application initialization.");

        if (env.IsDevelopment())
        {
            logger.LogInformation("Environment is Development. Using DeveloperExceptionPage.");
            app.UseDeveloperExceptionPage();
        }
        else
        {
            logger.LogInformation("Environment is Production. Using HSTS, HTTPS Redirection, and ExceptionHandler.");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseExceptionHandler("/error");
        }

        logger.LogInformation("Configuring Swagger UI.");
        ConfigureSwaggerUI(app);

        logger.LogInformation("Configuring middleware pipeline.");
        app.UseStatusCodePages()
           .UseCorrelationId()
           .UseStaticFiles()
           .UseRouting()
           .Use(async (context, next) =>
           {
               context.Response.Headers.CacheControl = "no-store, no-cache, must-revalidate";
               context.Response.Headers.StrictTransportSecurity = "max-age=31536000; includeSubDomains";
               await next();
           })
           .UseForwardedHeaders()
           .UseAuthentication()
           .UseCors()
           .UseAuthorization()
           .UseUnitOfWork()
           .UseSerilogRequestLogging()
           .UseConfiguredEndpoints();

        logger.LogInformation("Configuring endpoints.");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (httpContext, report) =>
                {
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.Headers.CacheControl = "no-store, no-cache, must-revalidate";
                    httpContext.Response.ContentType = "application/json";
                    var response = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new
                        {
                            name = e.Key,
                            status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                            duration = e.Value.Duration.TotalMilliseconds
                        }),
                        totalDuration = report.TotalDuration.TotalMilliseconds,
                        timestamp = DateTime.UtcNow
                    };
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });
        });
    }

    private static void ConfigureSwaggerUI(IApplicationBuilder app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
            options.SerializeAsV2 = false;
        });

        app.UseSwaggerUI(static options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService API");
            options.RoutePrefix = string.Empty;
            options.DocumentTitle = "UserService API Documentation";
            options.DefaultModelsExpandDepth(2);
            options.DefaultModelRendering(ModelRendering.Model);
            options.DocExpansion(DocExpansion.None);
            options.DisplayRequestDuration();
            options.EnableFilter();
            options.EnableDeepLinking();
            options.EnablePersistAuthorization();
            options.ShowExtensions();
            options.EnableValidator();
        });
    }
}
