using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SquidLabs.Tentacles.Infrastructure.Tests.DataStore;

public class ISearchableDataStoreTest
{
    [Test]
    public async Task ShouldBeAbleToSearchISearchableDataStore()
    {
        var dataStore = new TestSearchDataStore<Guid, TestEntity, TestSearchCriteria>();
        var ackbar = new TestEntity
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Phrase = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        var response = await dataStore.SearchAsync(new TestSearchCriteria { LastName = "ackbar" });
        Assert.AreEqual(response.FirstOrDefault().Id, ackbar.Id);
    }
}