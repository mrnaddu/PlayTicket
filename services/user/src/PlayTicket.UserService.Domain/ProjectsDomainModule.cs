using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(UserServiceDomainSharedModule)
)]
public class ProjectsDomainModule : AbpModule
{
}