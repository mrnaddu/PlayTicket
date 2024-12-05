using PlayTicket.CashVoucherService.Localization;
using Volo.Abp.Application.Services;

namespace PlayTicket.CashVoucherService;

public abstract class CashVoucherServiceAppService : ApplicationService
{
    protected CashVoucherServiceAppService()
    {
        LocalizationResource = typeof(CashVoucherServiceResource);
        ObjectMapperContext = typeof(CashVoucherServiceApplicationModule);
    }
}