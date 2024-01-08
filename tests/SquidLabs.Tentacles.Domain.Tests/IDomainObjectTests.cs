using System;
using SquidLabs.Tentacles.Domain.Objects;
using Xunit;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

public class DomainObjectImplementationGuid : IDomainObject<Guid>
{
    public Guid Id { get; set; }

    public bool Equals(IDomainObject<Guid>? other)
    {
        return other is not null && Id.Equals(other.Id);
    }
}

public class DomainObjectImplementationInt : IDomainObject<int>
{
    public int Id { get; set; }

    public bool Equals(IDomainObject<int>? other)
    {
        return other is not null && Id.Equals(other.Id);
    }
}

public class DomainObjectTests
{
    [Fact]
    public void ShouldBeEqualGuidIdWithDefaultNew()
    {
        IDomainObject<Guid> d1 = new DomainObjectImplementationGuid();
        IDomainObject<Guid> d2 = new DomainObjectImplementationGuid();
        Assert.Equal(d1, d1);
    }

    [Fact]
    public void ShouldBeEqualGuidIdWhenBothAreInitialized()
    {
        var testValue1 = Guid.NewGuid();
        var testValue2 = new Guid(testValue1.ToString());
        IDomainObject<Guid> d1 = new DomainObjectImplementationGuid { Id = testValue1 };
        IDomainObject<Guid> d2 = new DomainObjectImplementationGuid { Id = testValue2 };

        Assert.Equal(testValue1, testValue2);
        Assert.Equal(d1, d2);
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