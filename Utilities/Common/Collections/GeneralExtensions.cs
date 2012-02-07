using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Numerics;
using System.ComponentModel;
//using AllenCopeland.Abstraction.Utilities.Tuples;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Utilities.Collections
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class GeneralExtensions
    {
        private static readonly Random shuffleRandom = new Random();
        private static readonly Random shuffleRandomSecond = GetSecondRandom();

        private static Random GetSecondRandom()
        {
            Random r = new Random();
            return new Random(Environment.TickCount + r.Next());
        }
        public static IEnumerable<T> AddInline<T>(this IEnumerable<T> target, params T[] inlineElements)
        {
            foreach (var element in target)
                yield return element;
            foreach (var element in inlineElements)
                yield return element;
        }

        public static T[] AddInlineArray<T>(this IEnumerable<T> target, params T[] inlineElements)
        {
            return ArrayExtensions.MergeArrays(target.ToArray(), inlineElements);
        }
        /// <summary>
        /// Performs <paramref name="method"/> on all <typeparamref name="T"/> intances in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of members in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> which needs to have <paramref name="method"/> carried out on
        /// its entries.</param>
        /// <param name="method">A method with no return value that accepts values of <typeparamref name="T"/>.</param>
        public static void OnAll<T>(this IEnumerable<T> source, Action<T> method)
        {
            foreach (T t in source)
                method(t);
        }
        /// <summary>
        /// Performs <paramref name="method"/> on all <typeparamref name="T"/> 
        /// intances in <paramref name="source"/> using a prefetch of the
        /// elements to ensure that changes to the 
        /// collection during iteration do not halt the execution.
        /// </summary>
        /// <typeparam name="T">The type of members in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> which needs to
        /// have <paramref name="method"/> carried out on its entries.
        /// </param>
        /// <param name="method">A method with no return value that
        /// accepts values of <typeparamref name="T"/>.</param>
        public static void OnAllP<T>(this IEnumerable<T> source, Action<T> method)
        {
            foreach (T t in source.ToArray())
                method(t);
        }

        public static void OnAll<TItem, TArg1>(this IEnumerable<TItem> source, Action<TItem, TArg1> method, TArg1 arg1)
        {
            foreach (TItem t in source)
                method(t, arg1);
        }
        public static void OnAll<TItem, TArg1, TArg2>(this IEnumerable<TItem> source, Action<TItem, TArg1, TArg2> method, TArg1 arg1, TArg2 arg2)
        {
            foreach (TItem t in source)
                method(t, arg1, arg2);
        }
        public static void OnAll<TItem, TArg1, TArg2, TArg3>(this IEnumerable<TItem> source, Action<TItem, TArg1, TArg2, TArg3> method, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            foreach (TItem t in source)
                method(t, arg1, arg2, arg3);
        }

        public static IEnumerable<TCallResult> OnAll<TItem, TCallResult>(this IEnumerable<TItem> source, Func<TItem, TCallResult> method)
        {
            List<TCallResult> result = new List<TCallResult>();
            foreach (TItem t in source)
                result.Add(method(t));
            return result;
        }

        public static IEnumerable<TCallResult> OnAllP<TItem, TCallResult>(this IEnumerable<TItem> source, Func<TItem, TCallResult> method)
        {
            List<TCallResult> result = new List<TCallResult>();
            foreach (TItem t in source.ToArray())
                result.Add(method(t));
            return result;
        }

        public static IEnumerable<TCallResult> OnAll<TItem, TCallResult, TArg1>(this IEnumerable<TItem> source, Func<TItem, TArg1, TCallResult> method, TArg1 arg1)
        {
            List<TCallResult> result = new List<TCallResult>();
            foreach (TItem t in source)
                result.Add(method(t, arg1));
            return result;
        }

        public static IEnumerable<TCallResult> OnAll<TItem, TCallResult, TArg1, TArg2>(this IEnumerable<TItem> source, Func<TItem, TArg1, TArg2, TCallResult> method, TArg1 arg1, TArg2 arg2)
        {
            List<TCallResult> result = new List<TCallResult>();
            foreach (TItem t in source)
                result.Add(method(t, arg1, arg2));
            return result;
        }

        public static IEnumerable<TCallResult> OnAll<TItem, TCallResult, TArg1, TArg2, TArg3>(this IEnumerable<TItem> source, Func<TItem, TArg1, TArg2, TArg3, TCallResult> method, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            List<TCallResult> result = new List<TCallResult>();
            foreach (TItem t in source)
                result.Add(method(t, arg1, arg2, arg3));
            return result;
        }

        public static Tuple<TKey[], TValue[]> SplitKeyValueSets<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> set)
        {
            var copy = set.ToArray();
            TKey[] resultA = new TKey[copy.Length];
            TValue[] resultB = new TValue[copy.Length];
            for (int i = 0; i < copy.Length; i++)
            {
                var current = copy[i];
                resultA[i] = current.Key;
                resultB[i] = current.Value;
            }
            return new Tuple<TKey[], TValue[]>(resultA, resultB);
        }

        /// <summary>
        /// Creates a new <see cref="IDictionary{TKey, TValue}"/> with the keys of <paramref name="source"/>
        /// and values as yielded by <paramref name="valueGen"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the resulted <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
        /// <typeparam name="TValue">The type of values in the resulted <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
        /// <param name="source">The data source for the <typeparamref name="TKey"/> instances for the resulted <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="valueGen">The delegate to manage the translation of the keys to the values.</param>
        /// <returns>A new <see cref="IDictionary{TKey, TValue}"/> with the elements of <paramref name="source"/> the results of the 
        /// </returns>
        public static IDictionary<TKey, TValue> ToDictionaryAlt<TKey, TValue>(this IEnumerable<TKey> source, Func<TKey, TValue> valueGen)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            else if (valueGen == null)
                throw new ArgumentNullException("valueGen");
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            //Lambda expressions seem allow for light obfuscation?
            source.OnAll((tk, target, method) => target.Add(tk, method(tk)), result, valueGen);
            return result;
        }
        
        public static T[] Filter<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            //'Where<T>(IEnumerable<T> x, Func<T, bool> predicate)' is the same.
            List<T> result = new List<T>();
            foreach (T t in source)
                if (predicate(t))
                    result.Add(t);
            return result.ToArray();
        }

        public static bool CompareSeriesTo<TSourceItem, TTargetItem>(this IEnumerable<TSourceItem> source, IEnumerable<TTargetItem> target, Func<TSourceItem, TTargetItem, bool> predicate)
        {
            bool mv1 = false,
                 mv2 = false;
            IEnumerator<TSourceItem> e1 = source.GetEnumerator();
            IEnumerator<TTargetItem> e2 = target.GetEnumerator();

            for (mv1 = e1.MoveNext(), mv2 = e2.MoveNext(); mv1 && mv2; mv1 = e1.MoveNext(), mv2 = e2.MoveNext())
                if (!(predicate(e1.Current, e2.Current)))
                    return false;
            //If they're both finished, instead of just one.
            return mv1 == mv2;
        }

        public static bool CompareSeriesTo<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> predicate)
        {
            bool mv1 = false,
                 mv2 = false;
            if (source.Count() != target.Count())
                return false;
            IEnumerator<T> e1 = source.GetEnumerator(),
                           e2 = target.GetEnumerator();
            for (mv1 = e1.MoveNext(), mv2 = e2.MoveNext(); mv1 && mv2; mv1 = e1.MoveNext(), mv2 = e2.MoveNext())
                if (!(predicate(e1.Current, e2.Current)))
                    return false;
            return mv1 == mv2;
        }

        /// <summary>
        /// Simplifies the generic <see cref="ICollection{T}"/> into a simpler <see cref="ICollection"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items in <paramref name="higher"/>.</typeparam>
        /// <param name="higher">The <see cref="ICollection{T}"/> to simplify.</param>
        /// <returns>A new <see cref="ICollection"/> implementation that encapsulates the <paramref name="higher"/>.</returns>
        public static ICollection Simplify<T>(this ICollection<T> higher)
        {
            return new SimpleCollection<T>(higher);
        }

        public static Dictionary<TKey, TValue> Copy<TKey, TValue>(this Dictionary<TKey, TValue> target)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> kvp in target)
                result.Add(kvp.Key, kvp.Value);
            return result;
        }

        public static IEnumerable<int> Range(this int source)
        {
            for (int i = 0; i < source; i++)
                yield return i;
            yield break;
        }

        public static IEnumerable<int> RangeTo(this int source, int exclusiveTo)
        {
            for (int i = source; i < exclusiveTo; i++)
                yield return i;
            yield break;
        }
        public static T[] GetRigidArray<T>(params T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            return array;
        }

        public static string ConvertToString(this byte[] source)
        {
            if (source.Length % 2 != 0)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.source, ExceptionMessageId.SourceStreamInvalidLength);

            char[] r = new char[source.Length / 2];
            for (int i = 0, i2 = 0; i < r.Length; i++, i2 += 2)
                r[i] = ((char)((((int)source[i2]) << 8) + (int)source[i2 + 1]));
            return new string(r);
        }

        /// <summary>
        /// Takes the <typeparamref name="T"/> elements from the <paramref name="target"/>
        /// from the <paramref name="start"/> and ending after the <paramref name="count"/>
        /// is reached or until the <paramref name="target"/> is expended.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The <see cref="IEnumerable{T}"/>
        /// to obtain the elements from.</param>
        /// <param name="start">The zero-based index of the point to start.</param>
        /// <param name="count">The number of elements to retrieve.</param>
        /// <returns></returns>
        public static IEnumerable<T> Take<T>(this IEnumerable<T> target, int start, int count)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (start < 0)
                throw new ArgumentOutOfRangeException("start");
            int index = 0;
            int curCount = 0;
            foreach (T item in target)
                if (index++ >= start)
                {
                    if (curCount < count)
                    {
                        curCount++;
                        yield return item;
                    }
                    else
                        yield break;
                }
        }
        
        /// <summary>
        /// Obtains the index of the <paramref name="element"/>
        /// in the <paramref name="target"/> <see cref="IEnumerable&lt;T&gt;"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the enumerable 
        /// <paramref name="target"/>.</typeparam>
        /// <param name="target">The <see cref="IEnumerable{T}"/> instance to iterate
        /// through.</param>
        /// <param name="element">The <typeparamref name="T"/> element
        /// to find in the <paramref name="target"/>.</param>
        /// <returns>The <see cref="Int32"/> value relative to the zero-based
        /// index of the element in the <paramref name="target"/>
        /// <see cref="IEnumerable{T}"/>.</returns>
        public static int GetIndexOf<T>(this IEnumerable<T> target, T element)
            where T :
                class
        {
            int index = 0;
            foreach (T t in target){
                if (t.Equals(element))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Returns the number of sequential incidents of <paramref name="characters"/>
        /// within <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> to search within.</param>
        /// <param name="characters">The <see cref="String"/> of characters to count
        /// the incidents of within <paramref name="value"/>.</param>
        /// <returns>A <see cref="Int32"/> value denoting the number of occurrences
        /// of <paramref name="characters"/> within
        /// <paramref name="value"/>.</returns>
        public static int Count(this string value, string characters)
        {
            int count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                bool match = true;
                if (i + (characters.Length - 1) >= value.Length)
                    break;
                for (int j = 0; j < characters.Length; j++)
                {
                    if (value[i + j] != characters[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (!match)
                    continue;
                i += characters.Length - 1;
                count++;
            }
            return count;
        }

        public static int Count(this string value, char character)
        {
            int count=0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == character)
                    count++;
            }
            return count;
        }

        public static IDictionary<TKey, TValue> ToDictionary<TItem, TKey, TValue>(
            this IEnumerable<TItem> series, Func<TItem, Tuple<TKey, TValue>> selector)
        {
            IDictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            foreach (var item in series)
            {
                var value = selector(item);
                result.Add(value.Item1, value.Item2);
            }
            return result;
        }

        public static T[] ConcatinateSeries<T>(this T[][] series)
        {
            int count = 0;
            for (int i = 0; i < series.Length; )
                count += series[i++].Length;
            T[] result = new T[count];
            for (int i = 0, k = 0; i < series.Length; i++)
                for (int j = 0; j < series[i].Length; j++, k++)
                    result[k] = series[i][j];
            return result;
        }

        public static IEnumerable<T> Concatinate<T>(this T[][] series)
        {
            return from array in series
                   from element in array
                   select element;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> target)
        {
            return from t in target
                   orderby shuffleRandom.Next() descending,
                           shuffleRandomSecond.Next() ascending
                   select t;
        }

        public static IReadOnlyCollection<TLower> GetCovariant<TLower, THigher>(this IReadOnlyCollection<THigher> source)
            where THigher :
                TLower
        {
            return new CovariantReadOnlyCollection<TLower, THigher>(source);
        }

    }
}
