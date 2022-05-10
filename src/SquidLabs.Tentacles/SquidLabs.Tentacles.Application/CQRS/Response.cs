namespace SquidLabs.Tentacles.Application.CQRS;

public readonly struct Response<T1, T2> :
    IResponse
    where T1 : class
    where T2 : class
{
    private readonly IResponse<T1>? _response1;
    private readonly IResponse<T2>? _response2;
    private readonly IResponse _response;
    private readonly Task<IResponse<T1>> _response1Task;
    private readonly Task<IResponse<T2>> _response2Task;

    public Response(Task<IResponse<T1>> response1, Task<IResponse<T2>> response2)
    {
        _response1Task = response1;
        _response2Task = response2;

        _response1 = response1.Status == TaskStatus.RanToCompletion ? response1.GetAwaiter().GetResult() : default;
        _response2 = response2.Status == TaskStatus.RanToCompletion ? response2.GetAwaiter().GetResult() : default;

        _response = _response1 as IResponse ??
                    _response2 ?? throw new ArgumentException("At least one response must have completed");
    }

    public Guid? MessageId => _response.MessageId;

    public Guid? RequestId => _response.RequestId;

    public Guid? CorrelationId => _response.CorrelationId;

    public Guid? ConversationId => _response.ConversationId;

    public Guid? InitiatorId => _response.InitiatorId;

    public DateTime? ExpirationTime => _response.ExpirationTime;

    public Uri SourceAddress => _response.SourceAddress;

    public Uri DestinationAddress => _response.DestinationAddress;

    public Uri ResponseAddress => _response.ResponseAddress;

    public Uri FaultAddress => _response.FaultAddress;

    public DateTime? SentTime => _response.SentTime;

    /*
    public Headers Headers => _response.Headers;

    public HostInfo Host => _response.Host;
    */

    public object Message => _response.Message;
}