# Imagem base do ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Imagem de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o projeto da subpasta
COPY ["BoraLe.Api/BoraLe.Api.csproj", "BoraLe.Api/"]
RUN dotnet restore "BoraLe.Api/BoraLe.Api.csproj"

# Copiar todo o conteúdo da subpasta
COPY BoraLe.Api/ BoraLe.Api/
WORKDIR /src/BoraLe.Api
RUN dotnet publish "BoraLe.Api.csproj" -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BoraLe.Api.dll"]