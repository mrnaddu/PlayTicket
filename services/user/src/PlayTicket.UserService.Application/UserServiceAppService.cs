using PlayTicket.UserService.Localization;
using Volo.Abp.Application.Services;

namespace PlayTicket.UserService;

public abstract class UserServiceAppService : ApplicationService
{
    protected UserServiceAppService()
    {
        LocalizationResource = typeof(UserServiceResource);
        ObjectMapperContext = typeof(UserServiceApplicationModule);
    }
}