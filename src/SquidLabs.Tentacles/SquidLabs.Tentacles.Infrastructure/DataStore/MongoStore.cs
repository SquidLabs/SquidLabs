using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

/// <summary>
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class MongoStore<TKey, TEntity> : IDataStore<TKey, TEntity> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;
    private readonly MongoOptions _options;

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    public MongoStore(IOptionsMonitor<MongoOptions> options)
    {
        _options = options.CurrentValue;
        var database = new MongoClient(_options.ConnectionString).GetDatabase(_options.DatabaseName);
        _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="content"></param>
    public async Task WriteAsync(TKey identifier, TEntity content, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(content, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public async Task<TEntity> ReadAsync(TKey identifier, CancellationToken cancellationToken)
    {
        var query = Builders<TEntity>.Filter.Eq("Id", identifier);
        var entity = (await _collection.FindAsync(query, cancellationToken: cancellationToken)).FirstOrDefault();
        if (entity == null)
            throw new KeyNotFoundException("There is no such an entity with given primary key. Entity type: " +
                                           typeof(TEntity).FullName + ", primary key: " + identifier);
        return entity;
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="entity"></param>
    public async Task UpdateAsync(TKey identifier, TEntity entity, CancellationToken cancellationToken)
    {
        var query = Builders<TEntity>.Filter.Eq("Id", identifier);
        await _collection.ReplaceOneAsync(query, entity, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="identifier"></param>
    public async Task DeleteAsync(TKey identifier, CancellationToken cancellationToken)
    {
        var query = Builders<TEntity>.Filter.Eq("Id", identifier);
        await _collection.DeleteOneAsync(query, cancellationToken);
    }
}