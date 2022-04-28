using SquidLabs.Tentacles.Domain.Objects;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Application.Mapper;

public interface IMapper<TKey, TDomainObject, TDataRecord>
    where TDomainObject : IDomainObject<TKey>
    where TDataRecord : IDataEntry
{
    TDataRecord? ConvertToDataStoreType(TDomainObject? domainObject);
    TDomainObject? ConvertFromDataStoreType(IDataEntry? dataStoreEntry);
}