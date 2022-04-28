using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Dapper;

public class DapperSqlStoreOptions<TDataEntry> : IConnectionOptions<TDataEntry> where TDataEntry : IDataEntry
{
    public string ConnectionString { get; set; }
}