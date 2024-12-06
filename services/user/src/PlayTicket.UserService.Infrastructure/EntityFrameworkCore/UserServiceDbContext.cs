using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore;

[ConnectionStringName(UserServiceDbProperties.DbOfficeConnectionStringName)]
public class UserServiceDbContext : AbpDbContext<UserServiceDbContext>,
    IUserServiceDbContext,
    IAuditLoggingDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
        : base(options)
    {
    }

    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUserService();
        builder.ConfigureAuditLogging();
    }
}