using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.AWS.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.AWS;

public class DynamoStore<TKey, TEntity> : IDataStore<TKey, TEntity>
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

    public async Task WriteAsync(TKey identifier, TEntity entity, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        var json = JsonSerializer.Serialize(entity);
        var doc = Document.FromJson(json);
        await table.PutItemAsync(doc, cancellationToken);
    }

    public async Task<TEntity> ReadAsync(TKey identifier, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        var response =
            await table.GetItemAsync(new Dictionary<string, DynamoDBEntry> { { "Id", identifier.ToString() } },
                cancellationToken);
        return JsonSerializer.Deserialize<TEntity>(response.ToJson());
    }

    public async Task UpdateAsync(TKey identifier, TEntity entity, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        var json = JsonSerializer.Serialize(entity);
        var doc = Document.FromJson(json);
        await table.UpdateItemAsync(doc, cancellationToken);
    }

    public async Task DeleteAsync(TKey identifier, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_client, _tableName);
        await table.DeleteItemAsync(new Dictionary<string, DynamoDBEntry> { { "Id", identifier.ToString() } },
            cancellationToken);
    }
}