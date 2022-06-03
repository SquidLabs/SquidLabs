using SquidLabs.Tentacles.Application.Mapper;
using SquidLabs.Tentacles.Domain.Objects;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Application.Repository;

// TODO: need something more advanced look at automapper and figure something out that is clean.
// TODO: Factory to generate a mapper?
// TODO: Retry Policy
// TODO: Locking
// TODO: Versioning
public abstract class BaseRepository<TDomainObject, TKey> : IRepository<TDomainObject, TKey>
    where TDomainObject : class, IDomainObject<TKey>
{
    private readonly IDataStore<TKey, IDataEntry> _dataStore;
    private readonly IMapper<TKey, TDomainObject, IDataEntry> _mapper;

    /// <summary>
    /// </summary>
    /// <param name="dataStore"></param>
    /// <param name="mapper"></param>
    protected BaseRepository(IDataStore<TKey, IDataEntry> dataStore, IMapper<TKey, TDomainObject, IDataEntry> mapper)
    {
        _dataStore = dataStore;
        _mapper = mapper;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TDomainObject?> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var response = await _dataStore.ReadAsync(id, cancellationToken).ConfigureAwait(false);
        return _mapper.ConvertFromDataStoreType(response);
    }

    /// <summary>
    /// </summary>
    /// <param name="domainObject"></param>
    /// <param name="cancellationToken"></param>
    public async Task InsertAsync(TDomainObject domainObject,
        CancellationToken cancellationToken = default)
    {
        var record = _mapper.ConvertToDataStoreType(domainObject);
        if (record != null)
            await _dataStore.WriteAsync(domainObject.GetKey(), record, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="domainObject"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(TDomainObject domainObject,
        CancellationToken cancellationToken = default)
    {
        var record = _mapper.ConvertToDataStoreType(domainObject);
        if (record != null)
            await _dataStore.UpdateAsync(domainObject.GetKey(), record, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="domainObject"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TDomainObject domainObject,
        CancellationToken cancellationToken = default)
    {
        await _dataStore.DeleteAsync(domainObject.GetKey(), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        await _dataStore.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
    }
}