using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;

namespace PlayTicket.DbMigrator;

public class PlayTicketDbMigrationService(
    ILogger<PlayTicketDbMigrationService> logger, IUnitOfWorkManager unitOfWorkManager)
    : ITransientDependency
{
    private readonly ILogger<PlayTicketDbMigrationService> _logger = logger;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await MigrateDatabasesAsync(cancellationToken);
        _logger.LogInformation("Migration completed!");
    }

    private async Task MigrateDatabasesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Migrating databases...");
        using (var uow = _unitOfWorkManager.Begin(true))
        {
            await MigrateDatabaseAsync<UserService.EntityFrameworkCore.DbCompliance.DbComplainceDbContext>(cancellationToken);
            await MigrateDatabaseAsync<UserService.EntityFrameworkCore.DbOffice.DbOfficeDbContext>(cancellationToken);
            await uow.CompleteAsync(cancellationToken);
        }

        _logger.LogInformation("All databases have been successfully migrated.");
    }

    private async Task MigrateDatabaseAsync<TDbContext>(CancellationToken cancellationToken)
        where TDbContext : DbContext, IEfCoreDbContext
    {
        var contextName = typeof(TDbContext).Name.RemovePostFix("DbContext");
        _logger.LogInformation("Migrating {contextName} database...", contextName);

        var dbContext = await _unitOfWorkManager.Current.ServiceProvider
            .GetRequiredService<IDbContextProvider<TDbContext>>()
            .GetDbContextAsync();

        await dbContext.Database.MigrateAsync(cancellationToken);

        _logger.LogInformation("{contextName} database migration completed successfully.", contextName);
    }
}
