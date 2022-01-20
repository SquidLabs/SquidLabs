namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public interface IDomainEvent<TCorrelationId> : IDomainObject<TCorrelationId>
{
    /// <summary>
    /// </summary>
    public TCorrelationId CorrelationId { get; init; }
}