using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlayTicket.CashVoucherService.EntityFrameworkCore;
using PlayTicket.UserService.EntityFrameworkCore;
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
            await MigrateDatabaseAsync<UserServiceDbContext>(cancellationToken);
            await MigrateDatabaseAsync<CashVoucherServiceDbContext>(cancellationToken);
            await uow.CompleteAsync(cancellationToken);
        }

        _logger.LogInformation("All databases have been successfully migrated.");
    }

    private async Task MigrateDatabaseAsync<TDbContext>(CancellationToken cancellationToken)
        where TDbContext : DbContext, IEfCoreDbContext
    {
        _logger.LogInformation($"Migrating {typeof(TDbContext).Name.RemovePostFix("DbContext")} database...");

        var dbContext = await _unitOfWorkManager.Current.ServiceProvider
            .GetRequiredService<IDbContextProvider<TDbContext>>()
            .GetDbContextAsync();

        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
