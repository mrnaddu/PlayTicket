using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore.DbCompliance;

[ConnectionStringName(UserServiceDbProperties.DbComplianceConnectionStringName)]
public class DbComplainceDbContext(DbContextOptions<DbComplainceDbContext> options) : AbpDbContext<DbComplainceDbContext>(options),
    IDbComplainceDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDbComplaince();
    }
}
