FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Redis/SquidLabs.Tentacles.Infrastructure.Redis.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Redis/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/SquidLabs.Tentacles.Infrastructure.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/"]
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Tests/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests.csproj"
COPY . .
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests"
RUN dotnet build "SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests.csproj" -c Release -o /app/build

FROM build AS testrunner
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests"
ENTRYPOINT ["dotnet", "test", "--no-restore"]