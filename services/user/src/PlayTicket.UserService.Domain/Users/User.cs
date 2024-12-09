using System;
using Volo.Abp.Domain.Entities;

namespace PlayTicket.UserService.Users;

public class User : Entity<int>
{
    public Guid ReferenceId { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public char Status { get; set; }

    protected User()
    {

    }

    public User(
        int id, Guid referenceId, string userId, string name, char status)
        : base(id)
    {
        ReferenceId = referenceId;
        UserId = userId;
        Name = name;
        CreatedDateTime = DateTime.Now;
        Status = status;
    }
}
