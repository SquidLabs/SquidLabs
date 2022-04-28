using MongoDB.Driver;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Mongo;

public static class MongoStoreExtensions
{
    public static IMongoCollection<TDataEntry> GetClientCollection<TDataEntry>(
        this IClientFactory<TDataEntry, IMongoOptions<TDataEntry>, IMongoDatabase> factory)
        where TDataEntry : IDataEntry
    {
        return factory.GetClient().GetCollection<TDataEntry>(typeof(TDataEntry).Name);
    }
}