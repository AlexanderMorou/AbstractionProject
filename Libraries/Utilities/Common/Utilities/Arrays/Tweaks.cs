using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public delegate void OnBoundsIncrement(int rank, bool isIterationComplete);
    /// <summary>
    /// Provides methods that give small case changes to arrays.
    /// </summary>
    public static partial class ArrayExtensions
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
                            return (TDesiredType) (object) item;
                        else
                            throw new InvalidCastException("A member of the source array cannot be cast to the TDesiredType");
                    }
                );
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
                k => (TDesiredType) (object) k);
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

        /// <summary>
        /// Merges a <paramref name="series"/> of arrays into a single, large, array.
        /// </summary>
        /// <typeparam name="T">The type of element contained within each array.</typeparam>
        /// <param name="series">The array of arrays.</param>
        /// <returns>A new <typeparamref name="T"/> array containing all of the
        /// elements from <paramref name="series"/>.</returns>
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
            for (int i = 0; i < series.Length; i++)
                if (series[i] == null)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.series, ExceptionMessageId.MemberOfSeriesNull, ThrowHelper.GetArgumentName(ArgumentWithException.series));
                else
                    fullLength += series[i].Length;
            T[] result = new T[fullLength];
            for (int i = 0, offset = 0; i < series.Length && offset < fullLength; offset += series[i++].Length)
                series[i].CopyTo(result, offset);
            return result;
        }

        public static T[] MergeSeries<T>(this IEnumerable<T[]> series)
        {
            return MergeArrays(series.ToArray());
        }

        public static T[] Interleave<T>(T[] itemsA, T[] itemsB)
        {
            T[] result = new T[itemsA.Length * 2];
            for (int i = 0, k = 0; i < itemsA.Length; i++, k++)
            {
                result[k] = itemsA[i];
                result[++k] = itemsB[i];
            }
            return result;
        }

        public static Tuple<T[], T[]> SeparateInterleave<T>(T[] source)
        {
            T[] resultA = new T[source.Length / 2];
            T[] resultB = new T[resultA.Length];
            for (int i = 0, k = 0; i < resultA.Length; i++, k++)
            {
                resultA[i] = source[k];
                resultB[i] = source[++k];
            }
            return new Tuple<T[], T[]>(resultA, resultB);
        }

        /// <summary>
        /// Obtains an enumerable object which can iterate through all of
        /// the indices of an <see cref="Array"/> regardless of its 
        /// dimensional complexity.
        /// </summary>
        /// <param name="array">The <see cref="Array"/> to perform
        /// iteration on.</param>
        /// <param name="useSingletonResult"></param>
        /// <param name="onBoundsIncrement">A <see cref="OnBoundsIncrement"/> delegate which denotes
        /// the rank of the dimension being incremented, and whether all passes have finished to indicate
        /// no further action will be taken.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> object that
        /// yields a single-dimensional array per iteration relative
        /// to the <paramref name="array"/> provided.</returns>
        /// <remarks><para>The number of values in the resultant array,
        /// per iteration, is equivalent to the
        /// <see cref="Array.Rank"/> of the 
        /// <paramref name="array"/> provided.</para>
        /// <para>Due to the nature this method was intended to be used,
        /// the array retrieved per iteration is the same instance, when
        /// <paramref name="useSingletonResult"/> is true, so its values are
        /// guaranteed to <b>not</b> be the same, on a much later access of the
        /// iteration, should its reference be stored.</para></remarks>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="array"/> is null.</exception>
        public static IEnumerable<int[]> Iterate(this Array array, bool useSingletonResult = true, OnBoundsIncrement onBoundsIncrement = null)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            int[] indices;
            int rank = array.Rank;
            if (rank == 1)
            {
                /* *
                 * Simple answer for one dimension
                 * */
                indices = new int[] { array.GetLowerBound(0) };
                if (useSingletonResult)
                    for (int upperBound = array.GetUpperBound(0); indices[0] <= upperBound; indices[0]++)
                        yield return indices;
                else
                    for (int upperBound = array.GetUpperBound(0); indices[0] <= upperBound; indices[0]++)
                        yield return (int[]) indices.Clone();
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

                int[] lowerBounds = (int[]) indices.Clone();
                bool finished = false;
                int topRank = rank - 1;
            Repeater:
                //while (true)
                {
                    if (useSingletonResult)
                        /* *
                         * Nifty thing is... it's always the same array,
                         * which means there's no performance hit for
                         * creating and returning new arrays, because we don't.
                         * */
                        yield return indices;
                    else
                        /* *
                         * If the user needs to cache the indices of every element
                         * of the array, this would be necessary.
                         * *
                         * Notable use case is parallel processing/initialization
                         * of an array.
                         * */
                        yield return (int[]) indices.Clone();
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
                         * and exceeded the high point of the dimension,
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

        public static ArrayBounds GetBounds(this Array target)
        {
            return new ArrayBounds(target);
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
        /// <remarks>Places a lock on the <paramref name="source"/> array.</remarks>
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
                     * dimension is best served to be given a normal
                     * loop as the tasking of the parallel elements 
                     * takes a greater overhead than the loop itself 
                     * does.
                     * */
                    for (int innerIndex = 0; innerIndex < lengthB; innerIndex++)
                        destination[lowerDA + outterIndex, lowerDB + innerIndex] = source[lowerSA + outterIndex, lowerSB + innerIndex];
                });
            }
        }

        /// <summary>
        /// Resizes the <paramref name="source"/> array provided
        /// with the <paramref name="newLengthA"/> and 
        /// <paramref name="newLengthB"/>,
        /// while preserving the original data.
        /// </summary>
        /// <typeparam name="T">The kind of element used within the array
        /// to resize.</typeparam>
        /// <param name="source">The two-dimensional array to resize.</param>
        /// <param name="newLengthA">The <see cref="Int32"/> value denoting
        /// the new number of elements in the first dimension of the array.</param>
        /// <param name="newLengthB">The <see cref="Int32"/> value denoting
        /// the new number of elements in the second dimension of the array.</param>
        /// <returns>A new two-dimensional array of <typeparamref name="T"/>
        /// with the original data from the <paramref name="source"/>.</returns>
        /// <remarks>If <paramref name="newLengthA"/> and/or <paramref name="newLengthB"/> are/is
        /// less than the number of elements, within their respective dimensions,
        /// the data will be truncated accordingly.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="source"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when <paramref name="newLengthA"/> or 
        /// <paramref name="newLengthB"/> is less than zero.</exception>
        public static T[,] Resize<T>(this T[,] source, int newLengthA, int newLengthB)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            int upperSA = source.GetUpperBound(0),
                upperSB = source.GetUpperBound(1);
            int lowerSA = source.GetLowerBound(0),
                lowerSB = source.GetLowerBound(1);

            T[,] result = (T[,]) (Array.CreateInstance(typeof(T), new int[] { newLengthA, newLengthB }, new int[] { lowerSA, lowerSB }));
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

        /// <summary>
        /// Copies data from the <paramref name="source"/> four-dimensional array
        /// to the <paramref name="destination"/> four-dimensional array.
        /// </summary>
        /// <typeparam name="T">The kind of element used within the arrays
        /// copied.</typeparam>
        /// <param name="source">The four-dimensional array from which copying occurs.</param>
        /// <param name="destination">The four-dimensional array of <typeparamref name="T"/>
        /// elements in which the elements are received.</param>
        /// <param name="lengthA">The <see cref="Int32"/>
        /// value representing the size of the lowest order dimension
        /// of the copy operation.</param>
        /// <param name="lengthB">The <see cref="Int32"/>
        /// value representing the size of the middle lowest order dimension
        /// of the copy operation.</param>
        /// <param name="lengthC">The <see cref="Int32"/>
        /// value representing the size of the middle highest order dimension
        /// of the copy operation.</param>
        /// <param name="lengthD">The <see cref="Int32"/>
        /// value representing the size of the highest order dimension
        /// of the copy operation.</param>
        public static void TesseracticCopy<T>(this T[, , ,] source, T[, , ,] destination, int lengthA, int lengthB, int lengthC, int lengthD)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            int upperSA = source.GetUpperBound(0),
                upperSB = source.GetUpperBound(1),
                upperSC = source.GetUpperBound(2),
                upperSD = source.GetUpperBound(3);
            int upperDA = destination.GetUpperBound(0),
                upperDB = destination.GetUpperBound(1),
                upperDC = destination.GetUpperBound(2),
                upperDD = destination.GetUpperBound(3);

            int lowerSA = source.GetLowerBound(0),
                lowerSB = source.GetLowerBound(1),
                lowerSC = source.GetLowerBound(2),
                lowerSD = source.GetLowerBound(3);
            int lowerDA = destination.GetLowerBound(0),
                lowerDB = destination.GetLowerBound(1),
                lowerDC = destination.GetLowerBound(2),
                lowerDD = destination.GetLowerBound(3);

            int lengthSA = upperSA + 1 - lowerSA,
                lengthSB = upperSB + 1 - lowerSB,
                lengthSC = upperSC + 1 - lowerSC,
                lengthSD = upperSD + 1 - lowerSD;
            int lengthDA = upperDA + 1 - lowerDA,
                lengthDB = upperDB + 1 - lowerDB,
                lengthDC = upperDC + 1 - lowerDC,
                lengthDD = upperDD + 1 - lowerDD;

            if (lengthA > lengthSA || lengthA > lengthDA)
                throw new ArgumentOutOfRangeException("lengthA");
            if (lengthB > lengthSB || lengthB > lengthDB)
                throw new ArgumentOutOfRangeException("lengthB");
            if (lengthC > lengthSC || lengthC > lengthDC)
                throw new ArgumentOutOfRangeException("lengthC");
            if (lengthD > lengthSD || lengthD > lengthDD)
                throw new ArgumentOutOfRangeException("lengthD");

            lock (source)
                lock (destination)
                    Parallel.For(0, lengthA, outterIndex =>
                    {
                        for (int outterMiddleIndex = 0; outterMiddleIndex < lengthB; outterMiddleIndex++)
                            Parallel.For(0, lengthC, innerMiddleIndex =>
                            {
                                for (int innerIndex = 0; innerIndex < lengthD; innerIndex++)
                                    destination[lowerDA + outterIndex, lowerDB + outterMiddleIndex, lowerDC + innerMiddleIndex, lowerDD + innerIndex] = source[lowerSA + outterIndex, lowerSB + outterMiddleIndex, lowerSC + innerMiddleIndex, lowerSD + innerIndex];
                            });
                    });
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

            T[, ,] result = (T[, ,]) (Array.CreateInstance(typeof(T), new int[] { newLengthA, newLengthB, newLengthC }, new int[] { lowerSA, lowerSB, lowerSC }));
            int lengthSA = upperSA + 1 - lowerSA,
                lengthSB = upperSB + 1 - lowerSB,
                lengthSC = upperSC + 1 - lowerSC;
            int copyMaxA = Math.Min(newLengthA, lengthSA),
                copyMaxB = Math.Min(newLengthB, lengthSB),
                copyMaxC = Math.Min(newLengthC, lengthSC);
            source.CubicCopy(result, copyMaxA, copyMaxB, copyMaxC);
            return result;
        }

        public static T[][] Chunk<T>(this T[] series, int chunkSize)
        {
            int chunkCount = (series.Length + chunkSize - 1) / chunkSize;
            T[][] result = new T[chunkCount][];
            for (int i = 0; i < chunkCount; i++)
            {
                int min = i * chunkSize;
                int max = Math.Min((i + 1) * chunkSize, series.Length);
                int size = max - min;
                T[] current = result[i] = new T[size];
                for (int j = 0; j < size; j++)
                    current[j] = series[min + j];
            }
            return result;
        }

        /// <summary>
        /// Determines whether the <paramref name="series"/> contains
        /// enough space to hold the <paramref name="numberOfNew"/> elements 
        /// relative to the actual <paramref name="itemCount"/> and the
        /// array <see cref="Array.Length"/>.
        /// </summary>
        /// <typeparam name="T">The type of element
        /// within the series.</typeparam>
        /// <param name="series">The series of <typeparamref name="T"/>
        /// elements to work with.</param>
        /// <param name="itemCount">The number of items actually contained within
        /// the <paramref name="series"/>.</param>
        /// <param name="numberOfNew">The <see cref="Int32"/> value representing
        /// the number of elements to introduce into the <paramref name="series"/>.</param>
        /// <returns>An array of <typeparamref name="T"/> elements representing
        /// the new buffer.</returns>
        internal static T[] EnsureSpaceExists<T>(this T[] series, int itemCount, int numberOfNew)
        {
            if (series == null)
            {
                var result = new T[(itemCount + numberOfNew) * 2];
                return result;
            }
            else if (series.Length < itemCount + numberOfNew)
            {
                int newLength = series.Length * 2;
                if (newLength < itemCount + numberOfNew)
                    /* *
                     * Ensures the next growth isn't the next item entered.
                     * */
                    newLength = (itemCount + numberOfNew) * 2;
                var copy = new T[newLength];
                Array.ConstrainedCopy(series, 0, copy, 0, itemCount);
                //for (int i = 0; i < series.Length; i++)
                //    copy[i] = series[i];
                return copy;
            }
            return series;
        }

        internal static T[] EnsureMinimalSpaceExists<T>(this T[] series, uint itemCount, uint numberOfNew, uint maxTotal)
        {
            if (series == null)
            {
                var result = new T[(itemCount + numberOfNew) * 2];
                return result;
            }
            else if (series.Length < itemCount + numberOfNew)
            {
                uint newLength = ((uint) series.Length) * 2;
                if (newLength < itemCount + numberOfNew)
                    /* *
                     * Ensures the next growth isn't the next item entered.
                     * */
                    newLength = (itemCount + numberOfNew) * 2;
                newLength = (uint) Math.Min(maxTotal, newLength);
                var copy = new T[newLength];
                Array.ConstrainedCopy(series, 0, copy, 0, (int) itemCount);
                //for (int i = 0; i < series.Length; i++)
                //    copy[i] = series[i];
                return copy;
            }
            return series;
        }

        public static Tuple<T1[], T2[]> SplitSet<T1, T2>(this IEnumerable<Tuple<T1, T2>> set)
        {
            if (set == null)
                throw new ArgumentNullException("set");
            return SplitSet<T1, T2>(set.ToArray());
        }

        public static Tuple<T1[], T2[]> SplitSet<T1, T2>(this Tuple<T1, T2>[] set)
        {
            if (set == null)
                throw new ArgumentNullException("set");
            T1[] first = new T1[set.Length];
            T2[] second = new T2[set.Length];
            for (int pairIndex = 0; pairIndex < set.Length; pairIndex++)
            {
                var currentPair = set[pairIndex];
                first[pairIndex] = currentPair.Item1;
                second[pairIndex] = currentPair.Item2;
            }
            return new Tuple<T1[], T2[]>(first, second);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> for the <paramref name="array"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="array"/> elements.</typeparam>
        /// <param name="array">The series of <typeparamref name="T"/> values which needs an enumerator.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> for the <paramref name="array"/> provided.</returns>
        /// <remarks>The <see cref="IEnumerable{T}"/> yielded obscures the original
        /// <paramref name="array"/> by providing a new enumerable source.</remarks>
        public static IEnumerable<T> GetEnumerable<T>(this T[] array)
        {
            if (array == null ||
                array.Length == 0)
                yield break;
            foreach (var t in array)
                yield return t;
        }

        public static T[] Flatten<T>(this IEnumerable<IEnumerable<T>> series)
        {
            return (from set in series.AsParallel()
                    select set.ToArray()).ToArray().ConcatinateSeries();
        }

        public static IEnumerable<IEnumerable<T>> Splay<T>(this IEnumerable<T> series)
        {
            /* When you want to yield an enumerable over each element in the series. */
            foreach (var element in series)
                yield return new T[1] { element };
        }

        public static IEnumerable<IEnumerable<T>> GetAllPermutations<T>(this T[][] series, int minSetLength)
        {
            return ((IEnumerable<IEnumerable<T>>)series).GetAllPermutations(minSetLength);
        }

        public static IEnumerable<T> AsEnumerable<T>(this T element) { return new T[1] { element }; }

        public static IEnumerable<IEnumerable<T>> GetAllPermutations<T>(this IEnumerable<IEnumerable<T>> series, int minSetLength)
        {
            var jaggedVariation =
              series.Select(set => set.ToArray()).ToArray();
            for (int minDepth = minSetLength; minDepth <= jaggedVariation.Length; minDepth++)
                foreach (var set in GetPermutationsOfLength<T>(minDepth, jaggedVariation))
                    yield return set;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutationsOfLength<T>(int elementsPerSet, T[][] series)
        {
            for (int subsetIndex = 0; subsetIndex < series.Length - (elementsPerSet - 1); subsetIndex++)
                foreach (var subset in GetPermutationsOfLength<T>(elementsPerSet, subsetIndex, 0, series).Select(k => k.ToArray()))
                    if (subset.Length == elementsPerSet) /* Keeps the logic below very simple. */
                        yield return subset;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutationsOfLength<T>(int elementsPerSet, int startingAt, int currentLength, T[][] series)
        {
            if (startingAt >= series.Length || currentLength >= elementsPerSet)
                yield break;

            var currentFrontSet = series[startingAt];
            foreach (var forefront in currentFrontSet)
            {
                var forefrontSet = new T[1] { forefront };
                /* Continue expanding recursively until the above constraints cause it to short circuit. */
                for (int i = startingAt + 1; i < series.Length; i++)
                {
                    var subsets = GetPermutationsOfLength<T>(elementsPerSet, i, currentLength + 1, series);
                    foreach (var subset in subsets)
                        yield return forefrontSet.Concat(subset);
                }
                yield return forefrontSet;
            }
        }

    }
}