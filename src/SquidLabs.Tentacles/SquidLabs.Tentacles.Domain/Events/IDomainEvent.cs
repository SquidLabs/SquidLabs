using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Events;

/// <summary>
/// </summary>
public interface IDomainEvent<TCorrelationId> : IDomainObject<TCorrelationId>
{
    /// <summary>
    /// </summary>
    public TCorrelationId CorrelationId { get; init; }
}