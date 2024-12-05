using PlayTicket.CashVoucherService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PlayTicket.CashVoucherService;

public abstract class CashVoucherServiceController : AbpControllerBase
{
    protected CashVoucherServiceController()
    {
        LocalizationResource = typeof(CashVoucherServiceResource);
    }
}