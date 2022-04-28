using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Dapper;

public class
    DbConnectionFactory<TDataEntry> : IClientFactory<TDataEntry, DapperSqlStoreOptions<TDataEntry>, IDbConnection>
    where TDataEntry : IDataEntry
{
    private readonly IOptionsMonitor<DapperSqlStoreOptions<TDataEntry>> _connectionOptionsMonitor;

    public DbConnectionFactory(IOptionsMonitor<DapperSqlStoreOptions<TDataEntry>> connectionOptionsMonitor)
    {
        _connectionOptionsMonitor = connectionOptionsMonitor;
    }

    public DapperSqlStoreOptions<TDataEntry> ClientOptions => _connectionOptionsMonitor.CurrentValue;

    public IDbConnection GetClient()
    {
        // Pooling is free here but require you wrap it in using as it implements IDisposable
        // https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-connection-pooling?redirectedfrom=MSDN#pool-creation-and-assignment
        return new SqlConnection(_connectionOptionsMonitor.CurrentValue.ConnectionString);
    }
}