using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Dapper;

public interface IDapperSqlStoreOptions<TDataEntry> : IConnectionOptions<TDataEntry> where TDataEntry : IDataEntry
{
    public string Table { get; init; }
    public string ConnectionString { get; init; }
    public string Key { get; init; }
}