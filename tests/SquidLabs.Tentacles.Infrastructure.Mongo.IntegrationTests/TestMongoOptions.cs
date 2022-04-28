using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Mongo.IntegrationTests;

public class TestMongoOptions : IMongoOptions<TestDataEntry>
{
    public string DatabaseName { get; set; } = null!;
    public string ConnectionDefinition { get; set; } = null!;
}