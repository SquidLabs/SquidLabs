namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IStoreNameFormatter
{
    string Collection<T>()
        where T : class;
}