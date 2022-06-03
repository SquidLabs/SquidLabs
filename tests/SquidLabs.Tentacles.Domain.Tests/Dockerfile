FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/SquidLabs.Tentacles.Domain.Tests/SquidLabs.Tentacles.Domain.Tests.csproj", "SquidLabs.Tentacles.Domain.Tests/"]
COPY ["src/SquidLabs.Hashing/SquidLabs.Hashing.csproj", "SquidLabs.Hashing/"]
COPY ["src/SquidLabs.Tentacles/SquidLabs.Tentacles.Domain/SquidLabs.Tentacles.Domain.csproj", "SquidLabs.Tentacles.Domain/"]
RUN dotnet restore "tests/SquidLabs.Tentacles.Domain.Tests/SquidLabs.Tentacles.Domain.Tests.csproj"
COPY . .
WORKDIR "/src/SquidLabs.Tentacles.Domain.Tests"
RUN dotnet build "SquidLabs.Tentacles.Domain.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SquidLabs.Tentacles.Domain.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SquidLabs.Tentacles.Domain.Tests.dll"]
