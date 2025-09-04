using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace PlayTicket.UserService.EntityFrameworkCore;

public static class UserServiceDbContextModelCreatingExtensions
{
    public static void ConfigureDbComplaince(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }

    public static void ConfigureDbOffice(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}