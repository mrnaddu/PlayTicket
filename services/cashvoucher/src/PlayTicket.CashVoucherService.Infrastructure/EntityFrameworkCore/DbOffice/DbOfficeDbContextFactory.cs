using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore.DbOffice;

public class DbOfficeDbContextFactory : IDesignTimeDbContextFactory<DbOfficeDbContext>
{
    public DbOfficeDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DbOfficeDbContext>()
            .UseMySql(GetConnectionStringFromConfiguration(), MySqlServerVersion.LatestSupportedServerVersion);

        return new DbOfficeDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(CashVoucherServiceDbProperties.DbOfficeConnectionStringName);
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