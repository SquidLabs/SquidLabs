namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public class BaseAggregateRoot : IAggregateRoot<Guid>
{
    /// <summary>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(IDomainObject<Guid>? other)
    {
        return other is not null && GetKey() == other.GetKey();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public Guid GetKey()
    {
        return Id;
    }

    /// <summary>
    /// </summary>
    public Guid Id { get; set; }
}