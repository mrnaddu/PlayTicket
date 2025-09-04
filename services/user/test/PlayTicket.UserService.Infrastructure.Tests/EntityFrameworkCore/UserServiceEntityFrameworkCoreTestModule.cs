using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using PlayTicket.UserService.EntityFrameworkCore.DbCompliance;
using PlayTicket.UserService.EntityFrameworkCore.DbOffice;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService.EntityFrameworkCore;

[DependsOn(
    typeof(UserServiceTestBaseModule),
    typeof(UserServiceInfrastructureModule),
    typeof(AbpEntityFrameworkCoreSqliteModule)
    )]
public class UserServiceEntityFrameworkCoreTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(abpDbContextConfigurationContext =>
            {
                abpDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
            });
        });
    }

    private static MySqlConnection CreateDatabaseAndGetConnection()
    {
        var connection = new MySqlConnection("Data Source=:memory:");
        connection.Open();

        new DbOfficeDbContext(
            new DbContextOptionsBuilder<DbOfficeDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        new DbComplainceDbContext(
            new DbContextOptionsBuilder<DbComplainceDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}
