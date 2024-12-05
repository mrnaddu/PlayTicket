using PlayTicket.SaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PlayTicket.SaaS;

public abstract class SaaSController : AbpControllerBase
{
    protected SaaSController()
    {
        LocalizationResource = typeof(SaaSResource);
    }
}