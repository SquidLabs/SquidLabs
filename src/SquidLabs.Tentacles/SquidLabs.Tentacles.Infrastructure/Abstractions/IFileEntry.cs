namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

public interface IFileEntry : IDataEntry
{
    Task<Stream> ToStreamAsync(CancellationToken cancellationToken = default);

    static abstract Task<IFileEntry> FromStreamAsync(Stream stream, CancellationToken cancellationToken = default);
}