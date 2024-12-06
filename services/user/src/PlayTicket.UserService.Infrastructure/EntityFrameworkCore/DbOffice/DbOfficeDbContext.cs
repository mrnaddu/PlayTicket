using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore.DbOffice;

[ConnectionStringName(UserServiceDbProperties.DbOfficeConnectionStringName)]
public class DbOfficeDbContext : AbpDbContext<DbOfficeDbContext>,
    IDbOfficeDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbOfficeDbContext(DbContextOptions<DbOfficeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUserService();
    }
}