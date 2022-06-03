FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/SquidLabs.Linq.Extenstions.Test/SquidLabs.Linq.Extenstions.Test.csproj", "SquidLabs.Linq.Extenstions.Test/"]
RUN dotnet restore "tests/SquidLabs.Linq.Extenstions.Test/SquidLabs.Linq.Extenstions.Test.csproj"
COPY . .
WORKDIR "/src/SquidLabs.Linq.Extenstions.Test"
RUN dotnet build "SquidLabs.Linq.Extenstions.Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SquidLabs.Linq.Extenstions.Test.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SquidLabs.Linq.Extenstions.Test.dll"]
