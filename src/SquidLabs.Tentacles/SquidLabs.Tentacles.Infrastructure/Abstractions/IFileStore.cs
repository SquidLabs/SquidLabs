namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

/// <summary>
///     an interface requiring that the file store must work on streams.
/// </summary>
/// <typeparam name="TFileName">File Name or Full Path</typeparam>
/// <typeparam name="TContent">File content, system.io.stream in most cases</typeparam>
public interface IFileStore<in TFileName, TContent> : IStore<TFileName, TContent>
    where TContent : IFileEntry
{
}