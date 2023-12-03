using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Azure.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.Azure;

public class AzureFileStore : IFileStore<string, Stream>
{
    private readonly AzureBlobOptions _options;
    private BlobClientOptions _config;
    private BlobContainerClient _container;

    public AzureFileStore(IOptionsMonitor<AzureBlobOptions> blobOptions)
    {
        _options = blobOptions.CurrentValue;
        var container = new BlobContainerClient(_options.ConnectionString, _options.ContainerName, _config);
        container.CreateIfNotExists();
    }

    public async Task WriteAsync(string id, Stream inputStream,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var blob = _container.GetBlobClient(id);
        await blob.UploadAsync(inputStream, cancellationToken).ConfigureAwait(false);
        ;
    }

    public async Task<Stream> ReadAsync(string id,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var blob = _container.GetBlobClient(id);
        var result = await blob.DownloadStreamingAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        ;
        return result.Value.Content;
    }

    public async Task UpdateAsync(string id, Stream inputStream,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await WriteAsync(id, inputStream, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _container.DeleteBlobAsync(id, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}