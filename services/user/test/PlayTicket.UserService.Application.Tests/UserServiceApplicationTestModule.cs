using Volo.Abp.Modularity;

namespace PlayTicket.UserService;

[DependsOn(
    typeof(UserServiceApplicationModule),
    typeof(UserServiceDomainTestModule)
    )]
public class UserServiceApplicationTestModule : AbpModule
{

}
