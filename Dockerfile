# Usar imagem oficial do .NET 8 (ou sua versão)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BoraLe.Api.csproj", "./"]
RUN dotnet restore "./BoraLe.Api.csproj"
COPY . .
RUN dotnet publish "BoraLe.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BoraLe.Api.dll"]