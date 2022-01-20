namespace SquidLabs.Tentacles.Domain.Objects;

public class BaseNotification<TEvent> : INotification<TEvent, Guid> where TEvent : IDomainEvent<Guid>
{
    /// <summary>
    /// </summary>
    public Guid NotificationId { get; }

    /// <summary>
    /// </summary>
    public Guid CorrelationId { get; }

    /// <summary>
    /// </summary>
    public Uri SourceAddress { get; }

    /// <summary>
    /// </summary>
    public Uri DestinationAddress { get; }

    /// <summary>
    /// </summary>
    public Uri FaultAddress { get; }

    /// <summary>
    /// </summary>
    public DateTime? ExpirationTime { get; }

    /// <summary>
    /// </summary>
    public DateTime? SentTime { get; }

    /// <summary>
    /// </summary>
    public TEvent Event { get; }
}