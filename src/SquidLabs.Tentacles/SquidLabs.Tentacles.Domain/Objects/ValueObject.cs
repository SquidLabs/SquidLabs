namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public abstract record ValueObject<TId> : IValueObject<TId> where TId : IEquatable<TId>
{
    public virtual bool Equals(IDomainObject<TId>? other)
    {
        return other is IValueObject<TId> && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public TId Id { get; init; } = default!;
    public abstract TId GetValueHashCode();
}