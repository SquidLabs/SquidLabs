namespace SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TEntry"></typeparam>
public interface IDataStore<TIdentifier, TEntry>
{
    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    Task WriteAsync(TIdentifier identifier, TEntry content,
        CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    Task<TEntry> ReadAsync(TIdentifier identifier, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    Task UpdateAsync(TIdentifier identifier, TEntry content,
        CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    Task DeleteAsync(TIdentifier identifier, CancellationToken cancellationToken = default(CancellationToken));
}