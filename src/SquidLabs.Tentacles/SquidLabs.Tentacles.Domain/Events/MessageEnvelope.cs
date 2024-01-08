namespace SquidLabs.Tentacles.Domain.Events;

public abstract class MessageEnvelope<TEvent> : IMessageEnvelope<TEvent, Guid> where TEvent : IDomainEvent<Guid>
{
    public string DestinationAddress { get; init; } = null!;
    public string SourceAddress { get; init; } = null!;
    public DateTime? ExpirationTime { get; init; }
    public DateTime? SentTime { get; init; }
    public required TEvent Event { get; init; }
}