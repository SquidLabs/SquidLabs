using System;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests;

public class TestEntity : Entity
{
    public TestEntity(string value)
    {
        Value = value;
        Id = Guid.NewGuid();
    }

    public TestEntity(Guid id, string value)
    {
        Value = value;
        Id = id;
    }

    public string Value { get; set; }
}