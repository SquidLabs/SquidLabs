using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Application.Mapper;

public interface IMapper<TKey, TDomainObject, TDataStoreRecord>
    where TDomainObject : IDomainObject<TKey>
{
    TDataStoreRecord ConvertToDataStoreType(TDomainObject domainObject);
    TDomainObject ConvertFromDataStoreType(TDataStoreRecord dataStoreRecord);
}