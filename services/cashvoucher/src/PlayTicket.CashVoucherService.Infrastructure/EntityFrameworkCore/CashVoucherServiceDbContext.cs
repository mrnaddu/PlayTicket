using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore;

[ConnectionStringName(CashVoucherServiceDbProperties.DbOfficeConnectionStringName)]
public class CashVoucherServiceDbContext : AbpDbContext<CashVoucherServiceDbContext>,
    ICashVoucherServiceDbContext,
    IAuditLoggingDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public CashVoucherServiceDbContext(DbContextOptions<CashVoucherServiceDbContext> options)
        : base(options)
    {
    }

    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAdministration();
        builder.ConfigureAuditLogging();
    }
}