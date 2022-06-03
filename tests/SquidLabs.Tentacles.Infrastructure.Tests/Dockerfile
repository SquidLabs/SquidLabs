FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj", "SquidLabs.Tentacles.Infrastructure.Tests/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/SquidLabs.Tentacles.Infrastructure.csproj", "SquidLabs.Tentacles.Infrastructure/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj"
COPY . .
WORKDIR "/src/SquidLabs.Tentacles.Infrastructure.Tests"
RUN dotnet build "SquidLabs.Tentacles.Infrastructure.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SquidLabs.Tentacles.Infrastructure.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SquidLabs.Tentacles.Infrastructure.Tests.dll"]
