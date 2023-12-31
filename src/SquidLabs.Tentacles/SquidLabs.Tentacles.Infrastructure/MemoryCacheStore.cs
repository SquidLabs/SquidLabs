using Microsoft.Extensions.Caching.Memory;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TDataEntry"></typeparam>
public class MemoryCacheStore<TIdentifier, TDataEntry> : IDataStore<TIdentifier, TDataEntry>
    where TDataEntry : IDataEntry
    where TIdentifier : notnull
{
    /// <summary>
    /// </summary>
    private static IMemoryCache _cache = null!;

    /// <summary>
    /// </summary>
    private static MemoryCacheOptions _options = null!;

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    public MemoryCacheStore(MemoryCacheOptionsOfTDataEntry<TDataEntry> options)
    {
        _options = options;
        _cache = new MemoryCache(options);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(TIdentifier id, TDataEntry content,
        CancellationToken cancellationToken = default)
    {
        await Task.Run(() => { _cache.Set(id, content); }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TDataEntry?> ReadAsync(TIdentifier id,
        CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _cache.Get<TDataEntry>(id), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(TIdentifier id, TDataEntry content,
        CancellationToken cancellationToken = default)
    {
        await WriteAsync(id, content, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TIdentifier id,
        CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _cache.Remove(id), cancellationToken).ConfigureAwait(false);
    }
}