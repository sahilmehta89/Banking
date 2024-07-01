#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Banking.API/Banking.API.csproj", "Banking.API/"]
COPY ["Banking.Core.Model/Banking.Core.Model.csproj", "Banking.Core.Model/"]
COPY ["Banking.Core/Banking.Core.csproj", "Banking.Core/"]
COPY ["Banking.Persistence.PostgreSQL/Banking.Persistence.PostgreSQL.csproj", "Banking.Persistence.PostgreSQL/"]
COPY ["Banking.Utilities.Common/Banking.Utilities.Common.csproj", "Banking.Utilities.Common/"]
COPY ["Banking.Services/Banking.Services.csproj", "Banking.Services/"]
RUN dotnet restore "./Banking.API/Banking.API.csproj"
COPY . .
WORKDIR "/src/Banking.API"
RUN dotnet build "./Banking.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Banking.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.API.dll"]