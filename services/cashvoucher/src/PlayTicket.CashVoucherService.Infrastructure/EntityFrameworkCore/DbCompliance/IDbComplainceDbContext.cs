﻿using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore.DbCompliance;

[ConnectionStringName(CashVoucherServiceDbProperties.DbComplianceConnectionStringName)]
public interface IDbComplainceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
