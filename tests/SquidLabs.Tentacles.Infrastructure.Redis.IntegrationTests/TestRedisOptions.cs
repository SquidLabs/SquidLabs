using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Redis.IntegrationTests;

public class TestRedisOptions : IRedisOptions<TestDataEntry>
{
    public string DatabaseName { get; set; } = null!;
    public int Database { get; set; } = 0;
    public string ConnectionDefinition { get; set; } = null!;
}