namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
///     An interface for SquidLabs.Tentacles.Domain Driven Design Entity
///     Extend this with another interface or make a base class based on your needs.
/// </summary>
public interface IEntity<TKey> : IDomainObject<TKey>
{
    /// <summary>
    ///     The unique Identifier for the SquidLabs.Tentacles.Domain Entity
    /// </summary>
    public TKey Id { get; set; }
}