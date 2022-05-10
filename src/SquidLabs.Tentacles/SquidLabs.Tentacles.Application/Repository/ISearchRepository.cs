using SquidLabs.Tentacles.Domain.Objects;
using SquidLabs.Tentacles.Domain.Specification;

namespace SquidLabs.Tentacles.Application.Repository;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TDomainObject"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSpecification"></typeparam>
public interface ISearchRepository<TDomainObject, TKey, TSpecification> : IRepository<TDomainObject, TKey>
    where TDomainObject : class, IDomainObject<TKey>
    where TSpecification : ISpecification
{
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<TDomainObject>> SearchAsync(TSpecification specification,
        CancellationToken cancellationToken = default);
}