using System;
using Volo.Abp.Domain.Entities;

namespace PlayTicket.UserService.Users;

public class UserGroup : Entity<int>
{
    public Guid GroupId { get; set; }
    public Guid PartnerId { get; set; }
    public Guid ApplicationId { get; set; }
    public string Description { get; set; }

    protected UserGroup()
    {

    }

    public UserGroup(
        int id,Guid groupId, Guid partnerId, Guid applicationId, string description)
        : base(id)
    {
        GroupId = groupId;
        PartnerId = partnerId;
        ApplicationId = applicationId;
        Description = description;
    }
}
