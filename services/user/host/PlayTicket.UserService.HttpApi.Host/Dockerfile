#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/projects/NuGet.Config", "services/projects/"]
COPY ["services/projects/host/PlayTicket.Projects.HttpApi.Host/PlayTicket.Projects.HttpApi.Host.csproj", "services/projects/host/PlayTicket.Projects.HttpApi.Host/"]
COPY ["services/projects/src/PlayTicket.Projects.HttpApi/PlayTicket.Projects.HttpApi.csproj", "services/projects/src/PlayTicket.Projects.HttpApi/"]
COPY ["services/projects/src/PlayTicket.Projects.Application.Contracts/PlayTicket.Projects.Application.Contracts.csproj", "services/projects/src/PlayTicket.Projects.Application.Contracts/"]
COPY ["services/projects/src/PlayTicket.Projects.Domain.Shared/PlayTicket.Projects.Domain.Shared.csproj", "services/projects/src/PlayTicket.Projects.Domain.Shared/"]
COPY ["services/projects/src/PlayTicket.Projects.Application/PlayTicket.Projects.Application.csproj", "services/projects/src/PlayTicket.Projects.Application/"]
COPY ["services/projects/src/PlayTicket.Projects.Domain/PlayTicket.Projects.Domain.csproj", "services/projects/src/PlayTicket.Projects.Domain/"]
COPY ["services/projects/src/PlayTicket.Projects.EntityFrameworkCore/PlayTicket.Projects.EntityFrameworkCore.csproj", "services/projects/src/PlayTicket.Projects.EntityFrameworkCore/"]
RUN dotnet restore "services/projects/host/PlayTicket.Projects.HttpApi.Host/PlayTicket.Projects.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/projects/host/PlayTicket.Projects.HttpApi.Host"
RUN dotnet build "PlayTicket.Projects.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayTicket.Projects.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayTicket.Projects.HttpApi.Host.dll"]