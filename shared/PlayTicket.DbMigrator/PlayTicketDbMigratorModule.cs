using PlayTicket.Administration;
using PlayTicket.Administration.EntityFrameworkCore;
using PlayTicket.IdentityService;
using PlayTicket.IdentityService.EntityFrameworkCore;
using PlayTicket.Microservice.Shared;
using PlayTicket.SaaS;
using PlayTicket.SaaS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace PlayTicket.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(AdministrationApplicationContractsModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(SaaSEntityFrameworkCoreModule),
    typeof(SaaSApplicationContractsModule)
)]
public class PlayTicketDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}