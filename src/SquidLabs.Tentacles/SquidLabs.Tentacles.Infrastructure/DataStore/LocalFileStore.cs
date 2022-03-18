using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

/// <summary>
/// </summary>
public class LocalFileStore : IFileStore<string, Stream>
{
    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="inputStream"></param>
    public async Task WriteAsync(string path, Stream inputStream,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await using var destinationStream = new FileStream(path,
            FileMode.Append, FileAccess.Write, FileShare.None,
            4096, true);
        await inputStream.CopyToAsync(destinationStream, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public async Task<Stream> ReadAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
    {
        return new FileStream(path,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            4096, true);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="inputStream"></param>
    public async Task UpdateAsync(string path, Stream inputStream,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await using var destinationStream = new FileStream(path,
            FileMode.Truncate, FileAccess.Write, FileShare.None,
            4096, true);
        await inputStream.CopyToAsync(destinationStream, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    public async Task DeleteAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
    {
        await new Task(() =>
        {
            var file = new FileInfo(path);
            file.Delete();
        }, cancellationToken);
    }
}