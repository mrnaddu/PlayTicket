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

public class PlayTicketDbMigrationService : ITransientDependency
{
    private readonly ILogger<PlayTicketDbMigrationService> _logger;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public PlayTicketDbMigrationService(
        ILogger<PlayTicketDbMigrationService> logger,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _logger = logger;
        _unitOfWorkManager = unitOfWorkManager;
    }

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
            await MigrateDatabaseAsync<CashVoucherService.EntityFrameworkCore.DbCompliance.DbComplainceDbContext>(cancellationToken);
            await MigrateDatabaseAsync<UserService.EntityFrameworkCore.DbCompliance.DbComplainceDbContext>(cancellationToken);
            await MigrateDatabaseAsync<CashVoucherService.EntityFrameworkCore.DbOffice.DbOfficeDbContext>(cancellationToken);
            await MigrateDatabaseAsync<UserService.EntityFrameworkCore.DbOffice.DbOfficeDbContext>(cancellationToken);
            await uow.CompleteAsync(cancellationToken);
        }

        _logger.LogInformation("All databases have been successfully migrated.");
    }

    private async Task MigrateDatabaseAsync<TDbContext>(CancellationToken cancellationToken)
        where TDbContext : DbContext, IEfCoreDbContext
    {
        var contextName = typeof(TDbContext).Name.RemovePostFix("DbContext");
        _logger.LogInformation($"Migrating {contextName} database...");

        var dbContext = await _unitOfWorkManager.Current.ServiceProvider
            .GetRequiredService<IDbContextProvider<TDbContext>>()
            .GetDbContextAsync();

        await dbContext.Database.MigrateAsync(cancellationToken);

        _logger.LogInformation($"{contextName} database migration completed successfully.");
    }
}
