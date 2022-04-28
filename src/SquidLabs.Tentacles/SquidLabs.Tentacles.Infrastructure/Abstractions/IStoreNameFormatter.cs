namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

public interface IStoreNameFormatter
{
    string Collection<T>()
        where T : class;
}