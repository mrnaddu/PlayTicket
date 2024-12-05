using PlayTicket.Projects.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PlayTicket.Projects;

public abstract class ProjectsController : AbpControllerBase
{
    protected ProjectsController()
    {
        LocalizationResource = typeof(ProjectsResource);
    }
}