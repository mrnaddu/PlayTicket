using PlayTicket.Administration.Localization;
using Volo.Abp.Application.Services;

namespace PlayTicket.Administration;

public abstract class AdministrationAppService : ApplicationService
{
    protected AdministrationAppService()
    {
        LocalizationResource = typeof(AdministrationResource);
        ObjectMapperContext = typeof(AdministrationApplicationModule);
    }
}