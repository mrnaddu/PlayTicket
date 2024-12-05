using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace PlayTicket;

[Dependency(ReplaceServices = true)]
public class PlayTicketBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "PlayTicket";
}
