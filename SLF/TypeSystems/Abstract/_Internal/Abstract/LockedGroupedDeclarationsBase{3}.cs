﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


/* *
 * The reason for creating a duplicate hierarcy of 
 * DeclarationBase derived classes is to ensure that 
 * there's little chance for someone to accidentally
 * enter a member that doesn't exist on a compiled type.  
 * This is important for the linking the system 
 * provides.
 * */
namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    /// <summary>
    /// Defines properties and methods for an <see cref="IDeclarationDictionary{TItem}"/> that is locked.
    /// </summary>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/> contained by the 
    /// <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.</typeparam>
    /// <typeparam name="TMItem">The type of member in the master 
    /// dictionary.</typeparam>
    /// <typeparam name="TSourceItem">The type of member in the 
    /// subordinate dictionary which is denoted as the source item
    /// for the current dictionary.</typeparam>
    internal abstract partial class LockedGroupedDeclarationsBase<TItem, TMItem, TSourceItem> :
        SubordinateDictionary<string, TItem, TMItem>,
        IGroupedDeclarationDictionary<TItem>,
        IGroupedDeclarationDictionary,
        _LockedGroupDeclarationsMasterPass
        where TItem :
            class,
            TMItem
        where TMItem :
            class,
            IDeclaration
        where TSourceItem :
            class
    {

        /// <summary>
        /// State indicates that the fetch methods are utilized in place of
        /// the base dictionary.
        /// </summary>
        private const int USE_FETCH = 1;
        /// <summary>
        /// State indicates that the base methods are utilized.
        /// </summary>
        private const int USE_BASE = 0;
        private int state;
        private TSourceItem[] sourceData;
        private Func<TSourceItem, TItem> fetchImpl;
        protected abstract string FetchKey(TSourceItem item);

        protected TItem Fetch(TSourceItem item)
        {
            if (this.fetchImpl == null)
                throw new InvalidOperationException();
            return fetchImpl(item);
        }

        protected LockedGroupedDeclarationsBase(MasterDictionaryBase<string, TMItem> master, TSourceItem[] sourceData, Func<TSourceItem, TItem> fetchImpl)
            : base(master)
        {
            this.state = USE_FETCH;
            this.sourceData = sourceData;
            this.fetchImpl = fetchImpl;
            ((_LockedRelativeHelper<TMItem>)master).RelateSubordinateMembers<TItem, TSourceItem>(sourceData, this);
        }

        /// <summary>
        /// Creates a new <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> with the <paramref name="master"/>
        /// dictionary provided.
        /// </summary>
        /// <param name="master">The <see cref="LockedFullDeclarations{TItem}"/>
        /// which moderates the <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.</param>
        internal LockedGroupedDeclarationsBase(MasterDictionaryBase<string, TMItem> master)
            : base(master)
        {
            this.state = USE_BASE;
        }

        /// <summary>
        /// Creates a new <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> with the <paramref name="items"/>
        /// to contain.
        /// </summary>
        /// <param name="master">The <see cref="LockedFullDeclarations{TItem}"/>
        /// which moderates the <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.</param>
        /// <param name="items">The <see cref="IDictionary{TKey, TValue}"/> 
        /// to encapsulate.</param>
        internal LockedGroupedDeclarationsBase(MasterDictionaryBase<string, TMItem> master, Dictionary<string, TItem> items)
            : base(master, items)
        {
            this.state = USE_BASE;
        }

        /// <summary>
        /// Creates a new <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> with the <paramref name="target"/>
        /// to contain.
        /// </summary>
        /// <param name="master">The <see cref="LockedFullDeclarations{TItem}"/>
        /// which moderates the <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.</param>
        /// <param name="target">The <see cref="IEnumerable{T}"/> of <typeparamref name="TItem"/> instances
        /// the <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> will contain.</param>
        internal LockedGroupedDeclarationsBase(MasterDictionaryBase<string, TMItem> master, IEnumerable<TItem> target)
            : base(master)
        {
            this.state = USE_BASE;
            foreach (TItem ti in target)
            {
                if (master != null)
                    master.Subordinate_ItemAdded(this, ti.UniqueIdentifier, ti);
                dictionaryCopy.Add(ti.UniqueIdentifier, ti);
            }
        }

        #region IDictionary<string,TItem> Members

        /// <summary>
        /// Adds an element of the provided <paramref name="key"/> and <paramref name="value"/> 
        /// to the <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.
        /// </summary>
        /// <param name="key">The <see cref="IDeclaration.UniqueIdentifier"/> of the current <paramref name="value"/></param>
        /// <param name="value">The <typeparamref name="TItem"/> to insert.</param>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> does not
        /// support modification.</exception>
        protected override void Add(string key, TItem value)
        {
            throw new NotSupportedException("Declarations locked.");
        }

        /// <summary>
        /// Removes an element with the specified <paramref name="key"/> from the 
        /// <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.
        /// </summary>
        /// <param name="key">The key of the <typeparamref name="TItem"/> to remove.</param>
        /// <returns>true if the element was successfully removed; false otherwise.</returns>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> does 
        /// not support modification.</exception>
        protected override bool Remove(string key)
        {
            throw new NotSupportedException("Declarations locked.");
        }

        /// <summary>
        /// Gets the value associated with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The <see cref="IDeclaration.UniqueIdentifier"/> to look for.</param>
        /// <returns>A <typeparamref name="TItem"/> relative to <paramref name="key"/>.</returns>
        /// <exception cref="System.NotSupportedException">The <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> does 
        /// not support modification.</exception>
        public override TItem this[string key]
        {
            get
            {
                if (this.state == USE_FETCH)
                {
                    if (this.ContainsKey(key))
                        return this.Values[((_KeysCollection)this.Keys).IndexOf(key)];
                    throw new ArgumentException("key");
                }
                else
                    return base[key];
            }
            protected set
            {
                throw new NotSupportedException("Declarations locked.");
            }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            if (this.state == USE_FETCH)
            {
                _ValuesCollection vc = ((_ValuesCollection)this.valuesCollection);
                _KeysCollection kc = ((_KeysCollection)this.keysCollection);
                if (vc != null)
                    vc.Dispose();
                if (kc != null)
                    kc.Dispose();
                this.keysCollection = null;
                this.valuesCollection = null;
            }
            else
            {
                foreach (var v in this.Values)
                    v.Dispose();
            }
        }

        #endregion

        #region _LockedGroupDeclarationsMasterPass Members

        IDeclaration _LockedGroupDeclarationsMasterPass.Fetch(object source)
        {
            if (!(source is TSourceItem))
                throw new ArgumentException("source");
            for (int i = 0; i < this.sourceData.Length; i++)
                if (this.sourceData[i] == source)
                    return this.Values[i];
            throw new ArgumentOutOfRangeException("source");
        }

        string _LockedGroupDeclarationsMasterPass.FetchKey(object source)
        {
            if (!(source is TSourceItem))
                throw new ArgumentException("source");
            for (int i = 0; i < this.sourceData.Length; i++)
                if (this.sourceData[i] == source)
                    return this.Keys[i];
            throw new ArgumentOutOfRangeException("source");
        }

        #endregion

        protected override ControlledStateDictionary<string, TItem>.KeysCollection InitializeKeysCollection()
        {
            if (this.state == USE_FETCH)
                return new _KeysCollection(this);
            else
                return base.InitializeKeysCollection();
        }

        protected override ControlledStateDictionary<string, TItem>.ValuesCollection InitializeValuesCollection()
        {
            if (this.state == USE_FETCH)
                return new _ValuesCollection(this);
            else
                return base.InitializeValuesCollection();
        }

        public override bool Contains(KeyValuePair<string, TItem> item)
        {
            if (this.state == USE_FETCH)
            {
                if (this.Keys.Contains(item.Key) && this.Values.Contains(item.Value))
                    return true;
                return false;
            }
            else
                return base.Contains(item);
        }
        public override KeyValuePair<string, TItem> this[int index]
        {
            get
            {
                if (this.state == USE_FETCH)
                    return new KeyValuePair<string, TItem>(this.Keys[index], this.Values[index]);
                else
                    return base[index];
            }
        }

        public override bool ContainsKey(string key)
        {
            if (this.state == USE_FETCH)
                return this.Keys.Contains(key);
            else
                return base.ContainsKey(key);
        }
        public override int Count
        {
            get
            {
                if (this.state == USE_FETCH)
                    return this.sourceData.Length;
                else
                    return base.Count;
            }
        }

        public override IEnumerator<KeyValuePair<string, TItem>> GetEnumerator()
        {
            if (state != USE_FETCH)
                return base.GetEnumerator();
            else
                return FetchEnumerator();
        }
        private IEnumerator<KeyValuePair<string, TItem>> FetchEnumerator()
        {
            for (int i = 0; i < this.sourceData.Length; i++)
                yield return new KeyValuePair<string, TItem>(this.Keys[i], this.Values[i]);
            yield break;
        }
        protected override TItem OnGetThis(string key)
        {
            if (this.state == USE_FETCH)
            {
                if (this.ContainsKey(key))
                    return this.Values[((_KeysCollection)this.Keys).IndexOf(key)];
                throw new ArgumentException("key");
            }
            else
                return base[key];
        }

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (decl is TItem)
                return this.IndexOf((TItem)(decl));
            throw new ArgumentException("decl");
        }

        public int IndexOf(TItem decl)
        {
            if (this.state == USE_FETCH)
            {
                for (int i = 0; i < this.Count; i++)
                    if (object.ReferenceEquals(((_ValuesCollection)this.Values).dataCopy[i], decl))
                        return i;
            }
            else
            {
                int index = 0;
                foreach (var v in Values)
                    if (object.ReferenceEquals(v, decl))
                        return index;
                    else
                        index++;
            }
            return -1;
        }
    }
}