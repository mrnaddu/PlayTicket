using PlayTicket.UserService;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace PlayTicket.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(UserServiceInfrastructureModule),
    typeof(UserServiceApplicationContractsModule)
)]
public class PlayTicketDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }
}