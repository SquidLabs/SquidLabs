namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
///     An Empty interface for SquidLabs.Tentacles.Domain Driven Design Value Object, Rosyln Analyzer enforces only record structs looking for
///     this Interface.
///     Extend this with another interface or make a base interface based on your needs.
/// </summary>
public interface IValueObject<TKey> : IDomainObject<TKey>
{
    /// <summary>
    ///     The resulting HashKey
    /// </summary>
    public TKey Key { get; }
}