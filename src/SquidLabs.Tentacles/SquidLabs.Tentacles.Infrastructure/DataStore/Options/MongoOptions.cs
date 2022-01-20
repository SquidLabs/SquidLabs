using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.Options;

/// <summary>
/// </summary>
public class MongoOptions : IConnectionStringOptions<string>
{
    /// <summary>
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// </summary>
    public string ConnectionString { get; set; }
}