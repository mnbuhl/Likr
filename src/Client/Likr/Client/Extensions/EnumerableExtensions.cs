namespace Likr.Client.Extensions;

public static class EnumerableExtensions
{
    public static IList<T> OrEmptyIfNull<T>(this IList<T> source)
    {
        return source;
    }
}