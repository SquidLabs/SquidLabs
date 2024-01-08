using System;
using Xunit;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

public class ValueObjectRecordStructTests
{
    private readonly PhoneNumberValue _p1;
    private readonly PhoneNumberValue _p2;
    private readonly PhoneNumberValue _p3;
    private readonly PhoneNumberValue _p4;

    public ValueObjectRecordStructTests()
    {
        _p1 = new PhoneNumberValue(CountryCodesEnum.Us, 5558675309, null);
        _p2 = new PhoneNumberValue(CountryCodesEnum.Us, 5558675309);
        _p3 = new PhoneNumberValue(CountryCodesEnum.Us, 2813308004);
        _p4 = new PhoneNumberValue(CountryCodesEnum.Us, 2813308005);
    }

    [Fact]
    public void ValueObjectRecordStructHashesCorrectly()
    {
        Assert.Equal(_p1.GetValueHashCode(), new Guid("0000022b-14bd-0000-0000-000000000000"));
    }

    [Fact]
    public void ValueObjectDifferentConstructorsStillEqual()
    {
        Assert.Equal(_p1, _p2);
    }

    [Fact]
    public void ValueObjectDifferentValuesNotEqual()
    {
        Assert.NotEqual(_p1, _p3);
    }

    [Fact]
    public void ValueObjectDifferentSingleBitDifferencesNotEqual()
    {
        Assert.NotEqual(_p3, _p4);
    }
}