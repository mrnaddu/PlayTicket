using PlayTicket.UserService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PlayTicket.UserService;

public abstract class UserServiceController : AbpControllerBase
{
    protected UserServiceController()
    {
        LocalizationResource = typeof(UserServiceResource);
    }
}