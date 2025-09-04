using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PlayTicket.UserService.EntityFrameworkCore.DbOffice;

[ConnectionStringName(UserServiceDbProperties.DbOfficeConnectionStringName)]
public interface IDbOfficeDbContext : IEfCoreDbContext
{

}