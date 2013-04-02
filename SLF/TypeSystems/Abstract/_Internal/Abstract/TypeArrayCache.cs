using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    /* *
     * Used to cache the single/multi-dimensional array types associated to a given
     * IType.
     * *
     * Doesn't automatically provide a creator for array types because they are 
     * implemented in the compiled model, due to target differences the implemented interfaces
     * and other aspects can change.
     * */
    internal class TypeArrayCache :
        IDisposable
    {
        /* *
         * Instance cache dictionaries, to prevent multiple instances of 
         * the same type.
         * */
        private IDictionary<int, IArrayType> normalArrayCache;
        private MultikeyedDictionary<int[], uint[], IArrayType> lengthsAndLowersCache;
        /* *
         * The element type that all the array types share.
         * */
        private IType source;
        /* *
         * Delegates which hold the creator methods, more reliable than 
         * the eventing alternative.
         * */
        private Func<int, IArrayType> creatorVector;
        private Func<int[], uint[], IArrayType> creatorMulti;
        /// <summary>
        /// Creates a new <see cref="TypeArrayCache"/> with the
        /// <paramref name="source"/>, <paramref name="creatorVector"/> and
        /// <paramref name="creatorMulti"/>
        /// </summary>
        /// <param name="source">The <see cref="IType"/> is represented by the 
        /// array sets.</param>
        /// <param name="creatorVector">The <see cref="Func{T, TResult}"/> which creates
        /// the vector array instances relative to the <paramref name="source"/> type.
        /// </param>
        /// <param name="creatorMulti">The <see cref="Func{T1, T2, TResult}"/> which creates
        /// the multi-dimensional instances relative to the <paramref name="source"/> type.
        /// </param>
        internal TypeArrayCache(IType source, Func<int, IArrayType> creatorVector, Func<int[], uint[], IArrayType> creatorMulti)
        {
            this.source = source;
            this.creatorVector = creatorVector;
            this.creatorMulti = creatorMulti;
        }

        #region IDisposable Members

        public void Dispose()
        {
            //Dispose the elements of the normal array cache.
            if (this.normalArrayCache != null)
            {
                var elements = this.normalArrayCache.Keys.ToArray();
                for (int i = 0; i < this.normalArrayCache.Count; i++)
                    this.normalArrayCache[elements[i]].Dispose();
                this.normalArrayCache.Clear();
            }
            //Dispose the elements of the non-standard array cache.
            if (this.lengthsAndLowersCache != null)
            {
                var elements = this.lengthsAndLowersCache.ToArray();
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i].Value.Dispose();
                    this.lengthsAndLowersCache.Remove(elements[i].Keys.Key1, elements[i].Keys.Key2);
                }
            }
            //Release delegates.
            creatorVector = null;
            creatorMulti = null;
        }

        #endregion

        public IArrayType CreateArray(int rank)
        {
            return CreateArray(rank, creatorVector);
        }

        public IArrayType CreateArray(int rank, Func<int, IArrayType> creator)
        {
            //Cache dictionary check.
            if (this.normalArrayCache == null)
                this.normalArrayCache = new Dictionary<int, IArrayType>();
            //Cache element check.
            IArrayType result;
            if (!this.normalArrayCache.TryGetValue(rank, out result))
                this.normalArrayCache.Add(rank, result = creator(rank));
            return result;
        }

        public IArrayType CreateArray()
        {
            return this.CreateArray(1);
        }

        public IArrayType CreateArray(int[] lowerBounds, uint[] lengths = null)
        {
            if (lowerBounds == null && lengths == null)
                throw new ArgumentNullException("lowerBounds and lengths");
            if (lowerBounds.Length == 1 && lowerBounds[0] == 0 && (lengths == null || lengths.Length == 1 && lengths[0] == 0))
                /* *
                 * Creating a zero-length type is pointless.
                 * */
                return this.CreateArray();
            var result = CreateArrayInternal(lowerBounds, lengths, creatorMulti);
            return result;
        }

        internal IArrayType CreateArrayInternal(int[] lowerBounds, uint[] lengths, Func<int[], uint[], IArrayType> creator)
        {
            /* *
             * Can't define a non-zero vector array.  The CLI doesn't provide innate support
             * for their declaration, but you can create them, just not pass them to anything.
             * */
            bool found = false;
            IArrayType result = null;
            if (lengths == null && lowerBounds == null)
                throw new ArgumentNullException("lowerBounds");
            if (this.lengthsAndLowersCache == null)
                this.lengthsAndLowersCache = new MultikeyedDictionary<int[], uint[], IArrayType>();

            if (lengths == null)
                lengths = new uint[0];
            else if (lowerBounds == null)
                lowerBounds = new int[0];
            foreach (var cacheElement in this.lengthsAndLowersCache)
            {
                var currentLowerBounds = cacheElement.Keys.Key1;
                var currentLengths = cacheElement.Keys.Key2;
                /* *
                 * Search for a series of lower-bounds that is the same.
                 * */
                if (currentLowerBounds.Length != lowerBounds.Length ||
                    currentLengths.Length != lengths.Length)
                    continue;
                else
                {
                    /* *
                     * Only where all the lower bounds are equal...
                     * */
                    found = true;
                    for (int i = 0; i < currentLowerBounds.Length; i++)
                        if (lowerBounds[i] != currentLowerBounds[i])
                        {
                            found = false;
                            break;
                        }
                    for (int i = 0; i < currentLengths.Length; i++)
                        if (lengths[i] != currentLengths[i])
                        {
                            found = false;
                            break;
                        }
                    if (found)
                    {
                        result = cacheElement.Value;
                        break;
                    }
                }
            }
            /* *
             * Create the element using the multi-dimensional delegate
             * provided at initialization if the member does not exist.
             * */
            if (!found)
                this.lengthsAndLowersCache.Add(lowerBounds, lengths, result = creator(lowerBounds, lengths));
                //this.noLengthsCache.Add(lowerBounds, result = creator(lowerBounds));
            return result;
        }
    }
}
