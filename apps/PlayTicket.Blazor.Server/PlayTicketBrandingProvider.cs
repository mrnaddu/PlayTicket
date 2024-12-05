using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace PlayTicket.Blazor.Server;

[Dependency(ReplaceServices = true)]
public class PlayTicketBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "PlayTicket";
}
