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
    public class GenericTypeCache<TType> :
        _IGenericTypeRegistrar,
        IDisposable,
        IEnumerable<TType>,
        IMassTargetHandler
        where TType :
            IGenericType<TType>
    {
        private ControlledStateDictionary<ITypeCollectionBase, TType> genericCache = null;
        private HashSet<ITypeCollectionBase> exodusCache;
        private object syncObject = new object();
        private bool disposing;
        #region _IGenericTypeRegistrar Members

        public void RegisterGenericType(IGenericType targetType, ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new ControlledStateDictionary<ITypeCollectionBase, TType>();
            TType required;
            if (this.ContainsGenericType(typeParameters, out required))
                return;
            lock (genericCache)
                genericCache._Add(new LockedTypeCollection(typeParameters), (TType)targetType);
        }

        public bool ContainsGenericType(ITypeCollectionBase typeParameters, out TType r)
        {
            if (this.genericCache == null || typeParameters == null)
            {
                r = default(TType);
                return false;
            }
            ITypeCollectionBase familliarSeries = ObtainGenericFamilliar(typeParameters);
            if (familliarSeries == null)
            {
                r = default(TType);
                return false;
            }
            r = this.genericCache[familliarSeries];
            return true;
        }

        public void UnregisterGenericType(ITypeCollectionBase typeParameters)
        {
            if (this.syncObject == null)
                return;
            if (typeParameters == null)
                return;
            if (this.exodusCache != null)
            {
                lock (this.exodusCache)
                    this.exodusCache.Add(typeParameters);
                return;
            }
            if (this.genericCache == null || this.disposing)
                return;
            ITypeCollectionBase familliarSeries = ObtainGenericFamilliar(typeParameters);
            if (familliarSeries == null)
                return;
            /* *
             * Multi-threading requirement, if the generic type which has been
             * disposed is the result of this disposing, this very well could
             * occur.
             * */

            lock (this.syncObject)
                if (this.genericCache != null)
                    genericCache._Remove(familliarSeries);
        }

        #endregion



        public void BeginExodus()
        {
            if (this.exodusCache != null)
                return;
            this.exodusCache = new HashSet<ITypeCollectionBase>();
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
                    TType[] genericCacheCopy;
                    lock (this.syncObject)
                        genericCacheCopy = this.genericCache.Values.ToArray();
                    Parallel.ForEach(genericCacheCopy, genericEntity =>
                        genericEntity.Dispose());
                    lock (this.syncObject)
                    {
                        this.genericCache._Clear();
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
            ITypeCollectionBase[] exodusCopy;
            lock (exodusCache)
            {
                exodusCopy = exodusCache.ToArray();
                exodusCache.Clear();
                if (exodusCopy.Length == 0)
                    return;
            }
            ITypeCollectionBase[] keyCopy;
            lock (this.syncObject)
                keyCopy = this.genericCache.Keys.ToArray();

            Parallel.For(0, exodusCopy.Length, exodusSetIndex =>
            {
                var currentSet = exodusCopy[exodusSetIndex];
                //Parallel.For(0, keyCopy.Length, (keySetIndex, keySetLoopState) =>
                for (int keySetIndex = 0; keySetIndex < keyCopy.Length; keySetIndex++)
                {
                    var currentKeySet = keyCopy[keySetIndex];
                    if (currentKeySet.Count != currentSet.Count)
                        return;
                    bool allFound = true;
                    //Parallel.For(0, currentSet.Count, (currentElementIndex, sequenceLoopState) =>
                    for (int currentElementIndex = 0; currentElementIndex < currentSet.Count; currentElementIndex ++)
                    {
                        IType currentElement = currentSet[currentElementIndex];
                        IType currentAlternate = currentKeySet[currentElementIndex];
                        if (!currentAlternate.Equals(currentElement))
                        {
                            allFound = false;
                            break;
                            //return;
                        }
                    }//);
                    if (allFound)
                    {
                        var currentLocked = currentKeySet as ILockedTypeCollection;
                        if (currentLocked != null && currentLocked.IsDisposed)
                            return;
                        lock (genericCache)
                            genericCache._Remove(currentKeySet);
                        break;
                        //keySetLoopState.Stop();
                    }
                }//);
            });
            exodusCache = null;
        }


        private ITypeCollectionBase ObtainGenericFamilliar(ITypeCollectionBase typeParameters)
        {
            ITypeCollectionBase familliar = null;
            ITypeCollectionBase[] keyCopy;
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
                var currentSet = keyCopy[i];
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
                    var currentLocked = currentSet as ILockedTypeCollection;
                    if (currentLocked != null && currentLocked.IsDisposed)
                        return;
                    familliar = currentSet;
                    parallelLoopState.Stop();
                }
            });
            return familliar;
        }


        #region IEnumerable<IType> Members

        public IEnumerator<TType> GetEnumerator()
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
    }
}
