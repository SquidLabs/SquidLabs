FROM mcr.microsoft.com/dotnet/sdk:7.0.100-preview.4-bullseye-slim-amd64 AS build
WORKDIR /build
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Dapper/SquidLabs.Tentacles.Infrastructure.Dapper.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Dapper/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/SquidLabs.Tentacles.Infrastructure.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/"]
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Tests/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests.csproj"
COPY . .
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests"
RUN dotnet build "SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests.dll"]
