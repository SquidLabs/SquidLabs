using SquidLabs.Tentacles.Domain.Events;

namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public class AggregateRoot<TId> : IAggregateRoot<TId> where TId : IEquatable<TId>
{
    private readonly List<IDomainEvent<TId>> _domainEvents = new();

    public virtual void AddDomainEvent(IDomainEvent<TId> domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent<TId> domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public TId Id { get; set; } = default!;

    public bool Equals(IDomainObject<TId>? other)
    {
        return other is IAggregateRoot<TId> && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }
}