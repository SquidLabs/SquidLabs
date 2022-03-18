using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;
using SquidLabs.Tentacles.Infrastructure.DataStore.Options;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

/// <summary>
/// </summary>
/// <typeparam name="TIdentifier"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TSearchCriteria"></typeparam>
public class ElasticSearchStore<TIdentifier, TEntity, TSearchCriteria>
    : ISearchableDataStore<TIdentifier, TEntity, TSearchCriteria>
    where TSearchCriteria : ISearchRequest
    where TEntity : class, IDataEntry
{
    private readonly ConnectionSettings _connectionSettings;
    private readonly ElasticClient _elasticClient;
    private readonly ElasticSearchOptions _searchOptions;

    public ElasticSearchStore(IOptionsMonitor<ElasticSearchOptions> searchOptionsMonitor)
    {
        _searchOptions = searchOptionsMonitor.CurrentValue;
        _connectionSettings = buildConnectionSettings(searchOptionsMonitor.CurrentValue);
        _elasticClient = new ElasticClient(_connectionSettings);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task WriteAsync(TIdentifier id, TEntity entity, CancellationToken cancellationToken)
    {
        await _elasticClient.IndexDocumentAsync(entity, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TEntity> ReadAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        var resp = await _elasticClient.GetAsync(DocumentPath<TEntity>.Id(id.ToString()),
            ct: cancellationToken);
        return resp.Found ? resp.Source : null;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task UpdateAsync(TIdentifier id, TEntity entity,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await _elasticClient.IndexDocumentAsync(entity, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task DeleteAsync(TIdentifier id,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await _elasticClient.DeleteAsync(DocumentPath<TEntity>.Id(id.ToString()), ct: cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="searchCriteria"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IReadOnlyCollection<TEntity>> SearchAsync(TSearchCriteria searchCriteria,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var response = await _elasticClient.SearchAsync<TEntity>(searchCriteria, cancellationToken);
        return response.Documents;
    }

    private static ConnectionSettings buildConnectionSettings(ElasticSearchOptions searchOptions)
    {
        if (searchOptions.ConnectionString.Length == 0) throw new MissingFieldException();
        return searchOptions.ConnectionString.Length > 1
            ? new ConnectionSettings(new SniffingConnectionPool(searchOptions.ConnectionString))
            : new ConnectionSettings(searchOptions.ConnectionString[0]);
    }
}