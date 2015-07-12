using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Obtains an enumerable object which iterates over the <paramref name="target"/>
        /// while caching the results, executing the enumerator on <paramref name="target"/>
        /// only once.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The <see cref="IEnumerable{T}"/>
        /// series on which to pass over once, then use the cached results.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> which will iterate over the results
        /// once and yield the cached results subsequent passes.</returns>
        public static IEnumerable<T> SinglePass<T>(this IEnumerable<T> target)
        {
            return new SinglePassEnumerable<T>(target);
        }

        public static IEnumerable<T> OddIndices<T>(this IEnumerable<T> series)
        {
            return series.Where((element, index) => index % 2 != 0);
        }

        public static IEnumerable<T> EvenIndices<T>(this IEnumerable<T> series)
        {
            return series.Where((element, index) => index % 2 == 0);
        }
    }
}
