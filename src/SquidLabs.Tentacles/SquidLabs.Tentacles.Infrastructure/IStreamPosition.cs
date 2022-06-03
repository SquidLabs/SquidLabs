namespace SquidLabs.Tentacles.Infrastructure.Abstractions;

public interface IStreamPosition : IEquatable<IStreamPosition>, IComparable<IStreamPosition>, IComparable
{
    public static IStreamPosition Start { get; } = null!;
    public static IStreamPosition End { get; } = null!;

    public static IStreamPosition operator ++(IStreamPosition position)
    {
        throw new NotImplementedException();
    }

    public IStreamPosition Next();
}