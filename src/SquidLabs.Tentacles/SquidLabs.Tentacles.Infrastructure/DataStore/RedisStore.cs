using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Extensions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Options;
using StackExchange.Redis;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

public class RedisStore<TKey, TEntry> : IDataStore<TKey, TEntry>
    where TEntry : class, IDataEntry
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

    public async Task WriteAsync(TKey id, TEntry content, CancellationToken cancellationToken)
    {
        await _database.SetAsync(id.ToString(), content, cancellationToken);
    }

    public async Task<TEntry> ReadAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _database.GetAsync<TEntry>(id.ToString());
    }

    public async Task UpdateAsync(TKey id, TEntry content, CancellationToken cancellationToken)
    {
        await WriteAsync(id, content, cancellationToken);
    }

    public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
    {
        await _database.KeyDeleteAsync(new RedisKey(id.ToString())).WaitAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}