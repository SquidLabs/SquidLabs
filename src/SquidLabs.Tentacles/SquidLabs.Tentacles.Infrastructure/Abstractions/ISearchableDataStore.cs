namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

/// <summary>
///     An additional interface for data stores that provide search/query functionality
/// </summary>
/// <typeparam name="TIdentifier">Identity field for the underlying data store</typeparam>
/// <typeparam name="TEntry">Data to store under the specified Id</typeparam>
/// <typeparam name="TSearchCriteria">Search criteria in the underlying store/library format</typeparam>
public interface ISearchableDataStore<TIdentifier, TEntry, in TSearchCriteria> : IDataStore<TIdentifier, TEntry>
    where TEntry : IDataEntry
{
    /// <summary>
    /// </summary>
    /// <param name="searchCriteria"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<TEntry>> SearchAsync(TSearchCriteria searchCriteria, CancellationToken cancellationToken);
}