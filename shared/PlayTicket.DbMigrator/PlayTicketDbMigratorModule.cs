using PlayTicket.CashVoucherService;
using PlayTicket.CashVoucherService.EntityFrameworkCore;
using PlayTicket.UserService;
using PlayTicket.UserService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace PlayTicket.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CashVoucherServiceEntityFrameworkCoreModule),
    typeof(CashVoucherServiceApplicationContractsModule),
    typeof(UserServiceEntityFrameworkCoreModule),
    typeof(UserServiceApplicationContractsModule)
)]
public class PlayTicketDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}