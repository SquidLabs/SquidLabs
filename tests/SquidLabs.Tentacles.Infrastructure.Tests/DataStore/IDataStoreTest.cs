using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SquidLabs.Tentacles.Infrastructure.Tests.DataStore;

public class IDataStoreTest
{
    [Test]
    public async Task ShouldWriteAndReadToDataStore()
    {
        var dataStore = new TestCacheDataStore<Guid, TestEntry>();
        var ackbar = new TestEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Phrase = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.AreEqual(ackbarCopy.Id, ackbar.Id);
        Assert.AreEqual(ackbarCopy.FirstName, ackbar.FirstName);
        Assert.AreEqual(ackbarCopy.LastName, ackbar.LastName);
        Assert.AreEqual(ackbarCopy.Phrase, ackbar.Phrase);
    }

    [Test]
    public async Task ShouldWriteAndUpdateToDataStore()
    {
        var dataStore = new TestCacheDataStore<Guid, TestEntry>();
        var ackbar = new TestEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Phrase = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        ackbar.Phrase = "Torpedoes inbound. It's been an honor serving with you all.";
        await dataStore.UpdateAsync(ackbar.Id, ackbar);
        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.AreEqual(ackbarCopy.Id, ackbar.Id);
        Assert.AreEqual(ackbarCopy.FirstName, ackbar.FirstName);
        Assert.AreEqual(ackbarCopy.LastName, ackbar.LastName);
        Assert.AreEqual(ackbarCopy.Phrase, ackbar.Phrase);
    }

    [Test]
    public async Task ShouldDeleteFromDataStore()
    {
        var dataStore = new TestCacheDataStore<Guid, TestEntry>();
        var ackbar = new TestEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Phrase = "It's a trap!" };
        await dataStore.WriteAsync(ackbar.Id, ackbar);
        await dataStore.DeleteAsync(ackbar.Id);
        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.IsNull(ackbarCopy);
    }

    [Test]
    public async Task ShouldStopOperationWithCancellationTokenDataStore()
    {
        var dataStore = new TestCacheDataStore<Guid, TestEntry>();
        var ackbar = new TestEntry
            { Id = Guid.NewGuid(), FirstName = "Gial", LastName = "Ackbar", Phrase = "It's a trap!" };
        var cancellationTokenSource = new CancellationTokenSource();

        // don't await this call, we are purposefully letting the cancelation occur before the write.
        Task.Run(async () =>
        {
            await Task.Delay(10);
            await dataStore.WriteAsync(ackbar.Id, ackbar, cancellationTokenSource.Token);
        });

        cancellationTokenSource.Cancel();
        await Task.Delay(20);

        var ackbarCopy = await dataStore.ReadAsync(ackbar.Id);
        Assert.IsNull(ackbarCopy);
    }
}