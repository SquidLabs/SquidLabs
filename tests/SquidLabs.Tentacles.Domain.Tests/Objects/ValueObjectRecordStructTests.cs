using System;
using NUnit.Framework;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

public class ValueObjectRecordStructTests
{
    private PhoneNumber _p1;
    private PhoneNumber _p2;
    private PhoneNumber _p3;
    private PhoneNumber _p4;

    [SetUp]
    public void Setup()
    {
        _p1 = new PhoneNumber(CountryCodes.Us, 5558675309, null);
        _p2 = new PhoneNumber(CountryCodes.Us, 5558675309);
        _p3 = new PhoneNumber(CountryCodes.Us, 2813308004);
        _p3 = new PhoneNumber(CountryCodes.Us, 2813308005);
    }

    [Test]
    public void ValueObjectRecordStructHashesCorrectly()
    {
        Assert.AreEqual(_p1.Key, new Guid("0000022b-14bd-0000-0000-000000000000"));
    }

    [Test]
    public void ValueObjectDifferentConstructorsStillEqual()
    {
        Assert.AreEqual(_p1, _p2);
    }

    [Test]
    public void ValueObjectDifferentValuesNotEqual()
    {
        Assert.AreNotEqual(_p1, _p3);
    }

    [Test]
    public void ValueObjectDifferentSingleBitDifferencesNotEqual()
    {
        Assert.AreNotEqual(_p3, _p4);
    }
}