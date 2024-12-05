using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CashVoucherServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CashVoucherServiceConsoleApiClientModule : AbpModule
{

}
