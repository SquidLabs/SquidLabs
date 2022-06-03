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
    static abstract Task<IFileEntry> FromStreamAsync(string fileName, Stream stream,
        CancellationToken cancellationToken = default);
}