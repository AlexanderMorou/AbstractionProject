using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class TypeModifiedCache :
        _IConstructCacheRegistrar<IModifiedType, ITypeModifierSetEntry>,
        IDisposable
    {
        private IDictionary<ITypeModifierSetEntry, IModifiedType> cacheDictionary = new Dictionary<ITypeModifierSetEntry, IModifiedType>();
        private object syncObject = new object();
        #region IDisposable Members

        public void Dispose()
        {
            lock (syncObject)
            {
                this.cacheDictionary.Values.OnAll(p => p.Dispose());
                this.cacheDictionary.Clear();
            }
        }

        #endregion


        #region _IConstructCacheRegistrar<IModifiedType,ITypeModificationEntry> Members

        public void RegisterConstruct(ITypeModifierSetEntry cacheKey, IModifiedType cachedType)
        {
            lock (this.syncObject)
                this.cacheDictionary.Add(cacheKey, cachedType);
        }

        public bool ContainsConstruct(ITypeModifierSetEntry cacheKey)
        {
            lock (this.syncObject)
                return this.cacheDictionary.ContainsKey(cacheKey);
        }

        public IModifiedType ObtainConstruct(ITypeModifierSetEntry cacheKey)
        {
            IModifiedType result;
            lock (this.syncObject)
                if (this.cacheDictionary.TryGetValue(cacheKey, out result))
                    return result;
            return null;
        }

        public void UnregisterConstruct(ITypeModifierSetEntry cacheKey)
        {
            lock (this.syncObject)
            {
                IModifiedType placeholder;
                if (this.cacheDictionary.TryGetValue(cacheKey, out placeholder))
                {
                    this.cacheDictionary.Remove(cacheKey);
                    placeholder.Dispose();
                    placeholder = null;
                }
            }
        }

        public bool TryObtainConstruct(ITypeModifierSetEntry cacheKey, out IModifiedType cachedType)
        {
            lock (this.syncObject)
                return this.cacheDictionary.TryGetValue(cacheKey, out cachedType);
        }

        #endregion
    }
}
