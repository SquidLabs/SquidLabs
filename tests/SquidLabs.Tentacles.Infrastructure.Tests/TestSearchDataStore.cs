using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Tests;

public class
    TestSearchDataStore<TIdentifier, TDataEntry, TSearchCriteria> : ISearchableDataStore<TIdentifier, TDataEntry,
        TSearchCriteria>
    where TSearchCriteria : TestSearchCriteria
    where TDataEntry : TestDataEntry, IDataEntry
    where TIdentifier : notnull
{
    private readonly Lazy<ConcurrentDictionary<TIdentifier, TDataEntry>> _dictionary =
        new(() => new ConcurrentDictionary<TIdentifier, TDataEntry>());

    public async Task WriteAsync(TIdentifier id, TDataEntry content,
        CancellationToken cancellationToken = default)
    {
        await Task.Run(() => { _dictionary.Value.TryAdd(id, content); }, cancellationToken);
    }

    public async Task<TDataEntry?> ReadAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TIdentifier id, TDataEntry content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<TDataEntry>> SearchAsync(TSearchCriteria searchCriteria,
        CancellationToken cancellationToken = default)
    {
        return await Task.Run(
            () =>
            {
                return _dictionary.Value.Values.Where(e =>
                    e.LastName?.IndexOf(searchCriteria.LastName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }, cancellationToken);
    }
}