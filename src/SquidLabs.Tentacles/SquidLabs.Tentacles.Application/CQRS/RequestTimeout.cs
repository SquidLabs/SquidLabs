namespace SquidLabs.Tentacles.Application.CQRS;

public struct RequestTimeout
{
    private TimeSpan? _timeout;

    private RequestTimeout(TimeSpan timeout)
    {
        if (timeout <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(timeout), "RequestTimeout must be greater than 0");

        _timeout = timeout;
    }

    public static RequestTimeout None => new(TimeSpan.Zero);
    public static RequestTimeout Default => new(TimeSpan.FromSeconds(30));
}