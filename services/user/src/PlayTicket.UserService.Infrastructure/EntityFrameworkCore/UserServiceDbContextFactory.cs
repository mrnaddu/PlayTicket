using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PlayTicket.UserService.EntityFrameworkCore;

public class UserServiceDbContextFactory : IDesignTimeDbContextFactory<UserServiceDbContext>
{
    public UserServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseMySql(GetConnectionStringFromConfiguration(), MySqlServerVersion.LatestSupportedServerVersion);

        return new UserServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(UserServiceDbProperties.ConnectionStringName);
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
