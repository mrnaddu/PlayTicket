using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService;

[DependsOn(
    typeof(CashVoucherServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class CashVoucherServiceApplicationContractsModule : AbpModule
{
}