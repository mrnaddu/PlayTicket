using PlayTicket.CashVoucherService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CashVoucherServiceEntityFrameworkCoreTestModule)
    )]
public class CashVoucherServiceDomainTestModule : AbpModule
{

}
