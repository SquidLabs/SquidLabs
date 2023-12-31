using Nest;
using SquidLabs.Tentacles.Infrastructure.Abstractions;
using SquidLabs.Tentacles.Infrastructure.Exceptions;

namespace SquidLabs.Tentacles.Infrastructure.Elastic;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TDataEntry"></typeparam>
/// <typeparam name="TSearchCriteria"></typeparam>
public class ElasticSearchStore<TIdentifier, TDataEntry, TSearchCriteria>
    : ISearchableDataStore<TIdentifier, TDataEntry, TSearchCriteria>
    where TSearchCriteria : ISearchRequest
    where TDataEntry : class, IDataEntry
{
    private readonly IClientFactory<TDataEntry, IElasticSearchOptions<TDataEntry>, IElasticClient> _clientFactory;

    public ElasticSearchStore(
        IClientFactory<TDataEntry, IElasticSearchOptions<TDataEntry>, IElasticClient> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public async Task WriteAsync(TIdentifier id, TDataEntry entity, CancellationToken cancellationToken = default)
    {
        var client = _clientFactory.GetClient();
        var response = await client.IndexDocumentAsync(entity, cancellationToken).ConfigureAwait(false);
        if (response.ServerError is not null) throw response.ServerError.Error.ToStoreException();
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TDataEntry?> ReadAsync(TIdentifier id, CancellationToken cancellationToken = default)
    {
        var client = _clientFactory.GetClient();
        var resp = await client.GetAsync(DocumentPath<TDataEntry>.Id(id!.ToString()),
            ct: cancellationToken).ConfigureAwait(false);

        if (!resp.Found)
            throw new StoreEntryNotFoundException("Could not find Entry in Elastic", resp.ApiCall.OriginalException);

        return resp.Source;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(TIdentifier id, TDataEntry entity,
        CancellationToken cancellationToken = default)
    {
        var client = _clientFactory.GetClient();
        var response = await client.UpdateAsync<TDataEntry, object>(DocumentPath<TDataEntry>.Id(entity),
            i => i.Index(_clientFactory.ClientOptions.IndexField.ToLower()).Doc(entity), cancellationToken);
        if (response.ServerError is not null) throw response.ServerError.Error.ToStoreException();
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TIdentifier id,
        CancellationToken cancellationToken = default)
    {
        var client = _clientFactory.GetClient();
        var response = await client.DeleteAsync(DocumentPath<TDataEntry>.Id(id!.ToString()), ct: cancellationToken)
            .ConfigureAwait(false);
        if (response.ServerError is not null) throw response.ServerError.Error.ToStoreException();
    }

    /// <summary>
    /// </summary>
    /// <param name="searchCriteria"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<TDataEntry>> SearchAsync(TSearchCriteria searchCriteria,
        CancellationToken cancellationToken = default)
    {
        var client = _clientFactory.GetClient();
        var response = await client.SearchAsync<TDataEntry>(searchCriteria, cancellationToken)
            .ConfigureAwait(false);
        if (response.ServerError is not null) throw response.ServerError.Error.ToStoreException();
        return response.Documents;
    }
}