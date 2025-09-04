using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace PlayTicket.UserService;

public class UserServiceDataSeedContributor(ICurrentTenant currentTenant)
    : IDataSeedContributor, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant = currentTenant;

    public Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            return Task.CompletedTask;
        }
    }
}
