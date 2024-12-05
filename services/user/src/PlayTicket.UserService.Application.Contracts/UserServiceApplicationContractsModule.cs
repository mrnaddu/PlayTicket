using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService;

[DependsOn(
    typeof(UserServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class UserServiceApplicationContractsModule : AbpModule
{
}