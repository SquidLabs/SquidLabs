namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TEntry"></typeparam>
public interface IStore<in TIdentifier, TEntry>
{
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WriteAsync(TIdentifier id, TEntry content,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntry?> ReadAsync(TIdentifier id, CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(TIdentifier id, TEntry content,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default);
}