namespace SquidLabs.Linq.Extensions;

public static class Linq
{
    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> list, int parts)
    {
        var i = 0;
        return list.GroupBy(g => i++ % parts).Select(p => p.AsEnumerable());
    }

    public static IEnumerable<T> Compact<T>(this IEnumerable<T> source)
    {
        return source.Where(IsTruthy);
    }

    private static bool IsTruthy<T>(T item)
    {
        // Handle null
        if (item == null) return false;

        // Handle booleans
        if (item is bool b) return b;

        // Handle numeric types (0 is falsy)
        if (item is IConvertible convertible)
            try
            {
                if (convertible.ToDouble(null) == 0) return false;
            }
            catch
            {
                // Ignore exceptions (e.g., invalid cast)
            }

        // Handle strings (empty or whitespace is falsy)
        if (item is string s) return !string.IsNullOrWhiteSpace(s);

        // Default to truthy for other types
        return true;
    }

    public static IEnumerable<T> Fill<T>(this IEnumerable<T> source, T value, int start = 0, int? count = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (start < 0) throw new ArgumentOutOfRangeException(nameof(start), "Start index cannot be negative.");

        var index = 0;
        foreach (var item in source)
        {
            if (index >= start && (!count.HasValue || index < start + count))
                yield return value;
            else
                yield return item;
            index++;
        }
    }

    public static (IEnumerable<T> Truthy, IEnumerable<T> Falsy) Partition<T>(this IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        var truePart = source.Where(predicate);
        var falsePart = source.Where(item => !predicate(item));

        return (truePart, falsePart);
    }
}