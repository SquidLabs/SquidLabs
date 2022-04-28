using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Redis;

public interface IRedisOptions<TDataEntry> : IConnectionOptions<TDataEntry> where TDataEntry : IDataEntry

{
    public int Database { get; set; }
    public string ConnectionDefinition { get; set; }
}