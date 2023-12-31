namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public abstract record ValueObject : IValueObject<Guid>
{
    /// <summary>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(IDomainObject<Guid>? other)
    {
        return other is not null && Id == other.Id;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public abstract Guid GetKey();
    

    public Guid Id { get; }
}