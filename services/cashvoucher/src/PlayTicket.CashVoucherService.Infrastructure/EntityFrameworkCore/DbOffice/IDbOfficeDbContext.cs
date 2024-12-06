using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore.DbOffice;

[ConnectionStringName(CashVoucherServiceDbProperties.DbOfficeConnectionStringName)]
public interface IDbOfficeDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}