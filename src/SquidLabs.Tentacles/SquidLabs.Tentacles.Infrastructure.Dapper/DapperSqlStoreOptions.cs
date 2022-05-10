using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Dapper;

public class DapperSqlStoreOptions<TDataEntry> : IConnectionOptions<TDataEntry> where TDataEntry : IDataEntry
{
    public string Table { get; init; } = null!;
    public string ConnectionString { get; init; } = null!;

    public string Key { get; init; } = null!;
}