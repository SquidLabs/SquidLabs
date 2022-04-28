namespace SquidLabs.Tentacles.Application.CQRS;

public interface IRequestClient<TRequest>
    where TRequest : IRequest
{
    Task<IResponse<T>> GetResponse<T>(TRequest message, CancellationToken cancellationToken = default,
        RequestTimeout timeout = default)
        where T : class;

    Task<Response<T1, T2>> GetResponse<T1, T2>(TRequest message, CancellationToken cancellationToken = default,
        RequestTimeout timeout = default)
        where T1 : class
        where T2 : class;
}