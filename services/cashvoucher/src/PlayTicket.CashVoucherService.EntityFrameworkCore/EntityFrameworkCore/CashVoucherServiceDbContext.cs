using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore;

[ConnectionStringName(CashVoucherServiceDbProperties.ConnectionStringName)]
public class CashVoucherServiceDbContext : AbpDbContext<CashVoucherServiceDbContext>
    ICashVoucherServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public CashVoucherServiceDbContext(DbContextOptions<CashVoucherServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAdministration();
    }
}