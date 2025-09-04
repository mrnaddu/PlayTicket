using Microsoft.Extensions.DependencyInjection;
using PlayTicket.UserService.EntityFrameworkCore.DbCompliance;
using PlayTicket.UserService.EntityFrameworkCore.DbOffice;
using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService;

[DependsOn(
    typeof(UserServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpDapperModule)
)]
public class UserServiceInfrastructureModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });

        context.Services.AddAbpDbContext<DbOfficeDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });

        context.Services.AddAbpDbContext<DbComplainceDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}