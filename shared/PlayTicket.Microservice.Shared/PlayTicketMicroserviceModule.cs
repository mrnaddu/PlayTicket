using PlayTicket.CashVoucherService.EntityFrameworkCore;
using PlayTicket.Hosting.Shared;
using Volo.Abp.Modularity;

namespace PlayTicket.Microservice.Shared;

[DependsOn(
    typeof(PlayTicketHostingModule),
    typeof(AdministrationEntityFrameworkCoreModule)
)]
public class PlayTicketMicroserviceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}