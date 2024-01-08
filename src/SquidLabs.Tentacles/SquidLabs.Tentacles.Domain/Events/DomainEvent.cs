using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Events;

/// <summary>
/// </summary>
public class DomainEvent<TId> : IDomainEvent<TId> where TId : IEquatable<TId>
{
    /// <summary>
    ///     Unqiue identifier of the event
    /// </summary>
    public required TId CorrelationId { get; init; }

    /// <summary>
    ///     identifier for the source domain object
    /// </summary>
    public required TId Id { get; set; }

    /// <summary>
    ///     Equality comparer for the domain event, based on the CorrelationId
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(IDomainObject<TId>? other)
    {
        return other is IDomainEvent<TId> domainEvent &&
               EqualityComparer<TId>.Default.Equals(CorrelationId, domainEvent.CorrelationId);
    }
}