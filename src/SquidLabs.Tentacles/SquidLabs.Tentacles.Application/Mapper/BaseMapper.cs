using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Application.Mapper;

public abstract class BaseMapper<TDomainObject, TDataStoreRecord> : IMapper<Guid, TDomainObject, TDataStoreRecord>
    where TDomainObject : IDomainObject<Guid>
{
    public abstract TDataStoreRecord ConvertToDataStoreType(TDomainObject domainObject);
    public abstract TDomainObject ConvertFromDataStoreType(TDataStoreRecord dataStoreRecord);
}