using Azure.Storage.Blobs;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Azure;

public static class AzureBlobClientFactoryExtensions
{
    public static BlobClient GetBlobClient<TDataEntry>(
        this IClientFactory<TDataEntry, IAzureBlobOptions<TDataEntry>, BlobContainerClient> factory,
        string id) where TDataEntry : IDataEntry
    {
        return factory.GetClient().GetBlobClient(id);
    }
}