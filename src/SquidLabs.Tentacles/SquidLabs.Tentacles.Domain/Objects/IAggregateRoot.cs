namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
///     An inteface for DDD Aggregate Root
/// </summary>
/// <typeparam name="TId"></typeparam>
public interface IAggregateRoot<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    /// <summary>
    ///     The unique Identifier for the Aggregate
    /// </summary>
    public new TId Id { get; set; }
}