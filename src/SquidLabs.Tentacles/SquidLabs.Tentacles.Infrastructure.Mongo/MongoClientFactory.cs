using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SquidLabs.Tentacles.Infrastructure.Abstractions;
using SquidLabs.Tentacles.Infrastructure.Exceptions;

namespace SquidLabs.Tentacles.Infrastructure.Mongo;

public class MongoClientFactory<TDataEntry> : IClientFactory<TDataEntry, IMongoOptions<TDataEntry>, IMongoDatabase>
    where TDataEntry : IDataEntry
{
    private readonly IOptionsMonitor<IMongoOptions<TDataEntry>> _mongoOptionsMonitor;

    public MongoClientFactory(IOptionsMonitor<IMongoOptions<TDataEntry>> mongoOptionsMonitor)
    {
        _mongoOptionsMonitor = mongoOptionsMonitor;
    }

    public IMongoOptions<TDataEntry> ClientOptions => _mongoOptionsMonitor.CurrentValue;

    public IMongoDatabase GetClient()
    {
        var currentOptions = _mongoOptionsMonitor.CurrentValue;

        try
        {
            return new MongoClient(currentOptions.ConnectionDefinition).GetDatabase(currentOptions.DatabaseName);
        }
        catch (ArgumentNullException ex)
        {
            throw new StoreClientFactoryArgumentNullException(ex.Message, ex);
        }
    }
}