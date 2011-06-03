using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Common;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
#if CODE_ANALYSIS
/* *
 * Namespace is used to expose a singular type which emits 
 * method signatures as extension methods, and therefore
 * violates this rule by design.  The intent is you only add
 * a using directive for the namespace if you need the extension
 * method functionality.
 * */
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AllenCopeland.Abstraction.Utilities.Arrays")]
#endif
namespace AllenCopeland.Abstraction.Utilities.Arrays
{
    /// <summary>
    /// Provides methods that give small case changes to arrays.
    /// </summary>
    public static partial class Tweaks
    {
        public static T[] Add<T>(this T[] target, params T[] elements)
        {
            return MergeArrays(target, elements);
        }
        public static T[] AddBefore<T>(this T[] target, params T[] elements)
        {
            return MergeArrays(elements, target);
        }
        /// <summary>
        /// Casts an array from the <typeparamref name="TOriginatingType"/> to the <typeparamref name="TDesiredType"/>
        /// </summary>
        /// <typeparam name="TDesiredType">The desired type of members in the array.</typeparam>
        /// <typeparam name="TOriginatingType">The originating type of the members in the array.</typeparam>
        /// <param name="array">The array of members to cast.</param>
        /// <returns>An array of the desired type of members.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="System.InvalidCastException">A member from <paramref name="array"/> could not be cast to the <typeparamref name="TDesiredType"/>.</exception>
        #if CODE_ANALYSIS
        /* *
         * Suppressed Type-Parameter inference code-analysis point.
         * This is intended to be an explicit cast, the to and from types should be 
         * expressly known.
         * */
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        #endif
        public static TDesiredType[] Cast<TDesiredType, TOriginatingType>(this TOriginatingType[] array)
        {
            return TranslateArray<TOriginatingType, TDesiredType>
                (
                    array,
                    item =>
                    {
                        if (item is TDesiredType)
                            return (TDesiredType)(object)item;
                        else
                            throw new InvalidCastException("A member of the source array cannot be cast to the TDesiredType");
                    }
                );
        }

        #if CODE_ANALYSIS
        /* *
         * Suppressed Type-Parameter inference code-analysis point.
         * This is intended to be an explicit cast, the to and from types should be 
         * expressly known.
         * */
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        #endif
        public static TDesiredType[] Cast<TDesiredType>(this object[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            TDesiredType[] r = new TDesiredType[array.Length];
            for (int i = 0; i < array.Length; i++)
                r[i] = (TDesiredType)(array[i]);
            return r;
        }


        /// <summary>
        /// Casts an array from the <typeparamref name="TOriginatingType"/> to the <typeparamref name="TDesiredType"/>; truncated
        /// by the number of members that are not of the <typeparamref name="TDesiredType"/>,
        /// </summary>
        /// <typeparam name="TDesiredType">The desired type of members in the array.</typeparam>
        /// <typeparam name="TOriginatingType">The originating type of the members in the array.</typeparam>
        /// <param name="array">The array of members to cast.</param>
        /// <returns>An array of members which are valid instances of <typeparamref name="TDesiredType"/>,
        /// other members being skipped.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        #if CODE_ANALYSIS
        /* *
         * Suppressed Type-Parameter inference code-analysis point.
         * This is intended to be an explicit cast, the to and from types should be 
         * expressly known.
         * */
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        #endif
        public static TDesiredType[] SlashCastArray<TDesiredType, TOriginatingType>(TOriginatingType[] array)
        {
            return TranslateFilteredArray<TOriginatingType, TDesiredType>(
                array, 
                k => k is TDesiredType, 
                k => (TDesiredType)(object)k);
        }
        /// <summary>
        /// Logically filters an array using the iteration-logic delegate provided.
        /// </summary>
        /// <typeparam name="TItem">The type of item to filter.</typeparam>
        /// <param name="items">The array of items to filter.</param>
        /// <param name="logicDelegate">The delegate which will provide the iteration
        /// logic to perform the filtering.</param>
        /// <returns>A list of the items filtered by the means provided by <paramref name="logicDelegate"/>.</returns>
        public static TItem[] FilterArray<TItem>(TItem[] items, Predicate<TItem> logicDelegate)
        {
            List<TItem> resultItems = new List<TItem>(items.Length);
            foreach (TItem item in items)
                if (logicDelegate(item))
                    resultItems.Add(item);
            resultItems.TrimExcess();
            return resultItems.ToArray();
        }

        /// <summary>
        /// Alters the contents of the array by the return values indicated by the <paramref name="alterDelegate"/>.
        /// </summary>
        /// <typeparam name="TSourceItem">The type of items to alter in an array.</typeparam>
        /// <typeparam name="TDestinationItem">The type of items to return in the result array.</typeparam>
        /// <param name="items">The array to alter</param>
        /// <param name="alterDelegate">The delegate to perform the change on the elements
        /// of <paramref name="items"/>.</param>
        /// <returns></returns>
        #if CODE_ANALYSIS
        /* *
         * Suppressed Type-Parameter inference code-analysis point.
         * Current languages do not support outgoing type as a point of inference.
         * */
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        #endif
        public static TDestinationItem[] TranslateArray<TSourceItem, TDestinationItem>(TSourceItem[] items, Func<TSourceItem, TDestinationItem> alterDelegate)
        {
            var resultItems = new List<TDestinationItem>(items.Length);
            foreach (var item in items)
                resultItems.Add(alterDelegate(item));
            return resultItems.ToArray();
        }
        /// <summary>
        /// Alters the contents of the array by the <see cref="Func{T, TResult}"/>
        /// <paramref name="translator"/> and ignores others based upon the 
        /// <see cref="Predicate{T}"/> <paramref name="filter"/>.
        /// </summary>
        /// <typeparam name="TSourceItem">The type of the parameter accepted as input.</typeparam>
        /// <typeparam name="TDestinationItem">The type of result that is expected.</typeparam>
        /// <param name="items">The array to be logically and data-wise altered.</param>
        /// <param name="filter">The delegate that determines which elements of <paramref name="items"/> are
        /// kept in the result array.</param>
        /// <param name="translator">The delegate that alters the value of the elements in <paramref name="items"/>.</param>
        /// <returns>A new array that contains the specific items transformed as desired by the two iteration logic delegates.</returns>
        #if CODE_ANALYSIS
        /* *
         * Suppressed Type-Parameter inference code-analysis point.
         * Current languages do not support outgoing type as a point of inference.
         * */
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        #endif
        public static TDestinationItem[] TranslateFilteredArray<TSourceItem, TDestinationItem>
            (TSourceItem[] items, 
            Predicate<TSourceItem> filter,
            Func<TSourceItem, TDestinationItem> translator)
        {
            TDestinationItem[] result = new TDestinationItem[items.Length];
            //Setup space.
            int skipped = 0;
            for (int i = 0, c = items.Length; i < c; i++)
            {
                var currentItem = items[i];
                if (filter(currentItem))
                    result[i] = translator(currentItem);
                else
                    skipped++;
            }
            if (skipped == 0)
                return result;
            else
            {
                var temp = new TDestinationItem[result.Length - skipped];
                Array.Copy(result, temp, temp.Length);
                return temp;
            }
        }
        #if CODE_ANALYSIS
        /* *
         * Suppressed Type-Parameter inference code-analysis point.
         * Redesign not possible.
         * */
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        #endif
        public static T[] MergeArrays<T>(params T[][] series)
        {
            if (series == null)
                throw new ArgumentNullException("series");
            int fullLength = 0;
            int[] starts = new int[series.Length];
            for (int i = 0; i < series.Length; i++)
                if (series[i] == null)
                    throw new ArgumentException("A member of series was null", "series");
                else
                {
                    starts[i] = fullLength;
                    fullLength += series[i].Length;
                }
            T[] result = new T[fullLength];
            Parallel.For(0, starts.Length, i =>
                {
                    series[i].CopyTo(result, starts[i]);
                });
            //for (int i = 0, offset = 0; i < series.Length && offset < fullLength; offset += series[i++].Length)
            //    series[i].CopyTo(result, offset);
            return result;
        }
        /// <summary>
        /// Obtains an enumerable object which can iterate through all of
        /// the indices of an <see cref="Array"/> regardless of its 
        /// dimensional complexity.
        /// </summary>
        /// <param name="array">The <see cref="Array"/> to perform
        /// iteration on.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> object that
        /// yields a single-dimensional array per iteration relative
        /// to the <paramref name="array"/> provided.</returns>
        /// <remarks><para>The number of values in the resultant array,
        /// per iteration, is equivalent to the
        /// <see cref="Array.Rank"/> of the 
        /// <paramref name="array"/> provided.</para>
        /// <para>Due to the nature this method was intended to be used,
        /// the array retrieved per iteration is the same so it is not
        /// guaranteed to be the same on a much later access
        /// should its reference be stored, and the iteration
        /// continued.</para></remarks>
        public static IEnumerable<int[]> Iterate(this Array array)
        {
            return array.Iterate(null);
        }

        public static IEnumerable<int[]> Iterate(this Array array, Action<int, bool> onBoundsIncrement)
        {
            int[] indices;
            int rank = array.Rank;
            if (rank == 1)
            {
                /* *
                 * Simple answer for one dimension
                 * */
                indices = new int[] { array.GetLowerBound(0) }; 
                for (; indices[0] <= array.GetUpperBound(0); indices[0]++)
                    yield return indices;
            }
            else
            {
                /* *
                 * Multi-dimensional, or non-vector, arrays are a bit different.
                 * */
                indices = new int[array.Rank];
                /* *
                 * Obtain the upper/lower bounds..
                 * */
                int[] upperBounds = new int[array.Rank];

                for (int i = 0; i < rank; i++)
                {
                    indices[i] = array.GetLowerBound(i);
                    upperBounds[i] = array.GetUpperBound(i);
                }

                int[] lowerBounds = (int[])indices.Clone();
                bool finished = false;
                int topRank = rank - 1;
            Repeater:
                {
                    /* *
                     * Nifty thing is... it's always the same array,
                     * which means there's no performance hit for
                     * creating and returning new arrays, because we don't.
                     * */
                    yield return indices;
                    /* *
                     * Move through the dimensions, starting 
                     * with the highest-order.
                     * */

                    for (int i = topRank; i >= 0; i--)
                    {
                        /* *
                         * Index the current dimension...
                         * */
                        indices[i]++;
                        /* *
                         * If the current dimension is in range
                         * we're done.
                         * */
                        if (indices[i] <= upperBounds[i])
                            break;
                        if (i == topRank)
                        {
                            finished = true;
                            for (int j = 0; j < rank; j++)
                                if (indices[j] < upperBounds[j])
                                {
                                    finished = false;
                                    break;
                                }
                        }
                        /* *
                         * Reset the current index, the loop
                         * will continue until all 'overflows' 
                         * on the array are hit and reset 
                         * accordingly.
                         * */
                        indices[i] = lowerBounds[i];
                        /* *
                         * Instruct the listener that the bounds for
                         * a given rank have been reached, and thus
                         * flushed back to the initial point.
                         * *
                         * When the rank that's incremented is zero, 
                         * the iteration is finished.
                         * */
                        if (onBoundsIncrement != null)
                            onBoundsIncrement(i, finished);
                        /* *
                         * If the first dimension has been incremented
                         * and exceeded the high point of the dimension
                         * exit stage left.
                         * */
                        if (i == 0)
                            yield break;
                    }

                    goto Repeater;
                }
            }
            yield break;
        }

        /// <summary>
        /// Copies data from the <paramref name="source"/> two-dimensional array to the
        /// <paramref name="destination"/> two-dimensional array.
        /// </summary>
        /// <typeparam name="T">The kind of element used within the arrays
        /// copied.</typeparam>
        /// <param name="source">The two-dimensional array from which copying occurs.</param>
        /// <param name="destination">The two-dimensional array of <typeparamref name="T"/>
        /// elements in which the elements are received.</param>
        /// <param name="lengthA">The <see cref="Int32"/>
        /// value representing the lower-order dimension's size.</param>
        /// <param name="lengthB">The <see cref="Int32"/>
        /// value representing the higher-order dimension's size.</param>
        public static void BlockCopy<T>(this T[,] source, T[,] destination, int lengthA, int lengthB)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            int upperSA = source.GetUpperBound(0),
                upperSB = source.GetUpperBound(1);
            int upperDA = destination.GetUpperBound(0),
                upperDB = destination.GetUpperBound(1);
            int lowerSA = source.GetLowerBound(0),
                lowerSB = source.GetLowerBound(1);
            int lowerDA = destination.GetLowerBound(0),
                lowerDB = destination.GetLowerBound(1);
            int lengthSA = upperSA + 1 - lowerSA,
                lengthSB = upperSB + 1 - lowerSB;
            int lengthDA = upperDA + 1 - lowerDA,
                lengthDB = upperDB + 1 - lowerDB;
            if (lengthA > lengthSA || lengthA > lengthDA)
                throw new ArgumentOutOfRangeException("lengthA");
            if (lengthB > lengthSB || lengthB > lengthDB)
                throw new ArgumentOutOfRangeException("lengthB");
            lock (source)
            {
                Parallel.For(0, lengthA, outterIndex =>
                {
                    /* *
                     * Point of diminishing returns, the innermost 
                     * array is best served to be given a normal loop as the 
                     * tasking of the parallel elements takes a greater
                     * overhead than the loop itself does.
                     * */
                    for (int innerIndex = 0; innerIndex < lengthB; innerIndex++)
                        destination[lowerDA + outterIndex, lowerDB + innerIndex] = source[lowerSA + outterIndex, lowerSB + innerIndex];
                });
            }
        }

        public static T[,] Resize<T>(this T[,] source, int newLengthA, int newLengthB)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            int upperSA = source.GetUpperBound(0),
                upperSB = source.GetUpperBound(1);
            int lowerSA = source.GetLowerBound(0),
                lowerSB = source.GetLowerBound(1);

            T[,] result = (T[,])(Array.CreateInstance(typeof(T), new int[] { newLengthA, newLengthB }, new int[] { lowerSA, lowerSB }));
            int lengthSA = upperSA + 1 - lowerSA,
                lengthSB = upperSB + 1 - lowerSB;
            int copyMaxA = Math.Min(newLengthA, lengthSA),
                copyMaxB = Math.Min(newLengthB, lengthSB);
            source.BlockCopy(result, copyMaxA, copyMaxB);
            return result;
        }

        /// <summary>
        /// Copies data from the <paramref name="source"/> three-dimensional array
        /// to the <paramref name="destination"/> three-dimensional array.
        /// </summary>
        /// <typeparam name="T">The kind of element used within the arrays
        /// copied.</typeparam>
        /// <param name="source">The three-dimensional array from which copying occurs.</param>
        /// <param name="destination">The three-dimensional array of <typeparamref name="T"/>
        /// elements in which the elements are received.</param>
        /// <param name="lengthA">The <see cref="Int32"/>
        /// value representing the size of the lowest order dimension
        /// of the copy operation.</param>
        /// <param name="lengthB">The <see cref="Int32"/>
        /// value representing the size of the middle order dimension
        /// of the copy operation.</param>
        /// <param name="lengthC">The <see cref="Int32"/>
        /// value representing the size of the highest order dimension
        /// of the copy operation.</param>
        public static void CubicCopy<T>(this T[, ,] source, T[, ,] destination, int lengthA, int lengthB, int lengthC)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            int upperSA = source.GetUpperBound(0),
                upperSB = source.GetUpperBound(1),
                upperSC = source.GetUpperBound(2);
            int upperDA = destination.GetUpperBound(0),
                upperDB = destination.GetUpperBound(1),
                upperDC = destination.GetUpperBound(2);
            int lowerSA = source.GetLowerBound(0),
                lowerSB = source.GetLowerBound(1),
                lowerSC = source.GetLowerBound(2);
            int lowerDA = destination.GetLowerBound(0),
                lowerDB = destination.GetLowerBound(1),
                lowerDC = destination.GetLowerBound(2);
            int lengthSA = upperSA + 1 - lowerSA,
                lengthSB = upperSB + 1 - lowerSB,
                lengthSC = upperSC + 1 - lowerSC;
            int lengthDA = upperDA + 1 - lowerDA,
                lengthDB = upperDB + 1 - lowerDB,
                lengthDC = upperDC + 1 - lowerDC;
            if (lengthA > lengthSA || lengthA > lengthDA)
                throw new ArgumentOutOfRangeException("lengthA");
            if (lengthB > lengthSB || lengthB > lengthDB)
                throw new ArgumentOutOfRangeException("lengthB");
            if (lengthC > lengthSC || lengthC > lengthDC)
                throw new ArgumentOutOfRangeException("lengthC");
            lock (source)
            {
                Parallel.For(0, lengthA, outterIndex =>
                {
                    Parallel.For(0, lengthB, middleIndex =>
                    {
                        for (int innerIndex = 0; innerIndex < lengthC; innerIndex++)
                            destination[lowerDA + outterIndex, lowerDB + middleIndex, lowerDC + innerIndex] = source[lowerSA + outterIndex, lowerSB + middleIndex, lowerSC + innerIndex];
                    });
                });
            }

        }

        public static T[, ,] Resize<T>(this T[, ,] source, int newLengthA, int newLengthB, int newLengthC)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            int upperSA = source.GetUpperBound(0),
                upperSB = source.GetUpperBound(1),
                upperSC = source.GetUpperBound(2);
            int lowerSA = source.GetLowerBound(0),
                lowerSB = source.GetLowerBound(1),
                lowerSC = source.GetLowerBound(2);

            T[, ,] result = (T[, ,])(Array.CreateInstance(typeof(T), new int[] { newLengthA, newLengthB, newLengthC }, new int[] { lowerSA, lowerSB, lowerSC }));
            int lengthSA = upperSA + 1 - lowerSA,
                lengthSB = upperSB + 1 - lowerSB,
                lengthSC = upperSC + 1 - lowerSC;
            int copyMaxA = Math.Min(newLengthA, lengthSA),
                copyMaxB = Math.Min(newLengthB, lengthSB),
                copyMaxC = Math.Min(newLengthC, lengthSC);
            source.CubicCopy(result, copyMaxA, copyMaxB, copyMaxC);
            return result;
        }

        public static T[] GetAnonymousTypeArray<T>(this T element, params T[] followers)
        {
            if (followers == null)
                throw new ArgumentNullException("followers");
            T[] result = new T[followers.Length + 1];
            result[0] = element;
            followers.CopyTo(result, 1);
            return result;
        }

        public static T[] GetAnonymousTypeArray<T>(this T anonymousTypeSeed, int length)
        {
            return new T[length];
        }

        public static T[][] Chunk<T>(this T[] series, int chunkSize)
        {
            int chunkCount = (int)Math.Ceiling(((double)series.Length) / chunkSize);
            T[][] result = new T[chunkCount][];
            for (int i = 0, c = series.Length; i < chunkCount; i++)
            {
                int min = i * chunkSize;
                int max = Math.Min((i + 1) * chunkSize, c);
                int size = max - min;
                T[] current = result[i] = new T[size];
                for (int j = 0; j < size; j++)
                    current[j] = series[min + j];
            }
            return result;
        }
    }
}