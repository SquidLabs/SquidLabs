using SquidLabs.Tentacles.Domain.Objects;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Application.Mapper;

public interface IMapper<TKey, TDomainObject, TDataRecord>
    where TDomainObject : IDomainObject<TKey>
    where TDataRecord : IDataEntry
{
    /// <summary>
    /// </summary>
    /// <param name="domainObject"></param>
    /// <returns></returns>
    TDataRecord? ConvertToDataStoreType(TDomainObject? domainObject);

    /// <summary>
    /// </summary>
    /// <param name="dataStoreEntry"></param>
    /// <returns></returns>
    TDomainObject? ConvertFromDataStoreType(IDataEntry? dataStoreEntry);
}