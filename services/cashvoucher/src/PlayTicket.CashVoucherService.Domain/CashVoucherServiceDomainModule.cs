using Volo.Abp.AuditLogging;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CashVoucherServiceDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule)
)]
public class CashVoucherServiceDomainModule : AbpModule
{
}