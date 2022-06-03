using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

/// <summary>
/// </summary>
public interface IS3StoreOptions<TDataEntry> : IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
    public string BucketName { get; set; }
    public string ServiceUrl { get; set; }
    public bool UseHttp { get; set; }
    public bool ForcePathStyle { get; set; }
    public IAwsCredentials Credentials { get; set; }
}