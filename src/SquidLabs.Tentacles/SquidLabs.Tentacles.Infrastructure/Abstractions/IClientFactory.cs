namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

/// <summary>
/// </summary>
/// <typeparam name="TDataEntry"></typeparam>
/// <typeparam name="TConnectionOptions"></typeparam>
/// <typeparam name="TClient"></typeparam>
public interface IClientFactory<TDataEntry, out TConnectionOptions, out TClient>
    where TConnectionOptions : IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
    /// <summary>
    /// </summary>
    TConnectionOptions ClientOptions { get; }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    TClient GetClient();
}