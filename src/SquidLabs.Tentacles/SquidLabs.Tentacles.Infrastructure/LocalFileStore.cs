using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure;

/// <summary>
/// </summary>
public class LocalFileStore<TFileEntry> : IFileStore<string, TFileEntry> where TFileEntry : class, IFileEntry
{
    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileEntry"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(string path, TFileEntry fileEntry,
        CancellationToken cancellationToken = default)
    {
        await using var destinationStream = new FileStream(path,
            FileMode.Append, FileAccess.Write, FileShare.None,
            4096, true);
        var stream = await fileEntry.ToStreamAsync(cancellationToken).ConfigureAwait(false);
        await stream.CopyToAsync(destinationStream, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TFileEntry?> ReadAsync(string fileName, CancellationToken cancellationToken = default)
    {
        return (TFileEntry?)await TFileEntry.FromStreamAsync<TFileEntry>(fileName, new FileStream(fileName,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            4096, true), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileEntry"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(string path, TFileEntry fileEntry,
        CancellationToken cancellationToken = default)
    {
        await using var destinationStream = new FileStream(path,
            FileMode.Truncate, FileAccess.Write, FileShare.None,
            4096, true);
        var stream = await fileEntry.ToStreamAsync(cancellationToken).ConfigureAwait(false);
        await stream.CopyToAsync(destinationStream, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(string path, CancellationToken cancellationToken = default)
    {
        await new Task(() =>
        {
            var file = new FileInfo(path);
            file.Delete();
        }, cancellationToken).ConfigureAwait(false);
    }
}