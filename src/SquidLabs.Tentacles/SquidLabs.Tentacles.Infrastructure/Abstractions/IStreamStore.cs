using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure;

public interface IStreamStore<TEventData>
{
    Task AppendAsync(TEventData eventData, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEventData>> ReadAsync(IStreamPosition? fromPosition = default,
        StreamDirectionEnum direction = StreamDirectionEnum.Forward, CancellationToken cancellationToken = default);

    Task DeleteAsync();
}