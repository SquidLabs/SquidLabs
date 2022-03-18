using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.AWS.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.AWS;

public class DynamoStore<TIdentifier, TEntry> : IDataStore<TIdentifier, TEntry> 
    where TEntry : IDataEntry
{
    private readonly AmazonDynamoDBClient _client;
    private readonly DynamoStoreOptions _dynamoStoreOptions;
    private readonly AwsSecretOptions _secretOptions;
    private readonly string _tableName;

    public DynamoStore(IOptionsMonitor<AwsSecretOptions> secretOptions,
        IOptionsMonitor<DynamoStoreOptions> dynamoStoreOptions)
    {
        _tableName = "test";
        _dynamoStoreOptions = dynamoStoreOptions.CurrentValue;
        _secretOptions = secretOptions.CurrentValue;
        _client = new AmazonDynamoDBClient(_secretOptions.AccessKey, _secretOptions.SecretKey);
    }

    public async Task WriteAsync(TIdentifier id, TEntry entity, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        var json = JsonSerializer.Serialize(entity);
        var doc = Document.FromJson(json);
        await table.PutItemAsync(doc, cancellationToken);
    }

    public async Task<TEntry> ReadAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        var response =
            await table.GetItemAsync(new Dictionary<string, DynamoDBEntry> { { "Id", id.ToString() } },
                cancellationToken);
        return JsonSerializer.Deserialize<TEntry>(response.ToJson());
    }

    public async Task UpdateAsync(TIdentifier id, TEntry entity, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        var json = JsonSerializer.Serialize(entity);
        var doc = Document.FromJson(json);
        await table.UpdateItemAsync(doc, cancellationToken);
    }

    public async Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        await table.DeleteItemAsync(new Dictionary<string, DynamoDBEntry> { { "Id", id.ToString() } },
            cancellationToken);
    }
}