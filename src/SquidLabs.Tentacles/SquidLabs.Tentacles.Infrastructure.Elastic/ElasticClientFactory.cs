using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Elastic;

public class
    ElasticClientFactory<TDataEntry> : IClientFactory<TDataEntry, IElasticSearchOptions<TDataEntry>, IElasticClient>
    where TDataEntry : class, IDataEntry
{
    private readonly IOptionsMonitor<IElasticSearchOptions<TDataEntry>> _elasticSearchOptionsMonitor;


    public ElasticClientFactory(IOptionsMonitor<IElasticSearchOptions<TDataEntry>> elasticSearchOptionsMonitor)
    {
        _elasticSearchOptionsMonitor = elasticSearchOptionsMonitor;
    }

    public IElasticSearchOptions<TDataEntry> ClientOptions => _elasticSearchOptionsMonitor.CurrentValue;

    public IElasticClient GetClient()
    {
        // ElasticClient does not reuse Http requests, the article outlines that it's safe to use as a singleton per connection.
        // https://github.com/elastic/elasticsearch-net/blob/0b2a83b8f9647ae482c91e67fe9bf983d18c2947/docs/client-concepts/connection/function-as-a-service-environments.asciidoc#azure-functions

        return new ElasticClient(BuildConnectionSettings(_elasticSearchOptionsMonitor.CurrentValue));
    }

    private static ConnectionSettings BuildConnectionSettings(IElasticSearchOptions<TDataEntry> searchOptions)
    {
        if (searchOptions.ConnectionDefinition.Length == 0) throw new MissingFieldException();
        return (searchOptions.ConnectionDefinition.Length > 1
                ? new ConnectionSettings(new SniffingConnectionPool(searchOptions.ConnectionDefinition))
                : new ConnectionSettings(searchOptions.ConnectionDefinition[0]))
            .DefaultMappingFor<TDataEntry>(descriptor => descriptor.IndexName(searchOptions.IndexField.ToLower()))
            .ThrowExceptions();
    }
}