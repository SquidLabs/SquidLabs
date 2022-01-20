namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public abstract record BaseValueObject : IValueObject<Guid>
{
    /// <summary>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(IDomainObject<Guid>? other)
    {
        return other is not null && GetKey() == other.GetKey();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public abstract Guid GetKey();

    /// <summary>
    /// </summary>
    public Guid Key { get; protected init; }
}