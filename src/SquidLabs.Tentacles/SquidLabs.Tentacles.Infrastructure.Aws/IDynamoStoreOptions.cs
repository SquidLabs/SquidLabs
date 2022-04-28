using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

public interface IDynamoStoreOptions<TDataEntry> : IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
    public Uri ConnectionDefinition { get; set; }
    public IAwsCredentials Credentials { get; set; }
}