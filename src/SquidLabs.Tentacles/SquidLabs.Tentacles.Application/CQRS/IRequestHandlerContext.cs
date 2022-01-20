namespace SquidLabs.Tentacles.Application.CQRS;

public interface IRequestHandlerContext
{
}

public interface IRequestHandlerContext<out T> :
    IRequestHandlerContext
    where T : class
{
    T Message { get; }
}