using Volo.Abp.Modularity;

namespace PlayTicket.Administration;

[DependsOn(
    typeof(AdministrationApplicationModule),
    typeof(CashVoucherServiceDomainTestModule)
    )]
public class CashVoucherServiceApplicationTestModule : AbpModule
{

}
