using Microsoft.EntityFrameworkCore;
using Npgsql;
using PlayTicket.UserService.EntityFrameworkCore.DbCompliance;
using PlayTicket.UserService.EntityFrameworkCore.DbOffice;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService.EntityFrameworkCore;

[DependsOn(
    typeof(UserServiceTestBaseModule),
    typeof(UserServiceInfrastructureModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
    )]
public class UserServiceEntityFrameworkCoreTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var pgConnection = CreateDatabaseAndGetConnection();

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(abpDbContextConfigurationContext =>
            {
                abpDbContextConfigurationContext.DbContextOptions.UseNpgsql(pgConnection);
            });
        });
    }

    private static NpgsqlConnection CreateDatabaseAndGetConnection()
    {
        var connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=yourpassword;Database=test_db");
        connection.Open();

        var officeOptions = new DbContextOptionsBuilder<DbOfficeDbContext>()
            .UseNpgsql(connection)
            .Options;

        var complianceOptions = new DbContextOptionsBuilder<DbComplainceDbContext>()
            .UseNpgsql(connection)
            .Options;

        using (var context = new DbOfficeDbContext(officeOptions))
        {
            context.Database.EnsureCreated();
        }

        using (var context = new DbComplainceDbContext(complianceOptions))
        {
            context.Database.EnsureCreated();
        }

        return connection;
    }
}
