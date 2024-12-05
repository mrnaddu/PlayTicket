#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/host/PlayTicket.Administration.HttpApi.Host/PlayTicket.Administration.HttpApi.Host.csproj", "services/administration/host/PlayTicket.Administration.HttpApi.Host/"]
COPY ["shared/PlayTicket.Hosting.Shared/PlayTicket.Hosting.Shared.csproj", "shared/PlayTicket.Hosting.Shared/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Application.Contracts/PlayTicket.IdentityService.Application.Contracts.csproj", "services/identity/src/PlayTicket.IdentityService.Application.Contracts/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Domain.Shared/PlayTicket.IdentityService.Domain.Shared.csproj", "services/identity/src/PlayTicket.IdentityService.Domain.Shared/"]
COPY ["services/identity/src/PlayTicket.IdentityService.EntityFrameworkCore/PlayTicket.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/PlayTicket.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Domain/PlayTicket.IdentityService.Domain.csproj", "services/identity/src/PlayTicket.IdentityService.Domain/"]
COPY ["services/saas/src/PlayTicket.SaaS.Application.Contracts/PlayTicket.SaaS.Application.Contracts.csproj", "services/saas/src/PlayTicket.SaaS.Application.Contracts/"]
COPY ["services/saas/src/PlayTicket.SaaS.Domain.Shared/PlayTicket.SaaS.Domain.Shared.csproj", "services/saas/src/PlayTicket.SaaS.Domain.Shared/"]
COPY ["services/administration/src/PlayTicket.Administration.HttpApi/PlayTicket.Administration.HttpApi.csproj", "services/administration/src/PlayTicket.Administration.HttpApi/"]
COPY ["services/administration/src/PlayTicket.Administration.Application.Contracts/PlayTicket.Administration.Application.Contracts.csproj", "services/administration/src/PlayTicket.Administration.Application.Contracts/"]
COPY ["services/administration/src/PlayTicket.Administration.Domain.Shared/PlayTicket.Administration.Domain.Shared.csproj", "services/administration/src/PlayTicket.Administration.Domain.Shared/"]
COPY ["services/administration/src/PlayTicket.Administration.Application/PlayTicket.Administration.Application.csproj", "services/administration/src/PlayTicket.Administration.Application/"]
COPY ["services/administration/src/PlayTicket.Administration.Domain/PlayTicket.Administration.Domain.csproj", "services/administration/src/PlayTicket.Administration.Domain/"]
COPY ["services/administration/src/PlayTicket.Administration.EntityFrameworkCore/PlayTicket.Administration.EntityFrameworkCore.csproj", "services/administration/src/PlayTicket.Administration.EntityFrameworkCore/"]
RUN dotnet restore "services/administration/host/PlayTicket.Administration.HttpApi.Host/PlayTicket.Administration.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/administration/host/PlayTicket.Administration.HttpApi.Host"
RUN dotnet build "PlayTicket.Administration.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayTicket.Administration.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayTicket.Administration.HttpApi.Host.dll"]