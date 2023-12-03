using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TEntry"></typeparam>
public class MongoStore<TIdentifier, TEntry> : IDataStore<TIdentifier, TEntry> 
    where TEntry : class, IDataEntry
{
    private readonly IMongoCollection<TEntry> _collection;
    private readonly MongoOptions _options;

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    public MongoStore(IOptionsMonitor<MongoOptions> options)
    {
        _options = options.CurrentValue;
        var database = new MongoClient(_options.ConnectionString).GetDatabase(_options.DatabaseName);
        _collection = database.GetCollection<TEntry>(typeof(TEntry).Name);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    public async Task WriteAsync(TIdentifier id, TEntry content, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(content, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public async Task<TEntry> ReadAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        var query = Builders<TEntry>.Filter.Eq("Id", id);
        var entity = (await _collection.FindAsync(query, cancellationToken: cancellationToken)).FirstOrDefault();
        if (entity == null)
            throw new KeyNotFoundException("There is no such an entity with given primary key. Entity type: " +
                                           typeof(TEntry).FullName + ", primary key: " + id);
        return entity;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    public async Task UpdateAsync(TIdentifier id, TEntry entity, CancellationToken cancellationToken)
    {
        var query = Builders<TEntry>.Filter.Eq("Id", id);
        await _collection.ReplaceOneAsync(query, entity, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        var query = Builders<TEntry>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(query, cancellationToken);
    }
}