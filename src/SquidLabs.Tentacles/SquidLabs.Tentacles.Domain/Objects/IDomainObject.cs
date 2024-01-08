namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
/// <typeparam name="TId"></typeparam>
public interface IDomainObject<TId> : IEquatable<IDomainObject<TId>> where TId : IEquatable<TId>
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public TId Id { get; }
}