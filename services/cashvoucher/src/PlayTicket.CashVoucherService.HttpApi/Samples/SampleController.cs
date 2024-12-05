using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace PlayTicket.CashVoucherService.Samples;

[Area(CashVoucherServiceRemoteServiceConsts.ModuleName)]
[RemoteService(Name = CashVoucherServiceRemoteServiceConsts.RemoteServiceName)]
[Route("api/CashVoucherService/sample")]
public class SampleController : CashVoucherServiceController, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public SampleController(ISampleAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}