using System;
using NUnit.Framework;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

[TestFixture]
public class DomainObjectTests
{
    [SetUp]
    public void Setup()
    {
        IEntity<Guid> d1 = new BaseEntity
    }

    [Test]
    public void ValueObjectRecordStructHashesCorrectly()
    {
        Assert.AreEqual(_a1.Key, new Guid("0000022b-14bd-0000-0000-000000000000"));
    }
}