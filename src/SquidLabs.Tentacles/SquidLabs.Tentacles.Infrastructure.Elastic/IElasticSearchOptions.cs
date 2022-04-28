using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Elastic;

public interface IElasticSearchOptions<TDataEntry> : IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
    public string IndexField { get; set; }
    public Uri[] ConnectionDefinition { get; set; }
}