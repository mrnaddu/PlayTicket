using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CashVoucherServiceDomainSharedModule)
)]
public class CashVoucherServiceDomainModule : AbpModule
{
}