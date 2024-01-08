using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Events;

/// <summary>
/// </summary>
public interface IDomainEvent<TCorrelationId> : IDomainObject<TCorrelationId>
    where TCorrelationId : IEquatable<TCorrelationId>
{
    /// <summary>
    ///     Unique identifier of the event
    /// </summary>
    public TCorrelationId CorrelationId { get; init; }
}