using SquidLabs.Tentacles.Domain.Events;

namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
public abstract class Entity<T> : IEntity<T> where T : notnull
{
    private readonly List<IDomainEvent<T>> _domainEvents = new();
    
    /// <summary>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(IDomainObject<T>? other)
    {
        return other is not null && Id == other.Id;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public T GetKey()
    {
        return Id;
    }

    /// <summary>
    /// </summary>
    public T Id { get; set; }

    public void AddDomainEvent(IDomainEvent<T> domainEvent)
    {
        throw new NotImplementedException();
    }

    public void RemoveDomainEvent(IDomainEvent<T> domainEvent)
    {
        throw new NotImplementedException();
    }

    public void ClearDomainEvents()
    {
        throw new NotImplementedException();
    }
}