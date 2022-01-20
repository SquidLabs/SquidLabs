using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore;

public class MicrosoftSqlStore<TKey, TType> : IDataStore<TKey, TType>
{
    public Task WriteAsync(TKey identifier, TType content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TType> ReadAsync(TKey identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TKey identifier, TType content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TKey identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}