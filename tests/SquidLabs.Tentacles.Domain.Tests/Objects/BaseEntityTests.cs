using System;
using SquidLabs.Tentacles.Domain.Objects;
using Xunit;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

public class BaseEntityTests
{
    [Fact]
    public void ShouldNotBeEqualWithSameInnerValue()
    {
        var t1 = new TestEntity("value");
        var t2 = new TestEntity("value");
        Assert.NotEqual(t1, t2);
    }

    [Fact]
    public void ShouldBeEqualWhenSameIdAndDifferentValue()
    {
        var id = Guid.NewGuid();
        var t1 = new TestEntity(id, "value");
        var t2 = new TestEntity(id, "value");
        Assert.Equal(t1, t2);
    }

    [Fact]
    public void ShouldFailWhenGuidIdAreDifferent()
    {
        var testValue1 = Guid.NewGuid();
        var testValue2 = Guid.NewGuid();
        IDomainObject<Guid> d1 = new DomainObjectImplementationGuid { Id = testValue1 };
        IDomainObject<Guid> d2 = new DomainObjectImplementationGuid { Id = testValue2 };

        Assert.NotEqual(testValue1, testValue2);
        Assert.NotEqual(d1, d2);
    }

    [Fact]
    public void ShouldBeEqualIntIdWithDefaultNew()
    {
        IDomainObject<int> d1 = new DomainObjectImplementationInt();
        IDomainObject<int> d2 = new DomainObjectImplementationInt();
        Assert.Equal(d1, d1);
    }

    [Fact]
    public void ShouldBeEqualIntIdWhenBothAreInitialized()
    {
        var testValue1 = 128;
        var testValue2 = 128;
        IDomainObject<int> d1 = new DomainObjectImplementationInt { Id = testValue1 };
        IDomainObject<int> d2 = new DomainObjectImplementationInt { Id = testValue2 };

        Assert.Equal(testValue1, testValue2);
        Assert.Equal(d1, d2);
    }

    [Fact]
    public void ShouldFailWhenIntIdAreDifferent()
    {
        var testValue1 = 128;
        var testValue2 = 64;
        IDomainObject<int> d1 = new DomainObjectImplementationInt { Id = testValue1 };
        IDomainObject<int> d2 = new DomainObjectImplementationInt { Id = testValue2 };

        Assert.NotEqual(testValue1, testValue2);
        Assert.NotEqual(d1, d2);
    }
}