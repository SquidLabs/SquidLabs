namespace SquidLabs.Tentacles.Infrastructure.DataStore.Abstractions;

public interface IConnectionStringOptions<T>
{
    public T ConnectionString { get; set; }
}