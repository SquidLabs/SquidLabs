using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.Abstractions;
using StackExchange.Redis;

namespace SquidLabs.Tentacles.Infrastructure.Redis;

public class RedisClientFactory<TDataEntry> : IClientFactory<TDataEntry, IRedisOptions<TDataEntry>, IDatabase>
    where TDataEntry : IDataEntry
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    private readonly IOptionsMonitor<IRedisOptions<TDataEntry>> _redisOptionsMonitor;

    public RedisClientFactory(IOptionsMonitor<IRedisOptions<TDataEntry>> redisOptionsMonitor)
    {
        _redisOptionsMonitor = redisOptionsMonitor;
        _connectionMultiplexer = ConnectionMultiplexer.Connect(_redisOptionsMonitor.CurrentValue.ConnectionDefinition);
    }

    public IRedisOptions<TDataEntry> ClientOptions => _redisOptionsMonitor.CurrentValue;

    public IDatabase GetClient()
    {
        return _connectionMultiplexer.GetDatabase(_redisOptionsMonitor.CurrentValue.Database);
    }
}