# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos .csproj primeiro para cache
COPY ["BoraLe.Api/BoraLe.Api.csproj", "BoraLe.Api/"]
COPY ["BoraLe.Application/BoraLe.Application.csproj", "BoraLe.Application/"]
COPY ["BoraLe.Domain/BoraLe.Domain.csproj", "BoraLe.Domain/"]
COPY ["BoraLe.Infrastructure/BoraLe.Infrastructure.csproj", "BoraLe.Infrastructure/"]

# Restaurar pacotes
RUN dotnet restore "BoraLe.Api/BoraLe.Api.csproj"

# Copiar todo o código
COPY . .

# Publish
WORKDIR /src/BoraLe.Api
RUN dotnet publish "BoraLe.Api.csproj" -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BoraLe.Api.dll"]