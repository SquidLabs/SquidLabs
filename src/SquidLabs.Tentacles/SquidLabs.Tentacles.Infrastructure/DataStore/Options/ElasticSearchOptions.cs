using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.Options;

public class ElasticSearchOptions : IConnectionStringOptions<Uri[]>
{
    public string IndexField { get; set; }
    public Uri[] ConnectionString { get; set; }
}