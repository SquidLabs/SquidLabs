using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Tests;

public class TestCacheDataStore<TIdentifier, TEntity> : IDataStore<TIdentifier, TEntity>
{
    /// <summary>
    ///     I Used this here because it has a cancellation token.
    /// </summary>
    private static readonly Lazy<IMemoryCache> _cache =
        new Lazy<IMemoryCache>(() => new MemoryCache(new MemoryCacheOptions()));

    public async Task WriteAsync(TIdentifier identifier, TEntity content,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.Run(async () => { _cache.Value.Set(identifier, content); }, cancellationToken);
    }

    public async Task<TEntity> ReadAsync(TIdentifier identifier,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Task.Run(async () => _cache.Value.Get<TEntity>(identifier), cancellationToken);
    }

    public async Task UpdateAsync(TIdentifier identifier, TEntity content,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await WriteAsync(identifier, content, cancellationToken);
    }

    public async Task DeleteAsync(TIdentifier identifier,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.Run(async () => _cache.Value.Remove(identifier), cancellationToken);
    }
}