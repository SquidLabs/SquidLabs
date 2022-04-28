using Amazon.S3;
using Amazon.S3.Model;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

/// <summary>
/// </summary>
public class S3Store<TFileEntry> : IFileStore<string, TFileEntry> where TFileEntry : IFileEntry
{
    /// <summary>
    /// </summary>
    private readonly IClientFactory<TFileEntry, IS3StoreOptions<TFileEntry>, IAmazonS3> _clientFactory;

    /// <summary>
    /// </summary>
    /// <param name="clientFactory"></param>
    public S3Store(IClientFactory<TFileEntry, IS3StoreOptions<TFileEntry>, IAmazonS3> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileEntry"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(string id, TFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        var request = new PutObjectRequest
        {
            InputStream = await fileEntry.ToStreamAsync(),
            BucketName = _clientFactory.ClientOptions.BucketName,
            Key = id
        };

        await _clientFactory.GetClient().PutObjectAsync(request, cancellationToken).ConfigureAwait(false);
        ;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TFileEntry?> ReadAsync(string id, CancellationToken cancellationToken = default)
    {
        var request = new GetObjectRequest
        {
            BucketName = _clientFactory.ClientOptions.BucketName,
            Key = id
        };

        using var response = await _clientFactory.GetClient().GetObjectAsync(request, cancellationToken)
            .ConfigureAwait(false);
        ;
        return (TFileEntry?)await TFileEntry.FromStreamAsync(response.ResponseStream);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileEntry"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(string id, TFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        await WriteAsync(id, fileEntry, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _clientFactory.ClientOptions.BucketName,
            Key = id
        };

        await _clientFactory.GetClient().DeleteObjectAsync(deleteObjectRequest, cancellationToken)
            .ConfigureAwait(false);
        ;
    }
}