using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Tests;

public class
    TestSearchDataStore<TIdentifier, TEntry, TSearchCriteria> : ISearchableDataStore<TIdentifier, TEntry,
        TSearchCriteria>
    where TSearchCriteria : TestSearchCriteria
    where TEntry : TestEntry, IDataEntry
{
    private readonly Lazy<ConcurrentDictionary<TIdentifier, TEntry>> _dictionary =
        new Lazy<ConcurrentDictionary<TIdentifier, TEntry>>(() => new ConcurrentDictionary<TIdentifier, TEntry>());

    public async Task WriteAsync(TIdentifier id, TEntry content,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.Run(async () => { _dictionary.Value.TryAdd(id, content); }, cancellationToken);
    }

    public async Task<TEntry> ReadAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TIdentifier id, TEntry content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<TEntry>> SearchAsync(TSearchCriteria searchCriteria,
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