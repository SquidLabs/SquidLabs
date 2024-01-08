using System;
using System.Text;
using SquidLabs.Hashing;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests;

public record PhoneNumberValue : ValueObject<Guid>
{
    private static readonly byte[] _keyBytes = [0xa3, 0x81, 0x7f, 0x04, 0x8c, 0x6e, 0x8e, 0x8e];

    public readonly CountryCodesEnum CountryCodeEnum;
    public readonly string? Extension;
    public readonly ulong NationalNumber;
    private readonly Guid _key = default;

    public PhoneNumberValue(CountryCodesEnum countryCodeEnum, ulong nationalNumber, string? extension)
    {
        CountryCodeEnum = countryCodeEnum;
        NationalNumber = nationalNumber;
        Extension = extension;
        Id = GetValueHashCode();
    }

    public PhoneNumberValue(CountryCodesEnum countryCodeEnum, ulong nationalNumber)
    {
        CountryCodeEnum = countryCodeEnum;
        NationalNumber = nationalNumber;
        Extension = null;
        Id = GetValueHashCode();
    }

    public override Guid GetValueHashCode()
    {
        if (_key != default) return _key;
        var bytes = new byte[16];
        Buffer.BlockCopy(NormalizedConverter.GetBytes((int)CountryCodeEnum), 0, bytes, 0, 4);
        Buffer.BlockCopy(NormalizedConverter.GetBytes(NationalNumber), 0, bytes, 4, 4);
        Buffer.BlockCopy(NormalizedConverter.GetBytes(Extension, Encoding.ASCII), 0, bytes, 12, 4);
        return new Guid(SipHash.ComputeHash128(bytes, _keyBytes));
    }


    public override string ToString()
    {
        return $"{CountryCodeEnum} {NationalNumber:(###) ###-####} {Extension}";
    }
}