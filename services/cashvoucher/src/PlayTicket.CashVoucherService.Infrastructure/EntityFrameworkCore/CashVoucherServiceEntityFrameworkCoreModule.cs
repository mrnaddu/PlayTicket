using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore;

[DependsOn(
    typeof(CashVoucherServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpDapperModule)
)]
public class CashVoucherServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });

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