using PlayTicket.Projects.Localization;
using Volo.Abp.Application.Services;

namespace PlayTicket.Projects;

public abstract class ProjectsAppService : ApplicationService
{
    protected ProjectsAppService()
    {
        LocalizationResource = typeof(ProjectsResource);
        ObjectMapperContext = typeof(ProjectsApplicationModule);
    }
}