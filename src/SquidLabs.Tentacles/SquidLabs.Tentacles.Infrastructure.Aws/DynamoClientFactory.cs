using Amazon.DynamoDBv2;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

public class
    DynamoClientFactory<TDataEntry> : IClientFactory<TDataEntry, IDynamoStoreOptions<TDataEntry>, IAmazonDynamoDB>
    where TDataEntry : IDataEntry
{
    private readonly IOptionsMonitor<IDynamoStoreOptions<TDataEntry>> _dynamoStoreOptionsMonitor;

    public DynamoClientFactory(IOptionsMonitor<IDynamoStoreOptions<TDataEntry>> dynamoStoreOptionsMonitor)
    {
        _dynamoStoreOptionsMonitor = dynamoStoreOptionsMonitor;
    }

    public IDynamoStoreOptions<TDataEntry> ClientOptions => _dynamoStoreOptionsMonitor.CurrentValue;

    public IAmazonDynamoDB GetClient()
    {
        //TODO AmazonDynamoDBClient and mapper are thread-safe, can probably be used as a single instance per connection
        return new AmazonDynamoDBClient(ClientOptions.Credentials.AccessKey, ClientOptions.Credentials.SecretKey);
    }
}