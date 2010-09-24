using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
        private Dictionary<int[], IArrayType> nonstandardArrayCache;
        /* *
         * The element type that all the array types share.
         * */
        private IType source;
        /* *
         * Delegates which hold the creator methods, more reliable than 
         * the eventing alternative.
         * */
        private Func<int, IArrayType> creatorVector;
        private Func<int[], IArrayType> creatorMulti;
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
        /// <param name="creatorMulti">The <see cref="Func{T, TResult}"/> which creates
        /// the multi-dimensional instances relative to the <paramref name="source"/> type.
        /// </param>
        internal TypeArrayCache(IType source, Func<int, IArrayType> creatorVector, Func<int[], IArrayType> creatorMulti)
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
            if (this.nonstandardArrayCache != null)
            {
                var elements = this.nonstandardArrayCache.Keys.ToArray();
                for (int i = 0; i < elements.Length; i++)
                    this.nonstandardArrayCache[elements[i]].Dispose();
                this.nonstandardArrayCache.Clear();
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
            if (!this.normalArrayCache.ContainsKey(rank))
                this.normalArrayCache.Add(rank, creator(rank));
            return this.normalArrayCache[rank];
        }

        public IArrayType CreateArray()
        {
            return this.CreateArray(1);
        }

        public IArrayType CreateArray(params int[] lowerBounds)
        {
            var result = CreateArrayInternal(lowerBounds, creatorMulti);
            return result;
        }

        internal IArrayType CreateArrayInternal(int[] lowerBounds, Func<int[], IArrayType> creator)
        {
            /* *
             * Can't define a non-zero vector array.  The CLI doesn't provide innate support
             * for their declaration, but you can create them, just not pass them to anything.
             * */
            bool found = false;
            IArrayType result = null;
            if (this.nonstandardArrayCache == null)
                this.nonstandardArrayCache = new Dictionary<int[], IArrayType>();
            foreach (var cacheElement in this.nonstandardArrayCache)
            {
                var array = cacheElement.Key;
                /* *
                 * Search for a series of lower-bounds that is the same.
                 * */
                if (array.Length != lowerBounds.Length)
                    continue;
                else
                {
                    /* *
                     * Only where all the lower bounds are equal...
                     * */
                    for (int i = 0; i < array.Length; i++)
                        if (!(found = (lowerBounds[i] == array[i])))
                            break;
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
                this.nonstandardArrayCache.Add(lowerBounds, result = creator(lowerBounds));
            return result;
        }
    }
}
