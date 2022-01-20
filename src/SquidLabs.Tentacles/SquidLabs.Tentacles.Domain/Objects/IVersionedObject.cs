namespace SquidLabs.Tentacles.Domain.Objects;

public interface IVersionedObject
{
    public int VersionId { get; set; }
}