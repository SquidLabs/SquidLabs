using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Aws;

public class DynamoStore<TIdentifier, TDataEntry> : IDataStore<TIdentifier, TDataEntry>
    where TDataEntry : IDataEntry
{
    private readonly IClientFactory<TDataEntry, IDynamoStoreOptions<TDataEntry>, IAmazonDynamoDB> _clientFactory;

    public DynamoStore(IClientFactory<TDataEntry, IDynamoStoreOptions<TDataEntry>, IAmazonDynamoDB> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task WriteAsync(TIdentifier id, TDataEntry entity, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_clientFactory.GetClient(), nameof(TDataEntry));
        var json = JsonSerializer.Serialize(entity);
        var doc = Document.FromJson(json);
        await table.PutItemAsync(doc, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TDataEntry?> ReadAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_clientFactory.GetClient(), nameof(TDataEntry));
        var response =
            await table.GetItemAsync(new Dictionary<string, DynamoDBEntry> { { "Id", id!.ToString() } },
                cancellationToken).ConfigureAwait(false);
        return JsonSerializer.Deserialize<TDataEntry>(response.ToJson());
    }

    public async Task UpdateAsync(TIdentifier id, TDataEntry entity, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_clientFactory.GetClient(), nameof(TDataEntry));
        var json = JsonSerializer.Serialize(entity);
        var doc = Document.FromJson(json);
        await table.UpdateItemAsync(doc, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        var table = Table.LoadTable(_clientFactory.GetClient(), nameof(TDataEntry));
        await table.DeleteItemAsync(new Dictionary<string, DynamoDBEntry> { { "Id", id!.ToString() } },
            cancellationToken).ConfigureAwait(false);
    }
}