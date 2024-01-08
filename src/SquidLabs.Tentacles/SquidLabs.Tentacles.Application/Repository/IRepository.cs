using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Application.Repository;

/// <summary>
/// </summary>
/// <typeparam name="TDomainObject"></typeparam>
/// <typeparam name="TId"></typeparam>
public interface IRepository<TDomainObject, TId> where TDomainObject : class, IDomainObject<TId>
    where TId : IEquatable<TId>
{
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TDomainObject?> GetAsync(TId id, CancellationToken cancellationToken = default);

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
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
}