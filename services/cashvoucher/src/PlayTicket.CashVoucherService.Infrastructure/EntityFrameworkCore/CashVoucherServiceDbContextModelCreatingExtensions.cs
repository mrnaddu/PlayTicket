using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace PlayTicket.CashVoucherService.EntityFrameworkCore;

public static class CashVoucherServiceDbContextModelCreatingExtensions
{
    public static void ConfigureDbComplaince(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }

    public static void ConfigureDbOffice(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}