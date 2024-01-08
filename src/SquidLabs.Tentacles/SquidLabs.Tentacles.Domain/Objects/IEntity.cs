using SquidLabs.Tentacles.Domain.Events;

namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
///     An interface for SquidLabs.Tentacles.Domain Driven Design Entity
///     Extend this with another interface or make a base class based on your needs.
/// </summary>
public interface IEntity<TId> : IDomainObject<TId> where TId : notnull, IEquatable<TId>
{
    new TId Id { get; set; }
    void AddDomainEvent(IDomainEvent<TId> domainEvent);
    void RemoveDomainEvent(IDomainEvent<TId> domainEvent);
    void ClearDomainEvents();
}