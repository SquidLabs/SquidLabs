using System;
using SquidLabs.Tentacles.Infrastructure.Aws;
using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests;

public class TestAWsS3Options : IS3StoreOptions<TestFileEntry>
{
    public string BucketName { get; set; } = null!;

    public IAwsCredentials Credentials { get; set; } = null!;
}