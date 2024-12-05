using PlayTicket.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PlayTicket.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}