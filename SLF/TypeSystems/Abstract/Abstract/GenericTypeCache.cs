using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Utilities.Collections;
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
        _IGenericClosureRegistrar,
        IDisposable,
        IEnumerable<IGenericType>,
        IMassTargetHandler
    {
        private Dictionary<ILockedTypeCollection, IGenericType> genericCache = null;
        private HashSet<ILockedTypeCollection> exodusCache;
        private object syncObject = new object();
        private bool disposing;
        #region _IGenericClosureRegistrar Members
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
        internal void RegisterGenericType(IGenericType targetType, ILockedTypeCollection typeParameters)
        {
            if (typeParameters == null)
                return;
            lock (syncObject)
            {
                if (this.genericCache == null)
                    this.genericCache = new Dictionary<ILockedTypeCollection, IGenericType>();
                if (this.genericCache.ContainsKey(typeParameters))
                    return;
                genericCache.Add(new LockedTypeCollection(typeParameters), (IGenericType)targetType);
            }
        }
        

        internal void UnregisterGenericType(ILockedTypeCollection typeParameters)
        {
            if (typeParameters == null)
                return;
            lock (this.syncObject)
            {
                if (this.exodusCache != null)
                {
                    this.exodusCache.Add(typeParameters);
                    return;
                }
                if (this.genericCache == null || this.disposing)
                    return;
                IGenericType unregistered;
                if (this.genericCache.TryGetValue(typeParameters, out unregistered))
                {
                    unregistered.Dispose();
                    this.genericCache.Remove(typeParameters);
                }
            }
        }

        #endregion

        /// <summary>
        /// Begins an exodus upon the <see cref="GenericTypeCache"/>.
        /// </summary>
        public void BeginExodus()
        {
            lock (this.syncObject)
                if (this.exodusCache != null)
                    return;
            this.exodusCache = new HashSet<ILockedTypeCollection>();
        }

        /// <summary>
        /// Ends an exodus upon the <see cref="GenericTypeCache"/>.
        /// </summary>
        public void EndExodus()
        {
            ILockedTypeCollection[] exodusCopy;
            lock (this.syncObject)
            {
                if (this.exodusCache == null)
                    return;
                if (exodusCache.Count == 0)
                {
                    exodusCache = null;
                    return;
                }
                exodusCopy = exodusCache.ToArray();
                exodusCache.Clear();
                if (exodusCopy.Length == 0)
                    return;
            }
            if (exodusCopy.Length > 0)
            {
                Dictionary<ILockedTypeCollection, IGenericType> genericCacheCopy = null;
                lock (this.syncObject)
                {
                    if (this.genericCache == null)
                    {
                        this.exodusCache = null;
                        return;
                    }
                    genericCacheCopy = new Dictionary<ILockedTypeCollection, IGenericType>(genericCache);
                }
                for (int i = 0; i < exodusCopy.Length; i++)
                {
                    var set = exodusCopy[i];
                    var removed = genericCacheCopy[set];
                    removed.Dispose();
                    genericCacheCopy.Remove(set);
                }
                lock (this.syncObject)
                    this.genericCache = genericCacheCopy;
            }
            lock (this.syncObject)
                exodusCache = null;
        }

        #region IDisposable Members

        /// <summary>
        /// Disposes the <see cref="GenericTypeCache"/>.
        /// </summary>
        public void Dispose()
        {
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
                {
                    genericCacheCopy = this.genericCache.Values.ToArray();
                    this.genericCache.Clear();
                    this.genericCache = null;
                }
                Parallel.ForEach(genericCacheCopy, genericEntity =>
                    genericEntity.Dispose());
            }
            finally
            {
                lock (this.syncObject)
                    this.disposing = false;
            }
        }

        #endregion

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

        #region _IGenericClosureRegistrar Members

        void _IGenericClosureRegistrar.RegisterGenericClosure(IGenericType targetType, ILockedTypeCollection typeParameters)
        {
            this.RegisterGenericType(targetType, typeParameters);
        }

        void _IGenericClosureRegistrar.UnregisterGenericClosure(ILockedTypeCollection typeParameters)
        {
            this.UnregisterGenericType(typeParameters);
        }

        public bool TryObtainGenericClosure(ILockedTypeCollection typeParameters, out IGenericType genericClosure)
        {
            lock (this.syncObject)
            {
                if (this.genericCache == null)
                {
                    genericClosure = null;
                    return false;
                }
                return this.genericCache.TryGetValue(typeParameters, out genericClosure);
            }
        }

        public bool ContainsGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.syncObject)
            {
                if (this.genericCache == null)
                    return false;
                return this.genericCache.ContainsKey(typeParameters);
            }
        }

        public IGenericType ObtainGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.syncObject)
            {
                if (this.genericCache == null)
                    return null;
                return this.genericCache[typeParameters];
            }
        }

        #endregion
    }
}
