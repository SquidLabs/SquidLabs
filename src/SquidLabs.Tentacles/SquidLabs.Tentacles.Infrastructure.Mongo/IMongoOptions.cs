using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Mongo;

/// <summary>
/// </summary>
public interface IMongoOptions<TDataEntry> : IConnectionOptions<TDataEntry> where TDataEntry : IDataEntry
{
    /// <summary>
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// </summary>
    public string ConnectionDefinition { get; set; }
}