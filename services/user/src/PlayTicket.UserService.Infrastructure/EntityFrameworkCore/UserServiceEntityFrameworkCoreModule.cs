using Microsoft.Extensions.DependencyInjection;
using PlayTicket.UserService.EntityFrameworkCore.DbCompliance;
using PlayTicket.UserService.EntityFrameworkCore.DbOffice;
using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace PlayTicket.UserService.EntityFrameworkCore;

[DependsOn(
    typeof(UserServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpDapperModule)
)]
public class UserServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });

        // dboffice
        context.Services.AddAbpDbContext<DbOfficeDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */

            options.AddDefaultRepositories(true);
        });

        context.Services.AddAbpDbContext<DbOfficeDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });

        // dbcompliance
        context.Services.AddAbpDbContext<DbComplainceDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */

            options.AddDefaultRepositories(true);
        });

        context.Services.AddAbpDbContext<DbComplainceDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}