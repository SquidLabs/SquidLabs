FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Elastic/SquidLabs.Tentacles.Infrastructure.Elastic.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Elastic/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/SquidLabs.Tentacles.Infrastructure.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/"]
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Tests/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests.csproj"
COPY . .
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests"
RUN dotnet build "SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests.csproj" -c Release -o /app/build

FROM build AS testrunner
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests"
ENTRYPOINT ["dotnet", "test", "--no-restore"]
