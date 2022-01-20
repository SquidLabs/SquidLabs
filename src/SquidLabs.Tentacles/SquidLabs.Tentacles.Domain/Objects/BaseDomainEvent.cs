namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public class BaseDomainEvent : IDomainEvent<Guid>
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
}