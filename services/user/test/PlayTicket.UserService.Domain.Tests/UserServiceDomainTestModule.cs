using PlayTicket.UserService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(UserServiceEntityFrameworkCoreTestModule)
)]
public class UserServiceDomainTestModule : AbpModule
{

}
