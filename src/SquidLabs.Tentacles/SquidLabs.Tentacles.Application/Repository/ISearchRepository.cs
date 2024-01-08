using SquidLabs.Tentacles.Domain.Objects;
using SquidLabs.Tentacles.Domain.Specification;

namespace SquidLabs.Tentacles.Application.Repository;

/// <summary>
/// </summary>
/// <typeparam name="TDomainObject"></typeparam>
/// <typeparam name="TId"></typeparam>
/// <typeparam name="TSpecification"></typeparam>
public interface ISearchRepository<TDomainObject, TId, TSpecification> : IRepository<TDomainObject, TId>
    where TDomainObject : class, IDomainObject<TId>
    where TSpecification : ISpecification
    where TId : IEquatable<TId>
{
    /// <summary>
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<TDomainObject>> SearchAsync(TSpecification specification,
        CancellationToken cancellationToken = default);
}