using Volo.Abp.Application.Services;

namespace PlayTicket.UserService;

public abstract class UserServiceAppService : ApplicationService
{
    protected UserServiceAppService()
    {
        ObjectMapperContext = typeof(UserServiceApplicationModule);
    }
}