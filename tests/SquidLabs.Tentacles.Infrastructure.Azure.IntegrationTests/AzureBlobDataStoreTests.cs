using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Moq;
using SquidLabs.Tentacles.Infrastructure.Abstractions;
using SquidLabs.Tentacles.Infrastructure.Elastic.IntegrationTests;
using SquidLabs.Tentacles.Infrastructure.Exceptions;
using SquidLabs.Tentacles.Infrastructure.Tests;
using Xunit;

namespace SquidLabs.Tentacles.Infrastructure.Azure.IntegrationTests;

public class AzureBlobDataStoreTests
{
    private const string TestTextOne = "simple test text value.";
    private const string TestTextTwo = "simple test two text value.";
    private readonly IOptionsMonitor<IAzureBlobOptions<TestFileEntry>> _azureBlobOptions;

    private readonly IClientFactory<TestFileEntry, IAzureBlobOptions<TestFileEntry>, BlobContainerClient>
        _clientFactory;

    private readonly IStore<Guid, TestFileEntry> _dataStore;

    private readonly FileInfo _testFileInfoOne = new("test1.txt");
    private readonly FileInfo _testFileInfoTwo = new("test1.txt");

    public AzureBlobDataStoreTests()
    {
        var mockOptionsMonitorForAzureBlob = new Mock<IOptionsMonitor<TestAzureBlobOptions>>();
        mockOptionsMonitorForAzureBlob.Setup(monitor => monitor.CurrentValue).Returns(new TestAzureBlobOptions
        {
            ConnectionDefinition = "derp"
        });

        _azureBlobOptions = mockOptionsMonitorForAzureBlob.Object;
        _clientFactory = new AzureBlobClientFactory<TestFileEntry>(_azureBlobOptions);

        _dataStore = new AzureFileStore<TestFileEntry>(_clientFactory);

        File.WriteAllText(_testFileInfoOne.FullName, TestTextOne);
    }

    [Fact]
    public async Task ShouldWriteToDataStore()
    {
        var id = Guid.NewGuid();
        var entry = new TestFileEntry(_testFileInfoOne.FullName, _testFileInfoOne.OpenRead());

        var exception = await Record.ExceptionAsync(async () =>
            await _dataStore.WriteAsync(id, entry, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task ShouldWriteAndReadToDataStore()
    {
        var id = Guid.NewGuid();
        var entry = new TestFileEntry(_testFileInfoOne.FullName, _testFileInfoOne.OpenRead());

        var exception = await Record.ExceptionAsync(async () =>
            await _dataStore.WriteAsync(id, entry, CancellationToken.None));
        var retrievedEntry = await _dataStore.ReadAsync(id);

        Assert.Null(exception);
        Assert.NotNull(retrievedEntry);
    }

    [Fact]
    public async Task ShouldWriteAndDeleteToDataStore()
    {
        var id = Guid.NewGuid();
        var entry = new TestFileEntry(_testFileInfoOne.FullName, _testFileInfoOne.OpenRead());

        await _dataStore.WriteAsync(id, entry, CancellationToken.None);
        var afterWrite = await _dataStore.ReadAsync(id);
        await _dataStore.DeleteAsync(id);
        var afterDelete = await _dataStore.ReadAsync(id);

        Assert.NotNull(afterWrite);
        Assert.Null(afterDelete);
    }

    [Fact]
    public async Task ShouldWriteAndDeleteAndRetrieveNullToDataStore()
    {
        var id = Guid.NewGuid();
        var entry = new TestFileEntry(_testFileInfoOne.FullName, _testFileInfoOne.OpenRead());

        await _dataStore.WriteAsync(id, entry, CancellationToken.None);
        await _dataStore.DeleteAsync(id);
        var result = await _dataStore.ReadAsync(id);

        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldWriteAndUpdateToDataStore()
    {
        var id = Guid.NewGuid();
        var entry = new TestFileEntry(_testFileInfoOne.FullName, _testFileInfoOne.OpenRead());

        var updateEntry = new TestFileEntry(_testFileInfoTwo.FullName, _testFileInfoTwo.OpenRead());

        await _dataStore.WriteAsync(id, entry, CancellationToken.None);

        await _dataStore.UpdateAsync(id, updateEntry);
        var resultEntry = await _dataStore.ReadAsync(id, CancellationToken.None);

        var streamReader = new StreamReader(await resultEntry?.ToStreamAsync()!);

        var result = streamReader.ReadToEnd();


        Assert.Equal(TestTextTwo, result);
    }

    [Fact]
    public async Task ShouldTriggerAnExceptionWithNoRecordToDelete()
    {
        var id = Guid.NewGuid();
        var exception =
            await Record.ExceptionAsync(async () =>
                await _dataStore.DeleteAsync(id, CancellationToken.None));

        Assert.NotNull(exception);
        Assert.Equal(typeof(StoreEntryNotFoundException), exception.GetType());
        Assert.Equal(StoreOperationTypeEnum.Delete, ((StoreEntryNotFoundException)exception).OperationType);
    }

    [Fact]
    public async Task Should_Return_Null_When_Reading_An_Id_That_Does_Not_Exist()
    {
        var id = Guid.NewGuid();
        var result = new TestFileEntry(_testFileInfoOne.FullName);
        var exception =
            await Record.ExceptionAsync(async () =>
                result = await _dataStore.ReadAsync(id, CancellationToken.None));

        Assert.Null(exception);
        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldThrowAnExceptionWhenWritingAnExistingRecord()
    {
        var id = Guid.NewGuid();
        var entry = new TestFileEntry(_testFileInfoOne.FullName);
        await _dataStore.WriteAsync(id, entry, CancellationToken.None);
        var exception =
            await Record.ExceptionAsync(async () =>
                await _dataStore.WriteAsync(id, entry, CancellationToken.None));

        Assert.NotNull(exception);
        Assert.Equal(typeof(StoreDuplicateIdentifierException), exception.GetType());
    }

    [Fact]
    public async Task ShouldThrowAnExceptionWhenUpdatingARecordThatDoesNotExist()
    {
        var id = Guid.NewGuid();
        var entry = new TestFileEntry(_testFileInfoOne.FullName);

        var exception =
            await Record.ExceptionAsync(async () =>
                await _dataStore.UpdateAsync(id, entry, CancellationToken.None));

        Assert.NotNull(exception);
        Assert.Equal(typeof(StoreEntryNotFoundException), exception.GetType());
        Assert.Equal(StoreOperationTypeEnum.Update, ((StoreEntryNotFoundException)exception).OperationType);
    }
}