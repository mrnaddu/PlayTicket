using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PlayTicket.Projects.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}