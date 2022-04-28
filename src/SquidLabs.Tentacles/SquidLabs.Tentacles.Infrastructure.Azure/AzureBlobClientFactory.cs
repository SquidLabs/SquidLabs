using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Azure;

public class
    AzureBlobClientFactory<TDataEntry> : IClientFactory<TDataEntry, IAzureBlobOptions<TDataEntry>, BlobContainerClient>
    where TDataEntry : IDataEntry
{
    private readonly IOptionsMonitor<IAzureBlobOptions<TDataEntry>> _azureBlobOptionsMonitor;

    public AzureBlobClientFactory(IOptionsMonitor<IAzureBlobOptions<TDataEntry>> azureBlobOptionsMonitor)
    {
        _azureBlobOptionsMonitor = azureBlobOptionsMonitor;
    }

    public IAzureBlobOptions<TDataEntry> ClientOptions => _azureBlobOptionsMonitor.CurrentValue;

    public BlobContainerClient GetClient()
    {
        // TODO refactor to share blob client per connection object as it's meant to be used as a single instance
        // https://docs.microsoft.com/en-us/dotnet/api/overview/azure/storage.blobs-readme-pre#thread-safety
        var currentAzureBlobOptionsMonitor = _azureBlobOptionsMonitor.CurrentValue;
        var config = new BlobClientOptions();
        var container = new BlobContainerClient(currentAzureBlobOptionsMonitor.ConnectionDefinition,
            currentAzureBlobOptionsMonitor.ContainerName, config);
        container.CreateIfNotExists();
        return container;
    }
}