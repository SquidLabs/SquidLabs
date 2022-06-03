FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Azure/SquidLabs.Tentacles.Infrastructure.Azure.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Azure/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/SquidLabs.Tentacles.Infrastructure.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/"]
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Tests/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests.csproj"
COPY . .
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests"
RUN dotnet build "SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests.csproj" -c Release -o /app/build

FROM build AS testrunner
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests"
ENTRYPOINT ["dotnet", "test", "--no-restore"]
