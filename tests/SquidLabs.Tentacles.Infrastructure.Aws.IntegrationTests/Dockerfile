FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Aws/SquidLabs.Tentacles.Infrastructure.Aws.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Aws/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/SquidLabs.Tentacles.Infrastructure.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/"]
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Tests/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests.csproj"
COPY . .
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests"
RUN dotnet build "SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests.csproj" -c Release -o /app/build

FROM build AS testrunner
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests"
ENTRYPOINT ["dotnet", "test", "--no-restore"]
