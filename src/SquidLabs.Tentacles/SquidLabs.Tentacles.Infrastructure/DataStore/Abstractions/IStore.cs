namespace SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TEntry"></typeparam>
public interface IStore<TIdentifier, TEntry>
{
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    Task WriteAsync(TIdentifier id, TEntry content,
        CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntry> ReadAsync(TIdentifier id, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    Task UpdateAsync(TIdentifier id, TEntry content,
        CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default(CancellationToken));
}