using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Dapper;

public class
    DapperClientFactory<TDataEntry> : IClientFactory<TDataEntry, IDapperSqlStoreOptions<TDataEntry>, IDbConnection>
    where TDataEntry : IDataEntry
{
    private readonly IOptionsMonitor<IDapperSqlStoreOptions<TDataEntry>> _connectionOptionsMonitor;

    public DapperClientFactory(IOptionsMonitor<IDapperSqlStoreOptions<TDataEntry>> connectionOptionsMonitor)
    {
        _connectionOptionsMonitor = connectionOptionsMonitor;
    }

    public IDapperSqlStoreOptions<TDataEntry> ClientOptions => _connectionOptionsMonitor.CurrentValue;

    public IDbConnection GetClient()
    {
        // Pooling is free here but require you wrap it in using as it implements IDisposable
        // https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-connection-pooling?redirectedfrom=MSDN#pool-creation-and-assignment
        return new SqlConnection(_connectionOptionsMonitor.CurrentValue.ConnectionString);
    }
}