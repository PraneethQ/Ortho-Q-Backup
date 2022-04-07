#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/UserManagement/UserManagement.API/UserManagement.API.csproj", "Services/UserManagement/UserManagement.API/"]
COPY ["Services/UserManagement/UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "Services/UserManagement/UserManagement.Infrastructure/"]
COPY ["Services/UserManagement/UserManagement.Application/UserManagement.Application.csproj", "Services/UserManagement/UserManagement.Application/"]
COPY ["Services/UserManagement/UserManagement.Domain/UserManagement.Domain.csproj", "Services/UserManagement/UserManagement.Domain/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "Services/UserManagement/UserManagement.API/UserManagement.API.csproj"
COPY . .
WORKDIR "/src/Services/UserManagement/UserManagement.API"
RUN dotnet build "UserManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.API.dll"]