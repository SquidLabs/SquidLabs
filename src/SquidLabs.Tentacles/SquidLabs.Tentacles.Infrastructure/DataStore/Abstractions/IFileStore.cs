namespace SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

/// <summary>
///     For now this is just a wrapper indicating that the store works on files (cloud or local)
/// </summary>
/// <typeparam name="TFileName">File Name or Full Path</typeparam>
/// <typeparam name="TContent">File content, system.io.stream in most cases</typeparam>
public interface IFileStore<TFileName, TContent> : IDataStore<TFileName, TContent>
{
}