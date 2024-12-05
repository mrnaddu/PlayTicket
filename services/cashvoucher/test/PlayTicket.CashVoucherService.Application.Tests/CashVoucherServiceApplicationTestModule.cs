using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService;

[DependsOn(
    typeof(CashVoucherServiceApplicationModule),
    typeof(CashVoucherServiceDomainTestModule)
    )]
public class CashVoucherServiceApplicationTestModule : AbpModule
{

}
