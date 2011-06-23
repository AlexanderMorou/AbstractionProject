using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a general case generic type cache for
    /// generic implementations.
    /// </summary>
    public class GenericTypeCache :
        _IGenericTypeRegistrar,
        IDisposable,
        IEnumerable<IGenericType>,
        IMassTargetHandler
    {
        private Dictionary<LockedTypeCollection, IGenericType> genericCache = null;
        private HashSet<LockedTypeCollection> exodusCache;
        private object syncObject = new object();
        private bool disposing;
        #region _IGenericTypeRegistrar Members
        /// <summary>
        /// Registers a generic type with the <paramref name="targetType"/> and 
        /// <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="targetType">The <see cref="IGenericType"/>
        /// which represents the specific generic instance of the user of the 
        /// <see cref="GenericTypeCache"/>.</param>
        /// <param name="typeParameters">The series of <see cref="IType"/>
        /// instances which replace the type-parameters of the type the 
        /// <see cref="GenericTypeCache"/> is used by.</param>
        internal void RegisterGenericType(IGenericType targetType, LockedTypeCollection typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new Dictionary<LockedTypeCollection, IGenericType>();
            IGenericType required;
            if (this.ContainsGenericType(typeParameters, out required))
                return;
            lock (syncObject)
                genericCache.Add(new LockedTypeCollection(typeParameters), (IGenericType)targetType);
        }
        
        /// <summary>
        /// Returns whether the current <see cref="GenericTypeCache"/>
        /// contains a generic instance for the <paramref name="typeParameters"/>
        /// provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ITypeCollectionBase"/>
        /// which represents the generic parameter replacements to search for.</param>
        /// <param name="r">The output <see cref="IGenericType"/>
        /// instance represented by the <paramref name="typeParameters"/> provided.</param>
        /// <returns>true if the <see cref="GenericTypeCache"/> contains
        /// a generic instance with the <paramref name="typeParameters"/>
        /// provided; false, otherwise.</returns>
        public bool ContainsGenericType(ITypeCollectionBase typeParameters, out IGenericType r)
        {
            if (this.genericCache == null || typeParameters == null)
            {
                r = default(IGenericType);
                return false;
            }
            LockedTypeCollection familliarSeries = ObtainGenericFamilliar(typeParameters);
            if (familliarSeries == null)
            {
                r = default(IGenericType);
                return false;
            }
            r = this.genericCache[familliarSeries];
            return true;
        }

        internal void UnregisterGenericType(LockedTypeCollection typeParameters)
        {
            if (this.syncObject == null || typeParameters == null)
                return;
            if (this.exodusCache != null)
            {
                lock (this.exodusCache)
                    this.exodusCache.Add(typeParameters);
                return;
            }
            if (this.genericCache == null || this.disposing)
                return;
            var familliarSeries = ObtainGenericFamilliar(typeParameters);
            if (familliarSeries == null)
                return;
            /* *
             * Multi-threading requirement, if the generic type which has been
             * disposed is the result of this disposing, this very well could
             * occur.
             * */

            lock (this.syncObject)
                if (this.genericCache != null)
                    genericCache.Remove(familliarSeries);
        }

        #endregion

        /// <summary>
        /// Begins an exodus upon the <see cref="GenericTypeCache"/>.
        /// </summary>
        public void BeginExodus()
        {
            if (this.exodusCache != null)
                return;
            this.exodusCache = new HashSet<LockedTypeCollection>();
        }

        /// <summary>
        /// Ends an exodus upon the <see cref="GenericTypeCache"/>.
        /// </summary>
        public void EndExodus()
        {
            if (this.exodusCache == null)
                return;
            int exodusElements = 0;
            LockedTypeCollection[] exodusCopy;
            lock (exodusCache)
            {
                exodusCopy = exodusCache.ToArray();
                exodusCache.Clear();
                if (exodusCopy.Length == 0)
                    return;
            }
            LockedTypeCollection[] keyCopy;
            lock (this.syncObject)
                keyCopy = this.genericCache.Keys.ToArray();

            Parallel.For(0, exodusCopy.Length, exodusSetIndex =>
            {
                var currentSet = exodusCopy[exodusSetIndex].copy;
                for (int keySetIndex = 0; keySetIndex < keyCopy.Length; keySetIndex++)
                {
                    var currentKeySet = keyCopy[keySetIndex].copy;
                    if (currentKeySet.Count != currentSet.Count)
                        return;
                    bool allFound = true;
                    for (int currentElementIndex = 0; currentElementIndex < currentSet.Count; currentElementIndex++)
                    {
                        IType currentElement = currentSet[currentElementIndex];
                        IType currentAlternate;
                        lock (currentKeySet)
                            currentAlternate = currentKeySet[currentElementIndex];
                        if (!currentAlternate.Equals(currentElement))
                        {
                            allFound = false;
                            break;
                        }
                    }
                    if (allFound)
                    {
                        var currentLocked = keyCopy[keySetIndex];
                        if (currentLocked != null && currentLocked.IsDisposed)
                            continue;

                        lock (syncObject)
                        {
                            genericCache.Remove(currentLocked);
                            exodusElements++;
                        }
                        break;
                    }
                }
            });
            exodusCache = null;
        }

        #region IDisposable Members

        /// <summary>
        /// Disposes the <see cref="GenericTypeCache"/>.
        /// </summary>
        public void Dispose()
        {
            if (this.syncObject == null)
                return;
            lock (syncObject)
            {
                if (this.disposing)
                    return;
                this.disposing = true;
            }
            try
            {
                IGenericType[] genericCacheCopy;
                lock (this.syncObject)
                    genericCacheCopy = this.genericCache.Values.ToArray();
                Parallel.ForEach(genericCacheCopy, genericEntity =>
                    genericEntity.Dispose());
                lock (this.syncObject)
                {
                    this.genericCache.Clear();
                    this.genericCache = null;
                }
            }
            finally
            {
                lock (this.syncObject)
                    this.disposing = false;
            }
        }

        #endregion

        private LockedTypeCollection ObtainGenericFamilliar(ITypeCollectionBase typeParameters)
        {
            LockedTypeCollection familliar = null;
            LockedTypeCollection[] keyCopy;
            IType[] typeParametersCopy;
            /* *
             * Obtain a copy of the object references.
             * */
            lock (this.syncObject)
                keyCopy = this.genericCache.Keys.ToArray();
            lock (typeParameters)
                typeParametersCopy = typeParameters.ToArray();

            /* *
             * iterate through the references and check them 
             * one by one.
             * */
            Parallel.For(0, keyCopy.Length, (i, parallelLoopState) =>
            {
                var currentSet = keyCopy[i].copy;
                if (currentSet.Count != typeParametersCopy.Length)
                    return;
                bool allFound = true;
                for (int j = 0; j < typeParametersCopy.Length; j++)
                {
                    var currentElement = typeParametersCopy[j];
                    IType currentAlternate;
                    lock (currentSet)
                        currentAlternate = currentSet[j];
                    if (!currentAlternate.Equals(currentElement))
                    {
                        allFound = false;
                        break;
                    }
                }
                if (allFound)
                {
                    var currentSetL = keyCopy[i];
                    if (currentSetL.IsDisposed)
                        return;
                    familliar = currentSetL;
                    parallelLoopState.Stop();
                }
            });
            return familliar;
        }


        #region IEnumerable<IType> Members

        /// <summary>
        /// Obtains an <see cref="IEnumerable{T}"/> instance
        /// which iterates the elements of the <see cref="GenericTypeCache"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> instance.</returns>
        public IEnumerator<IGenericType> GetEnumerator()
        {
            foreach (var element in this.genericCache.Values)
                yield return element;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region _IGenericTypeRegistrar Members

        void _IGenericTypeRegistrar.RegisterGenericType(IGenericType targetType, LockedTypeCollection typeParameters)
        {
            this.RegisterGenericType(targetType, typeParameters);
        }

        void _IGenericTypeRegistrar.UnregisterGenericType(LockedTypeCollection typeParameters)
        {
            this.UnregisterGenericType(typeParameters);
        }

        #endregion
    }
}
