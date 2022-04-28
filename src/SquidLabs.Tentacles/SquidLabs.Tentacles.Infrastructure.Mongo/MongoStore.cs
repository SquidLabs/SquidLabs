using MongoDB.Driver;
using SquidLabs.Tentacles.Infrastructure.Abstractions;
using SquidLabs.Tentacles.Infrastructure.Exceptions;

namespace SquidLabs.Tentacles.Infrastructure.Mongo;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TDataEntry"></typeparam>
public class MongoStore<TIdentifier, TDataEntry> : IDataStore<TIdentifier, TDataEntry>
    where TDataEntry : class, IDataEntry
{
    /// <summary>
    /// </summary>
    private readonly IClientFactory<TDataEntry, IMongoOptions<TDataEntry>, IMongoDatabase> _clientFactory;

    /// <summary>
    /// </summary>
    /// <param name="clientFactory"></param>
    public MongoStore(IClientFactory<TDataEntry, IMongoOptions<TDataEntry>, IMongoDatabase> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(TIdentifier id, TDataEntry content, CancellationToken cancellationToken = default)
    {
        var client = _clientFactory.GetClientCollection();

        try
        {
            await client.InsertOneAsync(content, null, cancellationToken).ConfigureAwait(false);
        }
        catch (MongoWriteException ex)
        {
            if (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
                throw new DuplicateIdentifierException(ex.Message, ex);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TDataEntry?> ReadAsync(TIdentifier id, CancellationToken cancellationToken = default)
    {
        var query = Builders<TDataEntry>.Filter.Eq("Id", id);
        return await (await _clientFactory.GetClientCollection().FindAsync(query, cancellationToken: cancellationToken)
            .ConfigureAwait(false)).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(TIdentifier id, TDataEntry entity, CancellationToken cancellationToken = default)
    {
        var query = Builders<TDataEntry>.Filter.Eq("Id", id);
        var result = await _clientFactory.GetClientCollection()
            .ReplaceOneAsync(query, entity, new ReplaceOptions { IsUpsert = false }, cancellationToken)
            .ConfigureAwait(false);
        if (result.ModifiedCount == 0)
            throw new DataStoreEntryNotFound("Could not find record to replace", DataStoreOperationTypeEnum.Update);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default)
    {
        var query = Builders<TDataEntry>.Filter.Eq("Id", id);
        var result = await _clientFactory.GetClientCollection()
            .DeleteOneAsync(query, new DeleteOptions(), cancellationToken).ConfigureAwait(false);
        if (result.DeletedCount == 0)
            throw new DataStoreEntryNotFound("Could not find record to replace", DataStoreOperationTypeEnum.Delete);
    }
}