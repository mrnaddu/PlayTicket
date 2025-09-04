using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore.DbOffice;

[ConnectionStringName(UserServiceDbProperties.DbOfficeConnectionStringName)]
public class DbOfficeDbContext(DbContextOptions<DbOfficeDbContext> options) : AbpDbContext<DbOfficeDbContext>(options),
    IDbOfficeDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDbOffice();
    }
}