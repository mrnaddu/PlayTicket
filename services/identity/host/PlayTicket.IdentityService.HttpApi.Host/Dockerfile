#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/identity/host/PlayTicket.IdentityService.HttpApi.Host/PlayTicket.IdentityService.HttpApi.Host.csproj", "services/identity/host/PlayTicket.IdentityService.HttpApi.Host/"]
COPY ["shared/PlayTicket.Hosting.Shared/PlayTicket.Hosting.Shared.csproj", "shared/PlayTicket.Hosting.Shared/"]
COPY ["services/administration/src/PlayTicket.Administration.EntityFrameworkCore/PlayTicket.Administration.EntityFrameworkCore.csproj", "services/administration/src/PlayTicket.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/PlayTicket.Administration.Domain/PlayTicket.Administration.Domain.csproj", "services/administration/src/PlayTicket.Administration.Domain/"]
COPY ["services/administration/src/PlayTicket.Administration.Domain.Shared/PlayTicket.Administration.Domain.Shared.csproj", "services/administration/src/PlayTicket.Administration.Domain.Shared/"]
COPY ["services/saas/src/PlayTicket.SaaS.EntityFrameworkCore/PlayTicket.SaaS.EntityFrameworkCore.csproj", "services/saas/src/PlayTicket.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/PlayTicket.SaaS.Domain/PlayTicket.SaaS.Domain.csproj", "services/saas/src/PlayTicket.SaaS.Domain/"]
COPY ["services/saas/src/PlayTicket.SaaS.Domain.Shared/PlayTicket.SaaS.Domain.Shared.csproj", "services/saas/src/PlayTicket.SaaS.Domain.Shared/"]
COPY ["services/identity/src/PlayTicket.IdentityService.HttpApi/PlayTicket.IdentityService.HttpApi.csproj", "services/identity/src/PlayTicket.IdentityService.HttpApi/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Application.Contracts/PlayTicket.IdentityService.Application.Contracts.csproj", "services/identity/src/PlayTicket.IdentityService.Application.Contracts/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Domain.Shared/PlayTicket.IdentityService.Domain.Shared.csproj", "services/identity/src/PlayTicket.IdentityService.Domain.Shared/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Application/PlayTicket.IdentityService.Application.csproj", "services/identity/src/PlayTicket.IdentityService.Application/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Domain/PlayTicket.IdentityService.Domain.csproj", "services/identity/src/PlayTicket.IdentityService.Domain/"]
COPY ["services/identity/src/PlayTicket.IdentityService.EntityFrameworkCore/PlayTicket.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/PlayTicket.IdentityService.EntityFrameworkCore/"]
RUN dotnet restore "services/identity/host/PlayTicket.IdentityService.HttpApi.Host/PlayTicket.IdentityService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/identity/host/PlayTicket.IdentityService.HttpApi.Host"
RUN dotnet build "PlayTicket.IdentityService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayTicket.IdentityService.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayTicket.IdentityService.HttpApi.Host.dll"]