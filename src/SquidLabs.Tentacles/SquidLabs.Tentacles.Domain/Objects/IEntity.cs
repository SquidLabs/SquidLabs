using SquidLabs.Tentacles.Domain.Events;

namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
///     An interface for SquidLabs.Tentacles.Domain Driven Design Entity
///     Extend this with another interface or make a base class based on your needs.
/// </summary>
public interface IEntity<TKey> : IDomainObject<TKey> where TKey : notnull
{
    void AddDomainEvent(IDomainEvent<TKey> domainEvent);
    void RemoveDomainEvent(IDomainEvent<TKey> domainEvent);
    void ClearDomainEvents();

}