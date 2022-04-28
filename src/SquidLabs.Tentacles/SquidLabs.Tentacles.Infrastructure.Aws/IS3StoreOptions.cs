using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

/// <summary>
/// </summary>
public interface IS3StoreOptions<TDataEntry> : IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
    /// <summary>
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    /// </summary>
    public IAwsCredentials Credentials { get; set; }
}