namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

public interface IClientFactory<TDataEntry, TConnectionOptions, out TClient>
    where TConnectionOptions : IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
    TConnectionOptions ClientOptions { get; }
    TClient GetClient();
}