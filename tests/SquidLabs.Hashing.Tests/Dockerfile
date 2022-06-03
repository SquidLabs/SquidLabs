FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/SquidLabs.Hashing.Tests/SquidLabs.Hashing.Tests.csproj", "SquidLabs.Hashing.Tests/"]
COPY ["src/SquidLabs.Hashing/SquidLabs.Hashing.csproj", "SquidLabs.Hashing/"]
RUN dotnet restore "tests/SquidLabs.Hashing.Tests/SquidLabs.Hashing.Tests.csproj"
COPY . .
WORKDIR "/src/SquidLabs.Hashing.Tests"
RUN dotnet build "SquidLabs.Hashing.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SquidLabs.Hashing.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SquidLabs.Hashing.Tests.dll"]
