namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IDomainObject<TKey> : IEquatable<IDomainObject<TKey>> where TKey : notnull
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public TKey Id { get; }
}