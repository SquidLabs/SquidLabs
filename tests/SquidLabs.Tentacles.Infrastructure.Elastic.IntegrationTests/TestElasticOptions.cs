using System;
using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests;

public class TestElasticSearchptions : IElasticSearchOptions<TestDataEntry>
{
    public string IndexField { get; set; } = null!;
    public Uri[] ConnectionDefinition { get; set; } = null!;
}