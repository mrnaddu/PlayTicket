﻿namespace PlayTicket.CashVoucherService;

public static class CashVoucherServiceDbProperties
{
    public const string DbOfficeConnectionStringName = "DbOffice";
    public static string DbTablePrefix { get; set; } = "CashVoucherService";

    public static string DbSchema { get; set; } = null;
}