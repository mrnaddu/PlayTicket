using Microsoft.EntityFrameworkCore;
using PlayTicket.UserService.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore.DbCompliance;

[ConnectionStringName(UserServiceDbProperties.DbComplianceConnectionStringName)]
public interface IDbComplainceDbContext : IEfCoreDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserGroup> UserGroups { get; }
    DbSet<UserGroupAccess> UserGroupAccesses { get; }
    DbSet<UserUserGroupMap> UserUserGroupMaps { get; }
}
