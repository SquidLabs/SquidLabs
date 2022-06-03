using System.Net;
using Amazon.S3;
using SquidLabs.Tentacles.Infrastructure.Exceptions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

public static class ErrorExceptionMapper
{
    public static Exception ToStoreException(this AmazonS3Exception exception)
    {
        return exception.StatusCode switch
        {
            HttpStatusCode.NotFound => new StoreEntryNotFoundException("S3 file not found", exception),
            _ => new StoreException("Untranslated S3 error", exception)
        };
    }
}