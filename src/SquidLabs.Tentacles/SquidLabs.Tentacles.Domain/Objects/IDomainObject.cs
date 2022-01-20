namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IDomainObject<TKey> : IEquatable<IDomainObject<TKey>>
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    TKey GetKey();
}