using Amazon.S3;
using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Aws.IntegrationTests;

public class TestAwsS3Options : IS3StoreOptions<TestFileEntry>
{
    public AmazonS3Config Config { get; init; } = null!;
    public string BucketName { get; set; } = null!;
    public string ServiceUrl { get; set; } = null!;
    public bool UseHttp { get; set; } = true;

    public bool ForcePathStyle { get; set; } = true;
    public IAwsCredentials Credentials { get; set; } = null!;
}

public class AwsCredentials : IAwsCredentials
{
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string Token { get; set; } = null!;
}