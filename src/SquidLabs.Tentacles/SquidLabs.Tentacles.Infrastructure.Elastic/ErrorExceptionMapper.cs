using Elasticsearch.Net;
using SquidLabs.Tentacles.Infrastructure.Exceptions;

namespace SquidLabs.Tentacles.Infrastructure.Elastic;

public static class ErrorExceptionMapper
{
    public static Exception ToStoreException(this Error error)
    {
        return error switch
        {
            { Type: "document_missing_exception" } => new StoreEntryNotFoundException(error.ToString(),
                StoreOperationTypeEnum.Update),
            _ => new StoreException(error.ToString())
        };
    }
}