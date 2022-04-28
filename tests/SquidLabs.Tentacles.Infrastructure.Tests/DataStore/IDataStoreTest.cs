using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SquidLabs.Tentacles.Infrastructure.Tests.DataStore;

public class IDataStoreTest
{
    [Fact]
    public async Task ShouldWriteAndReadToDataStore()
    {
        var dataStore = new MemoryCacheStore<Guid, TestDataEntry>(new MemoryCacheOptionsOfTDataEntry<TestDataEntry>());
        var ackbar = new TestDataEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Quote = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.NotNull(ackbarCopy);
        Assert.Equal(ackbarCopy?.Id, ackbar.Id);
        Assert.Equal(ackbarCopy?.FirstName, ackbar.FirstName);
        Assert.Equal(ackbarCopy?.LastName, ackbar.LastName);
        Assert.Equal(ackbarCopy?.Quote, ackbar.Quote);
    }

    [Fact]
    public async Task ShouldWriteAndUpdateToDataStore()
    {
        var dataStore = new MemoryCacheStore<Guid, TestDataEntry>(new MemoryCacheOptionsOfTDataEntry<TestDataEntry>());
        var ackbar = new TestDataEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Quote = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        ackbar.Quote = "Torpedoes inbound. It's been an honor serving with you all.";
        await dataStore.UpdateAsync(ackbar.Id, ackbar);
        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.Equal(ackbarCopy!.Id, ackbar.Id);
        Assert.Equal(ackbarCopy.FirstName, ackbar.FirstName);
        Assert.Equal(ackbarCopy.LastName, ackbar.LastName);
        Assert.Equal(ackbarCopy.Quote, ackbar.Quote);
    }

    [Fact]
    public async Task ShouldDeleteFromDataStore()
    {
        var dataStore = new MemoryCacheStore<Guid, TestDataEntry>(new MemoryCacheOptionsOfTDataEntry<TestDataEntry>());
        var ackbar = new TestDataEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Quote = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        await dataStore.DeleteAsync(ackbar.Id);
        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.Null(ackbarCopy);
    }

    [Fact]
    public async Task ShouldStopOperationWithCancellationTokenDataStore()
    {
        var dataStore = new MemoryCacheStore<Guid, TestDataEntry>(new MemoryCacheOptionsOfTDataEntry<TestDataEntry>());
        var ackbar = new TestDataEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Quote = "It's a trap!" };
        var cancellationTokenSource = new CancellationTokenSource();

        // don't await this call, we are purposefully letting the cancellation occur before the write.
        await Task.Run(async () =>
        {
            await Task.Delay(10);
            await dataStore.WriteAsync(ackbar.Id, ackbar, cancellationTokenSource.Token);
        });

        cancellationTokenSource.Cancel();
        await Task.Delay(20);

        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.Null(ackbarCopy);
    }
}