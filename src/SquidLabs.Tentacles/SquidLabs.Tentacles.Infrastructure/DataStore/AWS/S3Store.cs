using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.AWS.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.AWS;

public class S3Store : IFileStore<string, Stream>
{
    private readonly AmazonS3Client _client;
    private readonly AmazonS3Config _config;
    private readonly S3StoreOptions _options;

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    /// <param name="secrets"></param>
    public S3Store(IOptionsMonitor<S3StoreOptions> options, IOptionsMonitor<AwsSecretOptions> secrets)
    {
        _options = options.CurrentValue;
        _config = new AmazonS3Config();
        _client = new AmazonS3Client(secrets.CurrentValue.AccessKey, secrets.CurrentValue.SecretKey, _config);
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="inputStream"></param>
    public async Task WriteAsync(string identifier, Stream inputStream, CancellationToken cancellationToken)
    {
        var request = new PutObjectRequest
        {
            InputStream = inputStream,
            BucketName = _options.BucketName,
            Key = identifier
        };

        await _client.PutObjectAsync(request, cancellationToken).ConfigureAwait(false);
        ;
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public async Task<Stream> ReadAsync(string identifier, CancellationToken cancellationToken)
    {
        var request = new GetObjectRequest
        {
            BucketName = _options.BucketName,
            Key = identifier
        };

        using var response = await _client.GetObjectAsync(request, cancellationToken).ConfigureAwait(false);
        ;
        return response.ResponseStream;
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="inputStream"></param>
    public async Task UpdateAsync(string identifier, Stream inputStream, CancellationToken cancellationToken)
    {
        await WriteAsync(identifier, inputStream, cancellationToken).ConfigureAwait(false);
        ;
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    public async Task DeleteAsync(string identifier, CancellationToken cancellationToken)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _options.BucketName,
            Key = identifier
        };

        await _client.DeleteObjectAsync(deleteObjectRequest, cancellationToken).ConfigureAwait(false);
        ;
    }
}