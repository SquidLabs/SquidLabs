using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Tests;

public class
    TestSearchDataStore<TIdentifier, TEntity, TSearchCriteria> : ISearchableDataStore<TIdentifier, TEntity,
        TSearchCriteria>
    where TSearchCriteria : TestSearchCriteria
    where TEntity : TestEntity
{
    private Lazy<ConcurrentDictionary<TIdentifier, TEntity>> _dictionary =
        new Lazy<ConcurrentDictionary<TIdentifier, TEntity>>(() => new ConcurrentDictionary<TIdentifier, TEntity>());

    public async Task WriteAsync(TIdentifier identifier, TEntity content,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.Run(async () => { _dictionary.Value.TryAdd(identifier, content); }, cancellationToken);
    }

    public async Task<TEntity> ReadAsync(TIdentifier identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TIdentifier identifier, TEntity content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TIdentifier identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<TEntity>> SearchAsync(TSearchCriteria searchCriteria,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Task.Run(
            () =>
            {
                return _dictionary.Value.Values.Where(e =>
                    e.LastName?.IndexOf(searchCriteria.LastName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }, cancellationToken);
    }
}