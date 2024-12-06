using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore.DbCompliance;

[ConnectionStringName(CashVoucherServiceDbProperties.DbComplianceConnectionStringName)]
public class DbComplainceDbContext : AbpDbContext<DbComplainceDbContext>,
    IDbComplainceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbComplainceDbContext(DbContextOptions<DbComplainceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAdministration();
    }
}
