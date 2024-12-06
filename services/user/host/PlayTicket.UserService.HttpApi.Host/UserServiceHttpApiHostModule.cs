using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using OpenIddict.Server.AspNetCore;
using PlayTicket.Hosting.Shared;
using PlayTicket.UserService.EntityFrameworkCore;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService;

[DependsOn(
    typeof(PlayTicketHostingModule),
    typeof(UserServiceApplicationModule),
    typeof(UserServiceEntityFrameworkCoreModule),
    typeof(UserServiceHttpApiModule)
)]
public class UserServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        if (!configuration.GetValue<bool>("App:DisablePII"))
        {
            IdentityModelEventSource.ShowPII = true;
            IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        if (!configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata"))
        {
            Configure<OpenIddictServerAspNetCoreOptions>(options =>
            {
                options.DisableTransportSecurityRequirement = true;
            });

            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
        }

        ConfigureSwagger(context, configuration);
        ConfigureAuthentication(context, configuration);
        ConfigureDistributedCache();
        ConfigureDataProtection(context, hostingEnvironment, configuration);
        ConfigureCors(context, configuration);
    }

    private static void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string> {
                {"UserService", "UserService API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
    }

    private static void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "UserService";
            });
    }

    private void ConfigureDistributedCache()
    {
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "UserService:";
        });
    }

    private static void ConfigureDataProtection(ServiceConfigurationContext context, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
    {
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("UserService");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "UserService-Protection-Keys");
        }
    }

    private static void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        IdentityModelEventSource.ShowPII = true;
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseAbpSecurityHeaders();
        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        ConfigureSwaggerUI(app, context);
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }

    private static void ConfigureSwaggerUI(IApplicationBuilder app, ApplicationInitializationContext context)
    {
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
        });
    }
}