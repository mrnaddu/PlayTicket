using PlayTicket.CashVoucherService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PlayTicket.CashVoucherService.Permissions;

public class CashVoucherServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CashVoucherServicePermissions.GroupName, L("Permission:CashVoucherService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CashVoucherServiceResource>(name);
    }
}