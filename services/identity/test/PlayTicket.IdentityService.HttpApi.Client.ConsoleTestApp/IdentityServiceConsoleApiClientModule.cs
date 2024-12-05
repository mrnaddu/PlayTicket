using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace PlayTicket.IdentityService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(IdentityServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class IdentityServiceConsoleApiClientModule : AbpModule
{

}
