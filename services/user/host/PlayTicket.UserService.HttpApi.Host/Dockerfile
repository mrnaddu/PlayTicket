#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/projects/NuGet.Config", "services/projects/"]
COPY ["services/projects/host/PlayTicket.UserService.HttpApi.Host/PlayTicket.UserService.HttpApi.Host.csproj", "services/projects/host/PlayTicket.UserService.HttpApi.Host/"]
COPY ["services/projects/src/PlayTicket.UserService.HttpApi/PlayTicket.UserService.HttpApi.csproj", "services/projects/src/PlayTicket.UserService.HttpApi/"]
COPY ["services/projects/src/PlayTicket.UserService.Application.Contracts/PlayTicket.UserService.Application.Contracts.csproj", "services/projects/src/PlayTicket.UserService.Application.Contracts/"]
COPY ["services/projects/src/PlayTicket.UserService.Domain.Shared/PlayTicket.UserService.Domain.Shared.csproj", "services/projects/src/PlayTicket.UserService.Domain.Shared/"]
COPY ["services/projects/src/PlayTicket.UserService.Application/PlayTicket.UserService.Application.csproj", "services/projects/src/PlayTicket.UserService.Application/"]
COPY ["services/projects/src/PlayTicket.UserService.Domain/PlayTicket.UserService.Domain.csproj", "services/projects/src/PlayTicket.UserService.Domain/"]
COPY ["services/projects/src/PlayTicket.UserService.EntityFrameworkCore/PlayTicket.UserService.EntityFrameworkCore.csproj", "services/projects/src/PlayTicket.UserService.EntityFrameworkCore/"]
RUN dotnet restore "services/projects/host/PlayTicket.UserService.HttpApi.Host/PlayTicket.UserService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/projects/host/PlayTicket.UserService.HttpApi.Host"
RUN dotnet build "PlayTicket.UserService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayTicket.UserService.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayTicket.UserService.HttpApi.Host.dll"]