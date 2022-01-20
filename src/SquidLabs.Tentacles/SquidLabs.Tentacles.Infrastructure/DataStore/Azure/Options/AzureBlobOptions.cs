using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.Azure.Options;

/// <summary>
/// </summary>
public class AzureBlobOptions : IConnectionStringOptions<string>
{
    /// <summary>
    /// </summary>
    public string ContainerName { get; set; }

    /// <summary>
    /// </summary>
    public string ConnectionString { get; set; }
}