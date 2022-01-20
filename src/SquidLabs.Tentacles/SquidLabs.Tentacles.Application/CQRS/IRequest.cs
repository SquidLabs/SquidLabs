namespace SquidLabs.Tentacles.Application.CQRS;

/// <summary>
///     Marker interface to represent a request with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface IRequest<out TResponse>
{
}

public interface IRequest
{
}