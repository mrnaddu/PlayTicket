using Volo.Abp.Modularity;

namespace PlayTicket.Projects;

[DependsOn(
    typeof(ProjectsApplicationModule),
    typeof(ProjectsDomainTestModule)
    )]
public class ProjectsApplicationTestModule : AbpModule
{

}
