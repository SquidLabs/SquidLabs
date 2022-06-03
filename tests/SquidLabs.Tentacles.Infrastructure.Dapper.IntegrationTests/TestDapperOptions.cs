using System;
using SquidLabs.Tentacles.Infrastructure.Dapper;
using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests;

public class TestDapperOptions : IDapperSqlStoreOptions<TestDataEntry>
{
    public string Table { get; init; } = null!;
    public string ConnectionString { get; init; }= null!;
    public string Key { get; init; } = null!;
}