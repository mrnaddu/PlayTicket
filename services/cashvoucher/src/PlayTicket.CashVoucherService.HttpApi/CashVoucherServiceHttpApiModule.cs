using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using PlayTicket.CashVoucherService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService;

[DependsOn(
    typeof(CashVoucherServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CashVoucherServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CashVoucherServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CashVoucherServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}