using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using SquidLabs.Tentacles.Infrastructure.Abstractions;
using SquidLabs.Tentacles.Infrastructure.Exceptions;
using SquidLabs.Tentacles.Infrastructure.Tests;
using Xunit;

namespace SquidLabs.Tentacles.Infrastructure.Dapper.IntegrationTests;

public class DapperDataStoreTests
{
    private readonly IClientFactory<TestDataEntry, IDapperSqlStoreOptions<TestDataEntry>, IDbConnection> _clientFactory;
    private readonly IDataStore<Guid, TestDataEntry> _dataStore;
    private readonly IOptionsMonitor<IDapperSqlStoreOptions<TestDataEntry>> _dapperOptions;

    public DapperDataStoreTests()
    {
        var mockOptionsMonitorForElastic = new Mock<IOptionsMonitor<TestDapperOptions>>();
        mockOptionsMonitorForElastic.Setup(monitor => monitor.CurrentValue).Returns(new TestDapperOptions()
        {
    
        });

        _dapperOptions = mockOptionsMonitorForElastic.Object;
        _clientFactory = new DapperClientFactory<TestDataEntry>(_dapperOptions);

        _dataStore = new DapperSqlStore<Guid, TestDataEntry>(_clientFactory);
    }

    [Fact]
    public async Task ShouldWriteToDataStore()
    {
        var entry = new TestDataEntry
        {
            Id = Guid.NewGuid(),
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"Cowards die many times before their deaths; The valiant never taste of death but once. 
                Of all the wonders that I yet have heard. It seems to me most strange that men should fear;
                Seeing that death, a necessary end, Will come when it will come."
        };

        var exception = await Record.ExceptionAsync(async () =>
            await _dataStore.WriteAsync(entry.Id, entry, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task ShouldWriteAndReadToDataStore()
    {
        var entry = new TestDataEntry
        {
            Id = Guid.NewGuid(),
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"Cowards die many times before their deaths; The valiant never taste of death but once. 
                Of all the wonders that I yet have heard. It seems to me most strange that men should fear;
                Seeing that death, a necessary end, Will come when it will come."
        };

        var exception = await Record.ExceptionAsync(async () =>
            await _dataStore.WriteAsync(entry.Id, entry, CancellationToken.None));
        var retrievedEntry = await _dataStore.ReadAsync(entry.Id);

        Assert.Null(exception);
        Assert.NotNull(retrievedEntry);
        Assert.Equal(entry.Id, retrievedEntry?.Id);
        Assert.Equal(entry.FirstName, retrievedEntry?.FirstName);
        Assert.Equal(entry.LastName, retrievedEntry?.LastName);
        Assert.Equal(entry.Quote, retrievedEntry?.Quote);
    }

    [Fact]
    public async Task ShouldWriteAndDeleteToDataStore()
    {
        var entry = new TestDataEntry
        {
            Id = Guid.NewGuid(),
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"Cowards die many times before their deaths; The valiant never taste of death but once. 
                Of all the wonders that I yet have heard. It seems to me most strange that men should fear;
                Seeing that death, a necessary end, Will come when it will come."
        };

        await _dataStore.WriteAsync(entry.Id, entry, CancellationToken.None);
        var afterWrite = await _dataStore.ReadAsync(entry.Id);
        await _dataStore.DeleteAsync(entry.Id);
        var afterDelete = await _dataStore.ReadAsync(entry.Id);

        Assert.NotNull(afterWrite);
        Assert.Null(afterDelete);
    }

    [Fact]
    public async Task ShouldWriteAndDeleteAndRetrieveNullToDataStore()
    {
        var entry = new TestDataEntry
        {
            Id = Guid.NewGuid(),
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"Cowards die many times before their deaths; The valiant never taste of death but once. 
                Of all the wonders that I yet have heard. It seems to me most strange that men should fear;
                Seeing that death, a necessary end, Will come when it will come."
        };

        await _dataStore.WriteAsync(entry.Id, entry, CancellationToken.None);
        await _dataStore.DeleteAsync(entry.Id);
        var result = await _dataStore.ReadAsync(entry.Id);

        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldWriteAndUpdateToDataStore()
    {
        var id = Guid.NewGuid();

        var entry = new TestDataEntry
        {
            Id = id,
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"Cowards die many times before their deaths; The valiant never taste of death but once. 
                Of all the wonders that I yet have heard. It seems to me most strange that men should fear;
                Seeing that death, a necessary end, Will come when it will come."
        };

        var updateEntry = new TestDataEntry
        {
            Id = id,
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"But I am constant as the Northern Star,
                Of whose true fixed and resting quality
                There is no fellow in the firmament."
        };

        await _dataStore.WriteAsync(id, entry, CancellationToken.None);

        await _dataStore.UpdateAsync(entry.Id, updateEntry);
        var result = await _dataStore.ReadAsync(entry.Id, CancellationToken.None);

        Assert.Equal(updateEntry.Id, result?.Id);
        Assert.Equal(updateEntry.Quote, result?.Quote);
    }

    [Fact]
    public async Task ShouldTriggerAnExceptionWithNoRecordToDelete()
    {
        var id = Guid.NewGuid();
        var exception =
            await Record.ExceptionAsync(async () => await _dataStore.DeleteAsync(id, CancellationToken.None));

        Assert.NotNull(exception);
        Assert.Equal(typeof(StoreEntryNotFoundException), exception.GetType());
        Assert.Equal(StoreOperationTypeEnum.Delete, ((StoreEntryNotFoundException)exception).OperationType);
    }

    [Fact]
    public async Task Should_Return_Null_When_Reading_An_Id_That_Does_Not_Exist()
    {
        var id = Guid.NewGuid();
        var result = new TestDataEntry();
        var exception =
            await Record.ExceptionAsync(async () => result = await _dataStore.ReadAsync(id, CancellationToken.None));

        Assert.Null(exception);
        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldThrowAnExceptionWhenWritingAnExistingRecord()
    {
        var id = Guid.NewGuid();
        var entry = new TestDataEntry
        {
            Id = id,
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"Cowards die many times before their deaths; The valiant never taste of death but once. 
                Of all the wonders that I yet have heard. It seems to me most strange that men should fear;
                Seeing that death, a necessary end, Will come when it will come."
        };
        await _dataStore.WriteAsync(id, entry, CancellationToken.None);
        var exception =
            await Record.ExceptionAsync(async () => await _dataStore.WriteAsync(id, entry, CancellationToken.None));

        Assert.NotNull(exception);
        Assert.Equal(typeof(StoreDuplicateIdentifierException), exception.GetType());
    }

    [Fact]
    public async Task ShouldThrowAnExceptionWhenUpdatingARecordThatDoesNotExist()
    {
        var id = Guid.NewGuid();
        var entry = new TestDataEntry
        {
            Id = id,
            FirstName = "Julius",
            LastName = "Caesar",
            Quote =
                @"Cowards die many times before their deaths; The valiant never taste of death but once. 
                Of all the wonders that I yet have heard. It seems to me most strange that men should fear;
                Seeing that death, a necessary end, Will come when it will come."
        };

        var exception =
            await Record.ExceptionAsync(async () => await _dataStore.UpdateAsync(id, entry, CancellationToken.None));

        Assert.NotNull(exception);
        Assert.Equal(typeof(StoreEntryNotFoundException), exception.GetType());
        Assert.Equal(StoreOperationTypeEnum.Update, ((StoreEntryNotFoundException)exception).OperationType);
    }
}