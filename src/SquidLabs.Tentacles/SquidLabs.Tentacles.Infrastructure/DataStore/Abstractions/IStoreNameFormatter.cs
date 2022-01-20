namespace SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

public interface IStoreNameFormatter
{
    string Collection<T>()
        where T : class;
}