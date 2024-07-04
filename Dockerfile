#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SE171762.ProductManagement.API/SE171762.ProductManagement.API.csproj", "SE171762.ProductManagement.API/"]
COPY ["NET171462.ProductManagement.Repo/SE171462.ProductManagement.Repo.csproj", "NET171462.ProductManagement.Repo/"]
RUN dotnet restore "./SE171762.ProductManagement.API/./SE171762.ProductManagement.API.csproj"
COPY . .
WORKDIR "/src/SE171762.ProductManagement.API"
RUN dotnet build "./SE171762.ProductManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SE171762.ProductManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SE171762.ProductManagement.API.dll"]