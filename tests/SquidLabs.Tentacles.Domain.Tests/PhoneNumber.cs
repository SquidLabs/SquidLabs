using System;
using System.Text;
using SquidLabs.Hashing;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests;

internal record struct PhoneNumber : IValueObject<Guid>
{
    private static byte[] _keyBytes = new byte[8] { 0xa3, 0x81, 0x7f, 0x04, 0x8c, 0x6e, 0x8e, 0x8e };
    private Guid _key = default;
    public readonly CountryCodes CountryCode;
    public readonly string? Extension;
    public readonly ulong NationalNumber;

    public PhoneNumber(CountryCodes countryCode, ulong nationalNumber, string? extension) : this()
    {
        CountryCode = countryCode;
        NationalNumber = nationalNumber;
        Extension = extension;
    }

    public PhoneNumber(CountryCodes countryCode, ulong nationalNumber) : this()
    {
        CountryCode = countryCode;
        NationalNumber = nationalNumber;
        Extension = null;
    }

    public Guid Key => GetKey();

    public Guid GetKey()
    {
        if (_key != default) return _key;
        var bytes = new byte[16];
        Buffer.BlockCopy(NormalizedConverter.GetBytes((int)CountryCode), 0, bytes, 0, 4);
        Buffer.BlockCopy(NormalizedConverter.GetBytes(NationalNumber), 0, bytes, 4, 4);
        Buffer.BlockCopy(NormalizedConverter.GetBytes(Extension, Encoding.ASCII), 0, bytes, 12, 4);
        return new Guid(SipHash.ComputeHash128(bytes, _keyBytes));
    }

    public bool Equals(IDomainObject<Guid>? other)
    {
        return other is not null && GetKey() == other.GetKey();
    }

    public override string ToString()
    {
        return $"{CountryCode} {NationalNumber:{{0:(###) ###-####}}} {Extension}";
    }
}