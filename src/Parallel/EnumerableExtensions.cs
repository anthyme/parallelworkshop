using System;
using System.Collections.Generic;

namespace Parallel
{
    public static class EnumerableExtensions
    {
        public static void Iter<T>(this IEnumerable<T> source, Action<T> action = null)
        {
            foreach (var element in source)
            {
                action?.Invoke(element);
            }
        }
    }
}
