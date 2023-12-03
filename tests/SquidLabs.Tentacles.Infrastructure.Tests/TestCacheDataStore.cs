using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Tests;

public class TestCacheDataStore<TIdentifier, TEntity> : IDataStore<TIdentifier, TEntity> where TEntity : IDataEntry
{
    /// <summary>
    ///     I Used this here because it has a cancellation token.
    /// </summary>
    private static readonly Lazy<IMemoryCache> _cache =
        new Lazy<IMemoryCache>(() => new MemoryCache(new MemoryCacheOptions()));

    public async Task WriteAsync(TIdentifier id, TEntity content,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.Run(async () => { _cache.Value.Set(id, content); }, cancellationToken);
    }

    public async Task<TEntity> ReadAsync(TIdentifier id,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Task.Run(async () => _cache.Value.Get<TEntity>(id), cancellationToken);
    }

    public async Task UpdateAsync(TIdentifier id, TEntity content,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await WriteAsync(id, content, cancellationToken);
    }

    public async Task DeleteAsync(TIdentifier id,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.Run(async () => _cache.Value.Remove(id), cancellationToken);
    }
}