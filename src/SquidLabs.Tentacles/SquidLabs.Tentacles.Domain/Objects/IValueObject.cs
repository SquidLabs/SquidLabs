namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
///     An Empty interface for SquidLabs.Tentacles.Domain Driven Design Value Object, Rosyln Analyzer enforces only record
///     structs looking for
///     this Interface.
///     Extend this with another interface or make a base interface based on your needs.
/// </summary>
public interface IValueObject<TId> : IDomainObject<TId> where TId : IEquatable<TId>
{
    new TId Id { get; init; }
    TId GetValueHashCode();
}