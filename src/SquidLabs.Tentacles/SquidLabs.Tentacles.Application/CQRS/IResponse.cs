namespace SquidLabs.Tentacles.Application.CQRS;

public interface IResponse :
    IMessageContext
{
    object Message { get; }
}

public interface IResponse<out TResponse> :
    IResponse
    where TResponse : class
{
    new TResponse Message { get; }
}