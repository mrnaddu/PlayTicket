#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["shared/PlayTicket.DbMigrator/PlayTicket.DbMigrator.csproj", "shared/PlayTicket.DbMigrator/"]
COPY ["services/administration/src/PlayTicket.CashVoucherService.Application.Contracts/PlayTicket.CashVoucherService.Application.Contracts.csproj", "services/administration/src/PlayTicket.CashVoucherService.Application.Contracts/"]
COPY ["services/administration/src/PlayTicket.CashVoucherService.Domain.Shared/PlayTicket.CashVoucherService.Domain.Shared.csproj", "services/administration/src/PlayTicket.CashVoucherService.Domain.Shared/"]
COPY ["services/administration/src/PlayTicket.CashVoucherService.EntityFrameworkCore/PlayTicket.CashVoucherService.EntityFrameworkCore.csproj", "services/administration/src/PlayTicket.CashVoucherService.EntityFrameworkCore/"]
COPY ["services/administration/src/PlayTicket.CashVoucherService.Domain/PlayTicket.CashVoucherService.Domain.csproj", "services/administration/src/PlayTicket.CashVoucherService.Domain/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Application.Contracts/PlayTicket.IdentityService.Application.Contracts.csproj", "services/identity/src/PlayTicket.IdentityService.Application.Contracts/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Domain.Shared/PlayTicket.IdentityService.Domain.Shared.csproj", "services/identity/src/PlayTicket.IdentityService.Domain.Shared/"]
COPY ["services/identity/src/PlayTicket.IdentityService.EntityFrameworkCore/PlayTicket.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/PlayTicket.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/PlayTicket.IdentityService.Domain/PlayTicket.IdentityService.Domain.csproj", "services/identity/src/PlayTicket.IdentityService.Domain/"]
COPY ["services/saas/src/PlayTicket.SaaS.Application.Contracts/PlayTicket.SaaS.Application.Contracts.csproj", "services/saas/src/PlayTicket.SaaS.Application.Contracts/"]
COPY ["services/saas/src/PlayTicket.SaaS.Domain.Shared/PlayTicket.SaaS.Domain.Shared.csproj", "services/saas/src/PlayTicket.SaaS.Domain.Shared/"]
COPY ["services/saas/src/PlayTicket.SaaS.EntityFrameworkCore/PlayTicket.SaaS.EntityFrameworkCore.csproj", "services/saas/src/PlayTicket.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/PlayTicket.SaaS.Domain/PlayTicket.SaaS.Domain.csproj", "services/saas/src/PlayTicket.SaaS.Domain/"]
COPY ["shared/PlayTicket.Microservice.Shared/PlayTicket.Microservice.Shared.csproj", "shared/PlayTicket.Microservice.Shared/"]
COPY ["shared/PlayTicket.Hosting.Shared/PlayTicket.Hosting.Shared.csproj", "shared/PlayTicket.Hosting.Shared/"]
RUN dotnet restore "shared/PlayTicket.DbMigrator/PlayTicket.DbMigrator.csproj"
COPY . .
WORKDIR "/src/shared/PlayTicket.DbMigrator"
RUN dotnet build "PlayTicket.DbMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayTicket.DbMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayTicket.DbMigrator.dll"]