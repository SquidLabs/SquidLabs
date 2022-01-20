using SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.DataStore.Options;

public class RedisOptions : IConnectionStringOptions<string>
{
    public string ConnectionString { get; set; }
}