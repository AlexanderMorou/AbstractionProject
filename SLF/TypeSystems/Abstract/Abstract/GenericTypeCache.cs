using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Threading.Tasks;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
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
        /// contains a generic instance for the <see cref="typeParameters"/>
        /// provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ITypeCollectionBase"/>
        /// which represents the generic parameter replacements to search for.</param>
        /// <param name="r">The output <typeparamref name="IGenericType"/>
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



        public void BeginExodus()
        {
            if (this.exodusCache != null)
                return;
            this.exodusCache = new HashSet<LockedTypeCollection>();
        }
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool dispose)
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
                if (dispose)
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
            }
            finally
            {
                lock (this.syncObject)
                    this.disposing = false;
            }
        }

        #endregion

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
            //for (int exodusSetIndex = 0; exodusSetIndex < exodusCopy.Length; exodusSetIndex++)
            {
                var currentSet = exodusCopy[exodusSetIndex].copy;
                //Parallel.For(0, keyCopy.Length, (keySetIndex, keySetLoopState) =>
                for (int keySetIndex = 0; keySetIndex < keyCopy.Length; keySetIndex++)
                {
                    var currentKeySet = keyCopy[keySetIndex].copy;
                    if (currentKeySet.Count != currentSet.Count)
                        return;
                    bool allFound = true;
                    //Parallel.For(0, currentSet.Count, (currentElementIndex, currentSetLoopState) =>
                    for (int currentElementIndex = 0; currentElementIndex < currentSet.Count; currentElementIndex++)
                    {
                        IType currentElement = currentSet[currentElementIndex];
                        IType currentAlternate;
                        lock (currentKeySet)
                            currentAlternate = currentKeySet[currentElementIndex];
                        if (!currentAlternate.Equals(currentElement))
                        {
                            allFound = false;
                            //currentSetLoopState.Stop(); return;
                            break;
                        }
                    }//);
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
                        //keySetLoopState.Stop(); return;
                        break;
                    }
                }//);
            });
            //Console.WriteLine("{0} were expunged.", exodusElements);
            exodusCache = null;
        }


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
