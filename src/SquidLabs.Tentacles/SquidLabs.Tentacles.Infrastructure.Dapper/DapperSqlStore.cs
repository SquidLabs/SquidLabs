using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Dapper;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TDataEntry"></typeparam>
public class DapperSqlStore<TIdentifier, TDataEntry> : IDataStore<TIdentifier, TDataEntry>
    where TDataEntry : class, IDataEntry
{
    /// <summary>
    /// </summary>
    private readonly IClientFactory<TDataEntry, IDapperSqlStoreOptions<TDataEntry>, IDbConnection> _connectionFactory;

    /// <summary>
    /// </summary>
    /// <param name="connectionFactory"></param>
    public DapperSqlStore(
        IClientFactory<TDataEntry, IDapperSqlStoreOptions<TDataEntry>, IDbConnection> connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(TIdentifier id, TDataEntry content, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.GetClient();
        await connection.InsertAsync(content).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TDataEntry?> ReadAsync(TIdentifier id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.GetClient();
        return await connection.GetAsync<TDataEntry>(id).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(TIdentifier id, TDataEntry content, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.GetClient();
        await connection.UpdateAsync(content).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default)
    {
        var options = _connectionFactory.ClientOptions;
        using var connection = _connectionFactory.GetClient();
        await connection
            .QueryAsync($"DELETE FROM {options.Table} WHERE {options.Key} = @id", new { id })
            .ConfigureAwait(false);
    }
}