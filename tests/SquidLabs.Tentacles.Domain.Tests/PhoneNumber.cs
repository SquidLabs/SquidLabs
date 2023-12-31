using System;
using System.Text;
using SquidLabs.Hashing;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests;

internal record struct PhoneNumber : IValueObject<Guid>
{
    public readonly CountryCodes CountryCode;
    public readonly string? Extension;
    public readonly ulong NationalNumber;

    public PhoneNumber(CountryCodes countryCode, ulong nationalNumber, string? extension) : this()
    {
        CountryCode = countryCode;
        NationalNumber = nationalNumber;
        Extension = extension;
        Key = GetKey();
    }

    public PhoneNumber(CountryCodes countryCode, ulong nationalNumber) : this()
    {
        CountryCode = countryCode;
        NationalNumber = nationalNumber;
        Extension = null;
        Key = GetKey();
    }

    public Guid Key { get; }

    public Guid GetKey()
    {
        if (Key != default) return Key;
        var bytes = new byte[16];
        Buffer.BlockCopy(NormalizedConverter.GetBytes((int)CountryCode), 0, bytes, 0, 4);
        Buffer.BlockCopy(NormalizedConverter.GetBytes(NationalNumber), 0, bytes, 4, 4);
        Buffer.BlockCopy(NormalizedConverter.GetBytes(Extension, Encoding.ASCII), 0, bytes, 12, 4);
        return new Guid(bytes);
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