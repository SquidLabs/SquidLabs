using System.Text.Json;
using StackExchange.Redis;

namespace SquidLabs.Tentacles.Infrastructure.Redis;

/// <summary>
///     I may come back and change this to non-async as we might want caching to be a blocking operation.
/// </summary>
public static class StackExchangeRedisExtensions
{
    public static async Task<T?> GetAsync<T>(this IDatabase cache, string key,
        CancellationToken cancellationToken = default)
    {
        var item = await cache.StringGetAsync(key).WaitAsync(cancellationToken);
        return await DeserializeAsync<T>(item!, cancellationToken).ConfigureAwait(false);
    }

    public static async Task SetAsync<T>(this IDatabase cache, string key, T value,
        CancellationToken cancellationToken = default) where T : class
    {
        await cache.StringSetAsync(key, await SerializeAsync(value)).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task<byte[]> SerializeAsync<T>(T entity,
        CancellationToken cancellationToken = default) where T : class
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        using var memoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(memoryStream, entity, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var objectDataAsStream = memoryStream.ToArray();
        return objectDataAsStream;
    }

    private static async Task<T?> DeserializeAsync<T>(byte[] stream,
        CancellationToken cancellationToken = default)
    {
        await using var memoryStream = new MemoryStream(stream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return await JsonSerializer.DeserializeAsync<T>(memoryStream, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}