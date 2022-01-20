namespace SquidLabs.Tentacles.Application.CQRS;

public interface IRequestPipeline<TRequest> where TRequest : IRequest
{
    Task InvokeAsync(IMessageContext messageContext);
}