using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Common;
using System.Diagnostics.CodeAnalysis;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        /// Alters the contents of the array and ignores others based upon the two 
        /// <see cref="Common.TranslateArgument{TResult, TArgument}"/> delegates.
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
            //Setup space.
            List<TDestinationItem> resultItems = new List<TDestinationItem>(items.Length);
            //Iterate, filter, and translate.
            foreach (TSourceItem item in items)
                if (filter(item))
                    resultItems.Add(translator(item));
            //Remove blanks.
            resultItems.TrimExcess();

            return resultItems.ToArray();
        }
        public static T[] ProcessArray<T>(T[] array, Func<T, T> processor)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            T[] result = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = processor(array[i]);
            return result;
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
            for (int i = 0; i < series.Length; i++)
                if (series[i] == null)
                    throw new ArgumentException("A member of series was null", "series");
                else
                    fullLength += series[i].Length;
            T[] result = new T[fullLength];
            for (int i = 0, offset = 0; i < series.Length && offset < fullLength; offset += series[i++].Length)
                series[i].CopyTo(result, offset);
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
        /// <see cref="Array.ArrayRank"/> of the 
        /// <paramref name="array"/> provided.</para>
        /// <para>Due to the nature this method was intended to be used,
        /// the array retrieved per iteration is the same so it is not
        /// guaranteed to be the same on a much later acces
        /// should its reference be stored, and the iteration
        /// continued.</para></remarks>
        public static IEnumerable<int[]> IterateArray(this Array array)
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
                 * Multi-dimensional arrays are a bit different.
                 * */
                var rankRange = rank.Range().ToArray();
                indices = rankRange.OnAll(p => array.GetLowerBound(p)).ToArray();
                /* *
                 * Obtain the upper/lower bounds..
                 * */
                int[] lowerBounds = (int[])indices.Clone();
                int[] upperBounds = rankRange.OnAll(p => array.GetUpperBound(p)).ToArray();
            Repeater:
                {
                    /* *
                     * Nifty thing is... it's always the same array,
                     * which means there's no performance hit for
                     * creating and returning new arrays.
                     * */
                    yield return indices;
                    /* *
                     * Move through the dimensions, starting 
                     * with the highest-order.
                     * */
                    for (int i = rank - 1; i >= 0; i--)
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
                        /* *
                         * Reset the current index, the loop
                         * will continue until all 'overflows' 
                         * on the array are hit and reset 
                         * accordingly.
                         * */
                        indices[i] = lowerBounds[i];
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
        public static T[] GetArray<T>(this T element, params T[] followers)
        {
            if (followers == null)
                throw new ArgumentNullException("followers");
            T[] result = new T[followers.Length + 1];
            result[0] = element;
            followers.CopyTo(result, 1);
            return result;
        }

    }
}