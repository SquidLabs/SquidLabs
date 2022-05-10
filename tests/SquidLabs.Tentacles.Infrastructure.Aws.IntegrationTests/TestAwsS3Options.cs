using Amazon;
using Amazon.S3;
using SquidLabs.Tentacles.Infrastructure.Aws;
using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests;

public class TestAWsS3Options : IS3StoreOptions<TestFileEntry>
{
    public AmazonS3Config Config { get; init; } = null!;
    public string BucketName { get; set; } = null!;
    public string ServiceURL { get; set; } = null!;
    public IAwsCredentials Credentials { get; set; } = null!;
}