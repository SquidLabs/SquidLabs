using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Events;

/// <summary>
/// </summary>
public class DomainEvent : IDomainEvent<Guid>
{
    /// <summary>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(IDomainObject<Guid>? other)
    {
        return other is not null && GetKey().Equals(other.GetKey());
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public Guid GetKey()
    {
        return CorrelationId;
    }

    /// <summary>
    /// </summary>
    public Guid CorrelationId { get; init; }

    public Guid Id { get; set; }
}