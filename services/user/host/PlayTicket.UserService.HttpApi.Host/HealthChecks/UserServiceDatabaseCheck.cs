using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Volo.Abp.DependencyInjection;

namespace PlayTicket.UserService.HealthChecks;

public class UserServiceDatabaseCheck : IHealthCheck, ITransientDependency
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // sample code to check database connection
        throw new NotImplementedException();
    }
}
