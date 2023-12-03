using Azure.Storage.Blobs;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Azure;

/// <summary>
/// </summary>
public class AzureFileStore<TFileEntry> : IFileStore<Guid, TFileEntry> where TFileEntry : class, IFileEntry
{
    /// <summary>
    /// </summary>
    private readonly IClientFactory<TFileEntry, IAzureBlobOptions<TFileEntry>, BlobContainerClient> _clientFactory;

    /// <summary>
    /// </summary>
    /// <param name="clientFactory"></param>
    public AzureFileStore(IClientFactory<TFileEntry, IAzureBlobOptions<TFileEntry>, BlobContainerClient> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileEntry"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(Guid id, TFileEntry fileEntry,
        CancellationToken cancellationToken = default)
    {
        var blob = _clientFactory.GetBlobClient(id.ToString());
        await blob.UploadAsync(await fileEntry.ToStreamAsync(cancellationToken), cancellationToken)
            .ConfigureAwait(false);
        ;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TFileEntry?> ReadAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var blob = _clientFactory.GetBlobClient(id.ToString());
        var result = await blob.DownloadStreamingAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return (TFileEntry?)await TFileEntry.FromStreamAsync<TFileEntry>(id.ToString(), result.Value.Content, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileEntry"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(Guid id, TFileEntry fileEntry,
        CancellationToken cancellationToken = default)
    {
        await WriteAsync(id, fileEntry, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _clientFactory.GetClient()
            .DeleteBlobAsync(id.ToString(), cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}