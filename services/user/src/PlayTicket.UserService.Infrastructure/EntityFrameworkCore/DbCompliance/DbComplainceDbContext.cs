using Microsoft.EntityFrameworkCore;
using PlayTicket.UserService.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore.DbCompliance;

[ConnectionStringName(UserServiceDbProperties.DbComplianceConnectionStringName)]
public class DbComplainceDbContext : AbpDbContext<DbComplainceDbContext>,
    IDbComplainceDbContext
{
    public DbComplainceDbContext(DbContextOptions<DbComplainceDbContext> options)
        : base(options)
    {

    }
    public DbSet<User> Users => throw new System.NotImplementedException();

    public DbSet<UserGroup> UserGroups => throw new System.NotImplementedException();

    public DbSet<UserGroupAccess> UserGroupAccesses => throw new System.NotImplementedException();

    public DbSet<UserUserGroupMap> UserUserGroupMaps => throw new System.NotImplementedException();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDbComplaince();
    }
}
