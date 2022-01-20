namespace SquidLabs.Linq.Extensions;

public static class Linq
{
    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
    {
        var i = 0;
        return list.GroupBy(g => i++ % parts).Select(p => p.AsEnumerable());
    }
}