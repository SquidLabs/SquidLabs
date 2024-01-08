using SquidLabs.Tentacles.Application.Mapper;
using SquidLabs.Tentacles.Domain.Objects;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Application.Repository;

// TODO: need something more advanced look at automapper and figure something out that is clean.
// TODO: Factory to generate a mapper?
// TODO: Retry Policy
// TODO: Locking
// TODO: Versioning
public abstract class BaseRepository<TDomainObject, TId> : IRepository<TDomainObject, TId>
    where TDomainObject : class, IDomainObject<TId> where TId : IEquatable<TId>
{
    private readonly IDataStore<TId, IDataEntry> _dataStore;
    private readonly IMapper<TId, TDomainObject, IDataEntry> _mapper;

    /// <summary>
    /// </summary>
    /// <param name="dataStore"></param>
    /// <param name="mapper"></param>
    protected BaseRepository(IDataStore<TId, IDataEntry> dataStore, IMapper<TId, TDomainObject, IDataEntry> mapper)
    {
        _dataStore = dataStore;
        _mapper = mapper;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TDomainObject?> GetAsync(TId id, CancellationToken cancellationToken = default)
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
            await _dataStore.WriteAsync(domainObject.Id, record, cancellationToken).ConfigureAwait(false);
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
            await _dataStore.UpdateAsync(domainObject.Id, record, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="domainObject"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TDomainObject domainObject,
        CancellationToken cancellationToken = default)
    {
        await _dataStore.DeleteAsync(domainObject.Id, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        await _dataStore.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
    }
}