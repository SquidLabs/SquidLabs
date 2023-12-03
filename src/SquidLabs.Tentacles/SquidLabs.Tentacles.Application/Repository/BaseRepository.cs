using SquidLabs.Tentacles.Application.Mapper;
using SquidLabs.Tentacles.Domain.Objects;
using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Application.Repository;

public abstract class BaseRepository<TDomainObject, TKey> : IRepository<TDomainObject, TKey>
    where TDomainObject : class, IDomainObject<TKey>
{
    private IDataStore<TKey, IDataEntry> _dataStore;

    // TODO: need something more advanced look at automapper and figure something out that is clean.
    // TODO: Factory to generate a mapper?
    // TODO: Retry Policy
    // TODO: Locking
    // TODO: Versioning
    private IMapper<TKey, TDomainObject, IDataEntry> _mapper;
    
    public async Task<TDomainObject> GetAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken))
    {
        var response = await _dataStore.ReadAsync(id, cancellationToken).ConfigureAwait(false);
        return _mapper.ConvertFromDataStoreType(response);
    }

    public async Task InsertAsync(TDomainObject domainObject,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var record = _mapper.ConvertToDataStoreType(domainObject);
        await _dataStore.WriteAsync(domainObject.GetKey(), record, cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(TDomainObject domainObject,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var record = _mapper.ConvertToDataStoreType(domainObject);
        await _dataStore.UpdateAsync(domainObject.GetKey(), record, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(TDomainObject domainObject,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await _dataStore.DeleteAsync(domainObject.GetKey(), cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        await _dataStore.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
    }
}