using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore.DbCompliance;

[ConnectionStringName(UserServiceDbProperties.DbComplianceConnectionStringName)]
public interface IDbComplainceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
