using System;
using SquidLabs.Tentacles.Domain.Objects;
using Xunit;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

public class ValueObjectRecordClassTests
{
    public record Address : ValueObject
    {
        public Address(string addressLine1, string? addressLine2, string? building, string city, string countryRegion,
            string? floorLevel, int postalCode, string stateProvince)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Building = building;
            City = city;
            CountryRegion = countryRegion;
            FloorLevel = floorLevel;
            PostalCode = postalCode;
            StateProvince = stateProvince;
            Key = GetKey();
        }

        public string AddressLine1 { get; init; }
        public string? AddressLine2 { get; init; }
        public string? Building { get; init; }
        public string City { get; init; }
        public string CountryRegion { get; init; }
        public string? FloorLevel { get; init; }
        public int PostalCode { get; init; }

        public string StateProvince { get; init; }

        public override Guid GetKey()
        {
            return new Guid();
        }
    }

    public class ValueObjectRecordStructTests
    {
        private Address _a1 = null!;
        private Address _a2 = null!;

        [Fact]
        public void Setup()
        {
            _a1 = new Address("1600 Pennsylvania Ave", null, null, "Washington", "USA", null, 20500, "D.C.");
            _a2 = new Address("1600 Pennsylvania Ave", null, null, "Washington", "USA", null, 20500, "D.C.");
        }

        [Fact]
        public void ValueObjectRecordStructHashesCorrectly()
        {
            Assert.Equal(_a1.Key, new Guid("0000022b-14bd-0000-0000-000000000000"));
        }

        [Fact]
        public void ValueObjectDifferentConstructorsStillEqual()
        {
            //Assert.Equal(p1, p2);
        }

        [Fact]
        public void ValueObjectDifferentValuesNotEqual()
        {
            // Assert.NotEqual(p1, p3);
        }

        [Fact]
        public void ValueObjectDifferentSingleBitDifferencesNotEqual()
        {
            //Assert.NotEqual(p3, p4);
        }
    }
}