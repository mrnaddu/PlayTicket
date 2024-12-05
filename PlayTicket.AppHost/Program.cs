var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<UserService.PlayTicket_CashVoucherService_HttpApi_Host>("playticket-cashvoucher-httpapi-host", launchProfileName: "PlayTicket.CashVoucherService.Host");

builder.AddProject<UserService.PlayTicket_UserService_HttpApi_Host>("playticket-userservice-httpapi-host", launchProfileName: "PlayTicket.UserService.Host");

builder.Build().Run();