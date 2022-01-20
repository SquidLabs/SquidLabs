using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Extensions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Options;
using StackExchange.Redis;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

public class RedisStore<TKey, TEntity> : IDataStore<TKey, TEntity>
    where TEntity : class
{
    private readonly ConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;
    private readonly RedisOptions _redisOptions;

    public RedisStore(IOptionsMonitor<RedisOptions> redisOptions)
    {
        _redisOptions = redisOptions.CurrentValue;
        _connectionMultiplexer = ConnectionMultiplexer.Connect(_redisOptions.ConnectionString);
        _database = _connectionMultiplexer.GetDatabase(1);
    }

    public async Task WriteAsync(TKey identifier, TEntity content, CancellationToken cancellationToken)
    {
        await _database.SetAsync(identifier.ToString(), content, cancellationToken);
    }

    public async Task<TEntity> ReadAsync(TKey identifier, CancellationToken cancellationToken)
    {
        return await _database.GetAsync<TEntity>(identifier.ToString());
    }

    public async Task UpdateAsync(TKey identifier, TEntity content, CancellationToken cancellationToken)
    {
        await WriteAsync(identifier, content, cancellationToken);
    }

    public async Task DeleteAsync(TKey identifier, CancellationToken cancellationToken)
    {
        await _database.KeyDeleteAsync(new RedisKey(identifier.ToString())).WaitAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}