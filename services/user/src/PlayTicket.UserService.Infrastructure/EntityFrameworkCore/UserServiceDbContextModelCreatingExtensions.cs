using Microsoft.EntityFrameworkCore;
using PlayTicket.UserService.Users;
using Volo.Abp;

namespace PlayTicket.UserService.EntityFrameworkCore;

public static class UserServiceDbContextModelCreatingExtensions
{
    public static void ConfigureDbComplaince(
        this ModelBuilder builder)
    {
        // users
        Check.NotNull(builder, nameof(builder));
        builder.Entity<User>(u =>
        {
            u.ToTable(name: "t_user");
            u.Property(user => user.Id)
                .HasColumnName("id");
            u.Property(user => user.ReferenceId)
                .HasColumnName("reference_id");
            u.Property(user => user.UserId)
                .HasColumnName("user_uid");
            u.Property(user => user.Name)
                .HasColumnName("name");
            u.Property(user => user.CreatedDateTime)
                .HasColumnName("created_dttm");
            u.Property(user => user.Status)
                .HasColumnName("status");
        });

        // usergroups
        Check.NotNull(builder, nameof(builder));
        builder.Entity<UserGroup>(u =>
        {
            u.ToTable(name: "t_user_group");
            u.Property(user => user.Id)
                .HasColumnName("id");
            u.Property(user => user.GroupId)
                .HasColumnName("group_uid");
            u.Property(user => user.PartnerId)
                .HasColumnName("partner_uid");
            u.Property(user => user.ApplicationId)
                .HasColumnName("application_uid");
            u.Property(user => user.Description)
                .HasColumnName("description");
        });

        // usergroupaccess
        Check.NotNull(builder, nameof(builder));
        builder.Entity<UserGroupAccess>(u =>
        {
            u.ToTable(name: "t_user_group_access");
            u.Property(user => user.Id)
                .HasColumnName("id");
            u.Property(user => user.GroupId)
                .HasColumnName("group_uid");
            u.Property(user => user.Assets)
                .HasColumnName("asset");
        });
    }

    public static void ConfigureDbOffice(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}