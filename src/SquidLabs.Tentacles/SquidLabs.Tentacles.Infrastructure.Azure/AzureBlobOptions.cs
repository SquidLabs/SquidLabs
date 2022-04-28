using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Azure;

/// <summary>
/// </summary>
public interface IAzureBlobOptions<TDataEntry> : IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
    /// <summary>
    /// </summary>
    public string ContainerName { get; set; }

    public string ConnectionDefinition { get; set; }
}