using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace PlayTicket.CashVoucherService;

[DependsOn(
    typeof(CashVoucherServiceApplicationContractsModule),
    typeof(AbpHttpClientModule)
)]
public class CashVoucherServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CashVoucherServiceApplicationContractsModule).Assembly,
            CashVoucherServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CashVoucherServiceHttpApiClientModule>();
        });
    }
}