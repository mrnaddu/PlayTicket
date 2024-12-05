using Volo.Abp.Modularity;

namespace PlayTicket.IdentityService;

[DependsOn(
    typeof(IdentityServiceApplicationModule),
    typeof(IdentityServiceDomainTestModule)
    )]
public class IdentityServiceApplicationTestModule : AbpModule
{

}
