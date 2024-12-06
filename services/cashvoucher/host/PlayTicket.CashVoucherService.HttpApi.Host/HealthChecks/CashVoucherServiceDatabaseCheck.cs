using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;
using System;
using Volo.Abp.DependencyInjection;

namespace PlayTicket.CashVoucherService.HealthChecks;

public class CashVoucherServiceDatabaseCheck : IHealthCheck, ITransientDependency
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // sample code to check database connection
        throw new NotImplementedException();
    }
}
