FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Mongo/SquidLabs.Tentacles.Infrastructure.Mongo.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure.Mongo/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/SquidLabs.Tentacles.Infrastructure.csproj", "src/SquidLabs.Tentacles/SquidLabs.Tentacles.Infrastructure/"]
COPY ["tests/SquidLabs.Tentacles.Infrastructure.Tests/SquidLabs.Tentacles.Infrastructure.Tests.csproj", "tests/SquidLabs.Tentacles.Infrastructure.Tests/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests/SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests.csproj"
COPY . .
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests"
RUN dotnet build "SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests.csproj" -c Release -o /app/build

FROM build AS testrunner
WORKDIR "/build/tests/SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests"
ENTRYPOINT ["dotnet", "test", "--no-restore"]
