using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PlayTicket.UserService.Samples;

public class SampleAppService : UserServiceAppService, ISampleAppService
{
    public Task<SampleDto> GetAsync()
    {
        return Task.FromResult(
            new SampleDto {
                Value = 42
            }
        );
    }

    [Authorize]
    public Task<SampleDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new SampleDto {
                Value = 42
            }
        );
    }
}