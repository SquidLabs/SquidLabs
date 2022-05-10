using Elasticsearch.Net;
using SquidLabs.Tentacles.Infrastructure.Exceptions;

namespace SquidLabs.Tentacles.Infrastructure.Elastic;

public static class ErrorExceptionMapper
{
    public static Exception ToException(this Error error)
    {
        return error switch
        {
            { Type: "document_missing_exception" } => new DataStoreEntryNotFound(error.ToString(),
                DataStoreOperationTypeEnum.Update),
            _ => new DataStoreException(error.ToString())
        };
    }
}