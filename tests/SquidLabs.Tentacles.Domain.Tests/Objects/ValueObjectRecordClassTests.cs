using System;
using NUnit.Framework;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests.Objects;

public class ValueObjectRecordClassTests
{
    public record Address : BaseValueObject
    {
        public Address(string addressLine1, string addressLine2, string building, string city, string countryRegion,
            string floorLevel, int postalCode, string stateProvince)
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
        public string FloorLevel { get; init; }
        public int PostalCode { get; init; }

        public string StateProvince { get; init; }

        public override Guid GetKey()
        {
            return new Guid();
        }
    }

    public class ValueObjectRecordStructTests
    {
        private Address _a1;
        private Address _a2;
        private Address _a3;
        private Address _a4;

        [SetUp]
        public void Setup()
        {
            _a1 = new Address("1600 Pennsylvania Ave", null, null, "Washington", null, null, 20500, "D.C.");
            _a2 = new Address("1600 Pennsylvania Ave", null, null, "Washington", null, null, 20500, "D.C.");
        }

        [Test]
        public void ValueObjectRecordStructHashesCorrectly()
        {
            Assert.AreEqual(_a1.Key, new Guid("0000022b-14bd-0000-0000-000000000000"));
        }

        [Test]
        public void ValueObjectDifferentConstructorsStillEqual()
        {
            //Assert.AreEqual(p1, p2);
        }

        [Test]
        public void ValueObjectDifferentValuesNotEqual()
        {
            // Assert.AreNotEqual(p1, p3);
        }

        [Test]
        public void ValueObjectDifferentSingleBitDifferencesNotEqual()
        {
            //Assert.AreNotEqual(p3, p4);
        }
    }
}