using System;
using NUnit.Framework;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

public class DomainObjectImplementationGuid : IDomainObject<Guid>
{
    public Guid Id { get; set; }

    public bool Equals(IDomainObject<Guid>? other)
    {
        return GetKey().Equals(other.GetKey());
    }

    public Guid GetKey()
    {
        return Id;
    }
}

public class DomainObjectImplementationInt : IDomainObject<int>
{
    public int Id { get; set; }

    public bool Equals(IDomainObject<int>? other)
    {
        return GetKey().Equals(other.GetKey());
    }

    public int GetKey()
    {
        return Id;
    }
}

[TestFixture]
public class DomainObjectTests
{
    [Test]
    public void ShouldBeEqualGuidIdWithDefaultNew()
    {
        IDomainObject<Guid> d1 = new DomainObjectImplementationGuid();
        IDomainObject<Guid> d2 = new DomainObjectImplementationGuid();
        Assert.AreEqual(d1, d1);
    }

    [Test]
    public void ShouldBeEqualGuidIdWhenBothAreInitialized()
    {
        var testValue1 = Guid.NewGuid();
        var testValue2 = new Guid(testValue1.ToString());
        IDomainObject<Guid> d1 = new DomainObjectImplementationGuid { Id = testValue1 };
        IDomainObject<Guid> d2 = new DomainObjectImplementationGuid { Id = testValue2 };

        Assert.AreEqual(testValue1, testValue2);
        Assert.AreEqual(d1, d2);
    }

    [Test]
    public void ShouldFailWhenGuidIdAreDifferent()
    {
        var testValue1 = Guid.NewGuid();
        var testValue2 = Guid.NewGuid();
        IDomainObject<Guid> d1 = new DomainObjectImplementationGuid { Id = testValue1 };
        IDomainObject<Guid> d2 = new DomainObjectImplementationGuid { Id = testValue2 };

        Assert.AreNotEqual(testValue1, testValue2);
        Assert.AreNotEqual(d1, d2);
    }

    [Test]
    public void ShouldBeEqualIntIdWithDefaultNew()
    {
        IDomainObject<int> d1 = new DomainObjectImplementationInt();
        IDomainObject<int> d2 = new DomainObjectImplementationInt();
        Assert.AreEqual(d1, d1);
    }

    [Test]
    public void ShouldBeEqualIntIdWhenBothAreInitialized()
    {
        var testValue1 = 128;
        var testValue2 = 128;
        IDomainObject<int> d1 = new DomainObjectImplementationInt { Id = testValue1 };
        IDomainObject<int> d2 = new DomainObjectImplementationInt { Id = testValue2 };

        Assert.AreEqual(testValue1, testValue2);
        Assert.AreEqual(d1, d2);
    }

    [Test]
    public void ShouldFailWhenIntIdAreDifferent()
    {
        var testValue1 = 128;
        var testValue2 = 64;
        IDomainObject<int> d1 = new DomainObjectImplementationInt { Id = testValue1 };
        IDomainObject<int> d2 = new DomainObjectImplementationInt { Id = testValue2 };

        Assert.AreNotEqual(testValue1, testValue2);
        Assert.AreNotEqual(d1, d2);
    }
}