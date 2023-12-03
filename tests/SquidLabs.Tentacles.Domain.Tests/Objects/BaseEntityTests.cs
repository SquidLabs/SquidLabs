using System;
using NUnit.Framework;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

[TestFixture]
public class BaseEntityTests
{
    [Test]
    public void ShouldNotBeEqualWithSameInnerValue()
    {
        TestEntity t1 = new TestEntity("value");
        TestEntity t2 = new TestEntity("value");
        Assert.AreNotEqual(t1, t2);
    }

    [Test]
    public void ShouldBeEqualWhenSameIdAndDifferentValue()
    {
        Guid id = Guid.NewGuid();
        TestEntity t1 = new TestEntity(id,"value");
        TestEntity t2 = new TestEntity(id,"value");
        Assert.AreEqual(t1, t2);
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