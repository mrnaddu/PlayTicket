using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PlayTicket.UserService.EntityFrameworkCore.DbOffice;

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
            .GetConnectionString(UserServiceDbProperties.DbOfficeConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory())?.Parent!.FullName!,
                    $"host{Path.DirectorySeparatorChar}PlayTicket.UserService.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}