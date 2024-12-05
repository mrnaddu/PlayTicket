using Volo.Abp.Reflection;

namespace PlayTicket.UserService.Permissions;

public class UserServicePermissions
{
    public const string GroupName = "UserService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(UserServicePermissions));
    }
}