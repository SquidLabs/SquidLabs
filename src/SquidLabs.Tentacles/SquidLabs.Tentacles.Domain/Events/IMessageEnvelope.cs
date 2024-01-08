namespace SquidLabs.Tentacles.Domain.Events;

/// <summary>
/// </summary>
/// <typeparam name="TEvent"></typeparam>
/// <typeparam name="TCorrelation"></typeparam>
public interface IMessageEnvelope<TEvent, TCorrelation> where TEvent : IDomainEvent<TCorrelation>
    where TCorrelation : IEquatable<TCorrelation>
{
    /// <summary>
    ///     Destination address specific to the message transport, used to verify message origin
    /// </summary>
    public string DestinationAddress { get; }

    /// <summary>
    ///     Source address specific to the message transport, used for replies
    /// </summary>
    public string SourceAddress { get; }

    /// <summary>
    ///     Expiration time for the message
    /// </summary>
    public DateTime? ExpirationTime { get; }

    /// <summary>
    ///     When the message was sent
    /// </summary>
    public DateTime? SentTime { get; }

    /// <summary>
    ///     Domain event
    /// </summary>
    TEvent Event { get; }
}