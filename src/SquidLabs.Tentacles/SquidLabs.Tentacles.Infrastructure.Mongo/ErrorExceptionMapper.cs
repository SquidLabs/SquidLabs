using MongoDB.Driver;
using SquidLabs.Tentacles.Infrastructure.Exceptions;

namespace SquidLabs.Tentacles.Infrastructure.Mongo;

public static class ErrorExceptionMapper
{
    public static StoreException ToStoreException(this Exception exception)
    {
        return exception switch
        {
            InvalidOperationException => new StoreEntryNotFoundException("Mongo record not found", exception),
            MongoWriteException writeException => writeException.ToStoreException(),
            _ => new StoreException("Untranslated mongo error", exception)
        };
    }

    private static StoreException ToStoreException(this MongoWriteException exception)
    {
        return exception.WriteError.Category switch
        {
            ServerErrorCategory.DuplicateKey => new StoreDuplicateIdentifierException(
                "Duplicate Identified in Mongo Exception", exception),
            ServerErrorCategory.ExecutionTimeout => new StoreOperationException("Mongo Operation Timeout", exception),
            _ => new StoreException("Unknown Mongo Write exception", exception)
        };
    }
}