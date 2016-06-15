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

        public static IEnumerable<Tuple<T, T>> EnumerateWith<T>(this IEnumerable<T> leftSeries, IEnumerable<T> rightSeries, bool throwIfInequalLength = true)
        {
            using (var leftEnumerator = leftSeries.GetEnumerator())
            using (var rightEnumerator = rightSeries.GetEnumerator())
            {
                bool leftContainsItem,
                     rightContainsItem;
                while ((leftContainsItem = leftEnumerator.MoveNext()) & (rightContainsItem = rightEnumerator.MoveNext()))
                    yield return Tuple.Create(leftEnumerator.Current, rightEnumerator.Current);
                if (leftContainsItem && throwIfInequalLength)
                    throw new InequalSeriesException("leftSeries is longer than rightSeries.");
                else if (rightContainsItem && throwIfInequalLength)
                    throw new InequalSeriesException("rightSeries is longer than leftSeries.");
            }
        }
        public static IEnumerable<IEnumerable<T>> GenerateVariations<T>(IEnumerable<T> series)
        {
            return GenerateVariationsInternal(series).Distinct(SeriesComparer<T>.Singleton);
        }

        private static IEnumerable<IEnumerable<T>> GenerateVariationsInternal<T>(IEnumerable<T> series)
        {
            var seriesCopy = series.ToArray();
            for (int i = 0; i < seriesCopy.Length; i++)
            {
                for (int j = i + 1; j <= seriesCopy.Length; j++)
                    foreach (var elements in GetSeriesVariant(seriesCopy.Skip(i).Take(1), series.Skip(j)))
                        yield return elements;
            }
            if (seriesCopy.Length == 1)
                yield return new[] { seriesCopy[0] };
        }

        private static IEnumerable<IEnumerable<T>> GetSeriesVariant<T>(IEnumerable<T> leadIn, IEnumerable<T> tail)
        {
            if (tail.Any())
                foreach (var element in GenerateVariations(tail))
                    yield return leadIn.Concat(element);
            else
                yield return leadIn;
        }

        /// <summary>Obtains an ordered series of variations of the <paramref name="series"/> provided based on all possible permutations of the said set.</summary>
        /// <typeparam name="T">The type of elements within the set which are orderable.</typeparam>
        /// <param name="series">The <see cref="IEnumerable{T}"/> to derive the sets of variations.</param>
        /// <returns>An ordered series of variations of the <paramref name="series"/> provided based on all possible permutations of the said set.</returns>
        public static IEnumerable<IEnumerable<T>> GenerateOrderedVariations<T>(IEnumerable<T> series)
            where T :
                IComparable<T>
        {
            return GenerateVariationsInternal(series).Select(k=>k.OrderBy(j=>j).ToArray()).Distinct(SeriesComparer<T>.Singleton);
        }

        private class SeriesComparer<T> :
          IEqualityComparer<IEnumerable<T>>
        {
            public static SeriesComparer<T> Singleton = new SeriesComparer<T>();

            public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(IEnumerable<T> obj)
            {
                return obj.Count();
            }
        }

    }
    public class InequalSeriesException :
        Exception
    {
        public InequalSeriesException(string message)
            : base(message) { }
    }


}
