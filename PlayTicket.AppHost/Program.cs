var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PlayTicket_AuthServer>("playticket-authserver", launchProfileName: "PlayTicket.AuthServer");

builder.AddProject<Projects.PlayTicket_Administration_HttpApi_Host>("playticket-administration-httpapi-host", launchProfileName: "PlayTicket.Administration.Host");

builder.AddProject<Projects.PlayTicket_IdentityService_HttpApi_Host>("playticket-identityservice-httpapi-host", launchProfileName: "PlayTicket.IdentityService.Host");

builder.AddProject<Projects.PlayTicket_Projects_HttpApi_Host>("playticket-projects-httpapi-host", launchProfileName: "PlayTicket.Projects.Host");

builder.AddProject<Projects.PlayTicket_SaaS_HttpApi_Host>("playticket-saas-httpapi-host", launchProfileName: "PlayTicket.SaaS.Host");

builder.AddProject<Projects.PlayTicket_Gateway>("playticket-gateway", launchProfileName: "PlayTicket.Gateway");

builder.AddProject<Projects.PlayTicket_Blazor_Server>("playticket-blazor-server", launchProfileName: "PlayTicket.Blazor.Server");

builder.AddProject<Projects.PlayTicket_Blazor>("playticket-blazor", launchProfileName: "PlayTicket.Blazor");

builder.Build().Run();