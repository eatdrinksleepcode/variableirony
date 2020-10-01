using System.Collections.Generic;

namespace VariableIrony
{
    public static class IListExtensions
    {
        public static int IndexOf<T>(this IList<T> source, T item)
        {
            return source.IndexOf(item, 0, source.Count, EqualityComparer<T>.Default);
        }

        public static int IndexOf<T>(this IList<T> source, T item, IEqualityComparer<T> comparer)
        {
            return source.IndexOf(item, 0, source.Count, comparer);
        }

        public static int IndexOf<T>(this IList<T> source, T item, int startIndex, int count)
        {
            return source.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
        }

        public static int IndexOf<T>(this IList<T> source, T item, int startIndex, int count, IEqualityComparer<T> comparer)
        {
            var num = startIndex + count;
            for (var i = startIndex; i < num; i++)
            {
                if (comparer.Equals(source[i], item))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
