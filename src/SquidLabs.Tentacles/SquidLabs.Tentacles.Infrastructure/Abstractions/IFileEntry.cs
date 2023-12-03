namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

/// <summary>
/// </summary>
public interface IFileEntry : IDataEntry
{
    /// <summary>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> ToStreamAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    static virtual Task<T?> FromStreamAsync<T>(string fileName, Stream stream,
        CancellationToken cancellationToken = default) where T: class, IFileEntry
    {
        return Task.FromResult(Activator.CreateInstance(typeof(T), fileName, stream) as T);
    }
}