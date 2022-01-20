namespace SquidLabs.Tentacles.Domain.Objects;

/// <summary>
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface INotification<TEvent, TCorrelation> where TEvent : IDomainEvent<TCorrelation>
{
    /// <summary>
    ///     A UUID to track the notifica tion itself, not necessarily describing the event.
    /// </summary>
    TCorrelation NotificationId { get; }

    /// <summary>
    ///     A unique ID to track the event as it moves through the system
    ///     The NotificationID may be removed but the correlation should follow the Event (maybe move this into the event)
    /// </summary>
    TCorrelation CorrelationId { get; }

    /// <summary>
    /// </summary>
    Uri SourceAddress { get; }

    /// <summary>
    /// </summary>
    Uri DestinationAddress { get; }

    /// <summary>
    /// </summary>
    Uri FaultAddress { get; }

    /// <summary>
    /// </summary>
    public DateTime? ExpirationTime { get; }

    /// <summary>
    /// </summary>
    public DateTime? SentTime { get; }

    /// <summary>
    /// </summary>
    TEvent Event { get; }
}