using PlayTicket.SaaS.Localization;
using Volo.Abp.Application.Services;

namespace PlayTicket.SaaS;

public abstract class SaaSAppService : ApplicationService
{
    protected SaaSAppService()
    {
        LocalizationResource = typeof(SaaSResource);
        ObjectMapperContext = typeof(SaaSApplicationModule);
    }
}