namespace SquidLabs.Tentacles.Application.CQRS;

public interface IRequestHandler
{
}

public interface IRequestHandler<in TMessage> :
    IRequestHandler
    where TMessage : class, IRequest
{
    Task Handle(IRequestHandlerContext<TMessage> context);
}