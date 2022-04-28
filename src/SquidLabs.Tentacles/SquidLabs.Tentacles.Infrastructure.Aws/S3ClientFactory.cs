using Amazon.S3;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

public class S3ClientFactory<TDataEntry> : IClientFactory<TDataEntry, IS3StoreOptions<TDataEntry>, IAmazonS3>
    where TDataEntry : IDataEntry
{
    private readonly AmazonS3Config _clientConfig;
    private readonly IOptionsMonitor<IS3StoreOptions<TDataEntry>> _s3StoreOptionsMonitor;

    public S3ClientFactory(IOptionsMonitor<IS3StoreOptions<TDataEntry>> s3StoreOptionsMonitor)
    {
        _s3StoreOptionsMonitor = s3StoreOptionsMonitor;
        _clientConfig = new AmazonS3Config();
    }

    public IS3StoreOptions<TDataEntry> ClientOptions => _s3StoreOptionsMonitor.CurrentValue;

    public IAmazonS3 GetClient()
    {
        // AmazonS3Client is thread safe and sharing is recommended set max connections property
        return new AmazonS3Client(_s3StoreOptionsMonitor.CurrentValue.Credentials?.AccessKey,
            _s3StoreOptionsMonitor.CurrentValue.Credentials.SecretKey, _clientConfig);
    }
}