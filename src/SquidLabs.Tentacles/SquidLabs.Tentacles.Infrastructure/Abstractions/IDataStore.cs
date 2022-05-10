namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

/// <summary>
///     an interface requiring that Data Stores must work on DataEntries
/// </summary>
/// <typeparam name="TIdentifier">id</typeparam>
/// <typeparam name="TEntry"></typeparam>
public interface IDataStore<in TIdentifier, TEntry> : IStore<TIdentifier, TEntry>
    where TEntry : IDataEntry
{
}