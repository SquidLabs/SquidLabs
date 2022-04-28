using SquidLabs.Tentacles.Infrastructure.Abstractions;
using StackExchange.Redis;

namespace SquidLabs.Tentacles.Infrastructure.Redis;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TDataEntry"></typeparam>
public class RedisStore<TIdentifier, TDataEntry> : IDataStore<TIdentifier, TDataEntry>
    where TDataEntry : class, IDataEntry
{
    /// <summary>
    /// </summary>
    private readonly IClientFactory<TDataEntry, IRedisOptions<TDataEntry>, IDatabase> _clientFactory;

    /// <summary>
    /// </summary>
    /// <param name="clientFactory"></param>
    public RedisStore(IClientFactory<TDataEntry, IRedisOptions<TDataEntry>, IDatabase> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(TIdentifier id, TDataEntry content, CancellationToken cancellationToken = default)
    {
        await _clientFactory.GetClient().SetAsync(id!.ToString()!, content, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TDataEntry?> ReadAsync(TIdentifier id, CancellationToken cancellationToken = default)
    {
        return await _clientFactory.GetClient().GetAsync<TDataEntry>(id!.ToString()!, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(TIdentifier id, TDataEntry content, CancellationToken cancellationToken = default)
    {
        await WriteAsync(id, content, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default)
    {
        await _clientFactory.GetClient().KeyDeleteAsync(new RedisKey(id!.ToString())).WaitAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}