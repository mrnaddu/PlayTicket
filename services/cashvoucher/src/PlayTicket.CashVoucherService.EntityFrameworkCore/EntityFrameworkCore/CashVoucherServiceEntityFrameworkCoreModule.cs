using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore;

[DependsOn(
    typeof(CashVoucherServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class CashVoucherServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        context.Services.AddAbpDbContext<CashVoucherServiceDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */

            options.AddDefaultRepositories(true);
        });

        context.Services.AddAbpDbContext<CashVoucherServiceDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}