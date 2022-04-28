using System;
using System.Diagnostics.CodeAnalysis;
using SquidLabs.Tentacles.Infrastructure.Azure;
using SquidLabs.Tentacles.Infrastructure.Tests;

namespace SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests;

public class TestAzureBlobOptions : IAzureBlobOptions<TestFileEntry>
{
    public string ContainerName { get; set; } = null!;

    public string ConnectionDefinition { get; set; } = null!;
}