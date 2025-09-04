var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PlayTicket_UserService_HttpApi_Host>("playticket-userservice-httpapi-host", launchProfileName: "PlayTicket.UserService.Host");

builder.Build().Run();