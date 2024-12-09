using System;
using Volo.Abp.Domain.Entities;

namespace PlayTicket.UserService.Users;

public class UserGroupAccess : Entity<int>
{
    public Guid GroupId { get; set; }
    public string Assets { get; set; }
}
