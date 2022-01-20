using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.AWS.Options;

public class DynamoStoreOptions : IConnectionStringOptions<Uri>
{
    public Uri ConnectionString { get; set; }
}