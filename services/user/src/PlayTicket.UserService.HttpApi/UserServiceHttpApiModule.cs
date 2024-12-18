﻿using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using PlayTicket.UserService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
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

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<UserServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}