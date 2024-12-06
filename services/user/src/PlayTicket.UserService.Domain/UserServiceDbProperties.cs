﻿namespace PlayTicket.UserService;

public static class UserServiceDbProperties
{
    public const string DbOfficeConnectionStringName = "DbOffice";
    public static string DbTablePrefix { get; set; } = "UserService";

    public static string DbSchema { get; set; } = null;
}