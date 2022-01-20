using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Azure.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.Azure;

public class AzureFileStore : IDataStore<string, Stream>
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

    public async Task WriteAsync(string identifier, Stream inputStream,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var blob = _container.GetBlobClient(identifier);
        await blob.UploadAsync(inputStream, cancellationToken).ConfigureAwait(false);
        ;
    }

    public async Task<Stream> ReadAsync(string identifier,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var blob = _container.GetBlobClient(identifier);
        var result = await blob.DownloadStreamingAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        ;
        return result.Value.Content;
    }

    public async Task UpdateAsync(string identifier, Stream inputStream,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await WriteAsync(identifier, inputStream, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _container.DeleteBlobAsync(identifier, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}