using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PlayTicket.UserService.EntityFrameworkCore.DbCompliance;

public class DbComplainceDbContextFactory : IDesignTimeDbContextFactory<DbComplainceDbContext>
{
    public DbComplainceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DbComplainceDbContext>()
            .UseMySql(GetConnectionStringFromConfiguration(), MySqlServerVersion.LatestSupportedServerVersion);

        return new DbComplainceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(UserServiceDbProperties.DbComplianceConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory())?.Parent!.FullName!,
                    $"host{Path.DirectorySeparatorChar}PlayTicket.CashVoucherService.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}
