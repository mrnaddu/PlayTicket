using System;
using Volo.Abp.Domain.Entities;

namespace PlayTicket.UserService.Users;

public class UserUserGroupMap : Entity<int>
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}
