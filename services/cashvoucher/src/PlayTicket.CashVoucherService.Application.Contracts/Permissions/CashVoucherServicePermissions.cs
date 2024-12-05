using Volo.Abp.Reflection;

namespace PlayTicket.CashVoucherService.Permissions;

public class CashVoucherServicePermissions
{
    public const string GroupName = "CashVoucherService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CashVoucherServicePermissions));
    }
}