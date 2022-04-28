using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SquidLabs.Tentacles.Infrastructure.Tests.DataStore;

public class ISearchableDataStoreTest
{
    [Fact]
    public async Task ShouldBeAbleToSearchISearchableDataStore()
    {
        var dataStore = new TestSearchDataStore<Guid, TestDataEntry, TestSearchCriteria>();
        var ackbar = new TestDataEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Quote = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        var response = await dataStore.SearchAsync(new TestSearchCriteria { LastName = "ackbar" });
        Assert.Equal(response.FirstOrDefault()?.Id, ackbar.Id);
    }
}