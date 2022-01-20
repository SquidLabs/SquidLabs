namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
///     An inteface for DDD Aggregate Root
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAggregateRoot<TKey> : IDomainObject<TKey>
{
    /// <summary>
    ///     The unique Identifier for the Aggregate
    /// </summary>
    public TKey Id { get; set; }
}