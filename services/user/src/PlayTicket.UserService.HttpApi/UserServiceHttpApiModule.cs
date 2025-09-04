using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService;

[DependsOn(
    typeof(UserServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class UserServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(UserServiceHttpApiModule).Assembly);
        });
    }
}