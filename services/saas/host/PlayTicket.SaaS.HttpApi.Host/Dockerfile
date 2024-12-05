#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/host/PlayTicket.SaaS.HttpApi.Host/PlayTicket.SaaS.HttpApi.Host.csproj", "services/saas/host/PlayTicket.SaaS.HttpApi.Host/"]
COPY ["shared/PlayTicket.Hosting.Shared/PlayTicket.Hosting.Shared.csproj", "shared/PlayTicket.Hosting.Shared/"]
COPY ["services/administration/src/PlayTicket.Administration.EntityFrameworkCore/PlayTicket.Administration.EntityFrameworkCore.csproj", "services/administration/src/PlayTicket.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/PlayTicket.Administration.Domain/PlayTicket.Administration.Domain.csproj", "services/administration/src/PlayTicket.Administration.Domain/"]
COPY ["services/administration/src/PlayTicket.Administration.Domain.Shared/PlayTicket.Administration.Domain.Shared.csproj", "services/administration/src/PlayTicket.Administration.Domain.Shared/"]
COPY ["services/saas/src/PlayTicket.SaaS.HttpApi/PlayTicket.SaaS.HttpApi.csproj", "services/saas/src/PlayTicket.SaaS.HttpApi/"]
COPY ["services/saas/src/PlayTicket.SaaS.Application.Contracts/PlayTicket.SaaS.Application.Contracts.csproj", "services/saas/src/PlayTicket.SaaS.Application.Contracts/"]
COPY ["services/saas/src/PlayTicket.SaaS.Domain.Shared/PlayTicket.SaaS.Domain.Shared.csproj", "services/saas/src/PlayTicket.SaaS.Domain.Shared/"]
COPY ["services/saas/src/PlayTicket.SaaS.Application/PlayTicket.SaaS.Application.csproj", "services/saas/src/PlayTicket.SaaS.Application/"]
COPY ["services/saas/src/PlayTicket.SaaS.Domain/PlayTicket.SaaS.Domain.csproj", "services/saas/src/PlayTicket.SaaS.Domain/"]
COPY ["services/saas/src/PlayTicket.SaaS.EntityFrameworkCore/PlayTicket.SaaS.EntityFrameworkCore.csproj", "services/saas/src/PlayTicket.SaaS.EntityFrameworkCore/"]
RUN dotnet restore "services/saas/host/PlayTicket.SaaS.HttpApi.Host/PlayTicket.SaaS.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/saas/host/PlayTicket.SaaS.HttpApi.Host"
RUN dotnet build "PlayTicket.SaaS.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayTicket.SaaS.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayTicket.SaaS.HttpApi.Host.dll"]