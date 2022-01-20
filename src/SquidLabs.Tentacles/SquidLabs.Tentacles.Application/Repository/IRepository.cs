using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Application.Repository;

/// <summary>
/// </summary>
/// <typeparam name="TDomainObject"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository<TDomainObject, TKey> where TDomainObject : class, IDomainObject<TKey>
{
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TDomainObject> GetAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task InsertAsync(TDomainObject entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(TDomainObject entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(TDomainObject entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}