﻿using PlayTicket.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PlayTicket.Administration;

public abstract class AdministrationController : AbpControllerBase
{
    protected AdministrationController()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}