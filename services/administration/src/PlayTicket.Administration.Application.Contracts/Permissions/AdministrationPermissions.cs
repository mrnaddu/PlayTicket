using Volo.Abp.Reflection;

namespace PlayTicket.Administration.Permissions;

public class AdministrationPermissions
{
    public const string GroupName = "Administration";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AdministrationPermissions));
    }
}