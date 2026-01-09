# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/Producao.Api/Producao.Api.csproj src/Producao.Api/
COPY src/Producao.Application/Producao.Application.csproj src/Producao.Application/
COPY src/Producao.Domain/Producao.Domain.csproj src/Producao.Domain/
COPY src/Producao.Infrastructure/Producao.Infrastructure.csproj src/Producao.Infrastructure/

RUN dotnet restore src/Producao.Api/Producao.Api.csproj

COPY . .
WORKDIR /src/src/Producao.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8081
ENTRYPOINT ["dotnet", "Producao.Api.dll"]
