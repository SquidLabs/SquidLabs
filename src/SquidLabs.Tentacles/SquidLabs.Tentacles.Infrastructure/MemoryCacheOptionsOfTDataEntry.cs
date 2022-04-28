using Microsoft.Extensions.Caching.Memory;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure;

public class MemoryCacheOptionsOfTDataEntry<TDataEntry> : MemoryCacheOptions, IConnectionOptions<TDataEntry>
    where TDataEntry : IDataEntry
{
}