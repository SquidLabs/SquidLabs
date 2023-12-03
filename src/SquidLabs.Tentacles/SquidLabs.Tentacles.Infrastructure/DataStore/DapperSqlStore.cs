using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

public class DapperSqlStore<TIdentifier, TEntry> : IDataStore<TIdentifier, TEntry> 
    where TEntry : IDataEntry
{
    public Task WriteAsync(TIdentifier id, TEntry content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntry> ReadAsync(TIdentifier id, CancellationToken cancellationToken)
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
}