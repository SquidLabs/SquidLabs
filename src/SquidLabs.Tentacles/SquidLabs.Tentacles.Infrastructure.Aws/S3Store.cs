using Amazon.S3;
using Amazon.S3.Model;
using SquidLabs.Tentacles.Infrastructure.Abstractions;
using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

/// <summary>
/// </summary>
public class S3Store<TFileEntry> : IFileStore<Guid, TFileEntry> where TFileEntry : class, IFileEntry
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
    public async Task WriteAsync(Guid id, TFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        await using var inputStream = await fileEntry.ToStreamAsync(cancellationToken);
        var request = new PutObjectRequest
        {
            InputStream = inputStream,
            BucketName = _clientFactory.ClientOptions.BucketName,
            Key = id.ToString()
        };

        try
        {
            await _clientFactory.GetClient().PutObjectAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (AmazonS3Exception amazonS3Exception)
        {
            throw amazonS3Exception.ToStoreException();
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TFileEntry?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var client = _clientFactory.GetClient();

        try
        {
            await using var fileStream = new FileStream(id.ToString(), FileMode.Create);
            await using var response = await client.GetObjectStreamAsync(_clientFactory.ClientOptions.BucketName,
                    id.ToString(), null, cancellationToken)
                .ConfigureAwait(false);
            await response.CopyToAsync(fileStream, cancellationToken);
            return new TestFileEntry(id.ToString()) as TFileEntry;
        }
        catch (AmazonS3Exception amazonS3Exception)
        {
            throw amazonS3Exception.ToStoreException();
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileEntry"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(Guid id, TFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        await WriteAsync(id, fileEntry, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var client = _clientFactory.GetClient();

        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _clientFactory.ClientOptions.BucketName,
            Key = id.ToString()
        };

        try
        {
            await client.DeleteObjectAsync(deleteObjectRequest, cancellationToken).ConfigureAwait(false);
        }
        catch (AmazonS3Exception amazonS3Exception)
        {
            throw amazonS3Exception.ToStoreException();
        }
    }
}