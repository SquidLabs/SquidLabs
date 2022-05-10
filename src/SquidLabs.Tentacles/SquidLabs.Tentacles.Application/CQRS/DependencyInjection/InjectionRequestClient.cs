using Microsoft.Extensions.Logging;

namespace SquidLabs.Tentacles.Application.CQRS.DependencyInjection;

public class InjectionRequestClient<TRequest> : IRequestClient<TRequest>
    where TRequest : class, IRequest
{
    private readonly ILogger<IRequestHandler<TRequest>> _logger;
    private readonly IRequestHandler<TRequest> _requestHandler;

    public InjectionRequestClient(IRequestHandler<TRequest> requestHandler, ILogger<IRequestHandler<TRequest>> logger)
    {
        _requestHandler = requestHandler;
        _logger = logger;
    }

    public Task<IResponse<T>> GetResponse<T>(TRequest message, CancellationToken cancellationToken = default,
        RequestTimeout timeout = default) where T : class
    {
        _logger.LogInformation("Request Client calling handler for message", message);

        // Where does the pipeline go, in the handler?
        // before handler gets called we run through the DI pipeline (make it look like middleware?)
        // do we want greenpipes style tpl flow?

        //IRequestHandlerContext<TRequest> context;
        //_requestHandler.Handle(context);
        return Task.FromResult<IResponse<T>>(null!);
    }

    public Task<Response<T1, T2>> GetResponse<T1, T2>(TRequest message, CancellationToken cancellationToken = default,
        RequestTimeout timeout = default) where T1 : class where T2 : class
    {
        _logger.LogInformation("Request Client calling handler for message", message);

        //IRequestHandlerContext<TRequest> context;
        //_requestHandler.Handle(context);
        return Task.FromResult(new Response<T1, T2>());
    }
}