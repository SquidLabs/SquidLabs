namespace SquidLabs.Tentacles.Infrastructure;

public interface IStreamStore<TEventData>
{ 
     Task AppendAsync(TEventData eventData);
     Task<IEnumerable<TEventData>> ReadAsync(int version = 0);
     Task DeleteAsync();
}