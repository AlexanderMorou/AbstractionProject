using System;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
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
    internal abstract partial class LockedGroupedDeclarationsBase<TItemIdentifier, TItem, TMItemIdentifier, TMItem, TSourceItem> :
        SubordinateDictionary<TItemIdentifier, TMItemIdentifier, TItem, TMItem>,
        IGroupedDeclarationDictionary<TItemIdentifier, TItem>,
        IGroupedDeclarationDictionary,
        _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>
        where TItemIdentifier :
            IDeclarationUniqueIdentifier,
            TMItemIdentifier
        where TItem :
            class,
            IDeclaration<TItemIdentifier>,
            TMItem
        where TMItemIdentifier :
            IDeclarationUniqueIdentifier
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
        private Func<TSourceItem, string> nameImpl;
        protected abstract TItemIdentifier FetchKey(TSourceItem item);

        protected TItem Fetch(TSourceItem item)
        {
            if (this.fetchImpl == null)
                throw new InvalidOperationException();
            return fetchImpl(item);
        }

        protected LockedGroupedDeclarationsBase(MasterDictionaryBase<TMItemIdentifier, TMItem> master, TSourceItem[] sourceData, Func<TSourceItem, TItem> fetchImpl, Func<TSourceItem, string> nameImpl)
            : base(master)
        {
            this.state = USE_FETCH;
            this.sourceData = sourceData;
            this.fetchImpl = fetchImpl;
            ((_LockedRelativeHelper<TMItemIdentifier, TMItem>)master).RelateSubordinateMembers<TItemIdentifier, TItem, TSourceItem>(sourceData, this);
            this.nameImpl = nameImpl;
        }

        /// <summary>
        /// Creates a new <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> with the <paramref name="master"/>
        /// dictionary provided.
        /// </summary>
        /// <param name="master">The <see cref="LockedFullDeclarations{TItem}"/>
        /// which moderates the <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.</param>
        internal LockedGroupedDeclarationsBase(MasterDictionaryBase<TMItemIdentifier, TMItem> master)
            : base(master)
        {
            this.state = USE_BASE;
        }

        /// <summary>
        /// Creates a new <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> with the <paramref name="sibling"/>
        /// to relate to.
        /// </summary>
        /// <param name="master">The <see cref="LockedFullDeclarations{TItem}"/>
        /// which moderates the <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/>.</param>
        /// <param name="sibling">The <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> 
        /// to encapsulate.</param>
        internal LockedGroupedDeclarationsBase(MasterDictionaryBase<TMItemIdentifier, TMItem> master, LockedGroupedDeclarationsBase<TItemIdentifier, TItem, TMItemIdentifier, TMItem, TSourceItem> sibling)
            : base(master, sibling)
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
        internal LockedGroupedDeclarationsBase(MasterDictionaryBase<TMItemIdentifier, TMItem> master, IEnumerable<TItem> target)
            : base(master)
        {
            this.state = USE_BASE;
            foreach (TItem ti in target)
            {
                if (master != null)
                    master.Subordinate_ItemAdded(this, ti.UniqueIdentifier, ti);
                this._Add(ti.UniqueIdentifier, ti);
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
        protected internal sealed override void _Add(TItemIdentifier key, TItem value)
        {
            throw new NotSupportedException("Declarations locked.");
        }

        /// <summary>
        /// Removes an element with the specified <paramref name="index"/>
        /// from the <see cref="SubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/>.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value of the ordinal index of 
        /// the <typeparamref name="TSValue"/> to remove.</param>
        /// <returns><see cref="System.NotSupportedException"/></returns>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="LockedGroupedDeclarationsBase{TItem, TMItem, TSourceItem}"/> does 
        /// not support modification.</exception>
        protected internal sealed override bool _Remove(int index)
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
        public override TItem this[TItemIdentifier key]
        {
            get
            {
                if (this.state == USE_FETCH)
                {
                    if (this.ContainsKey(key))
                        return this.Values[((_KeysCollection)this.Keys).IndexOf(key)];
                    throw new KeyNotFoundException();
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
                _ValuesCollection vc = ((_ValuesCollection)this.valuesInstance);
                _KeysCollection kc = ((_KeysCollection)this.keysInstance);
                if (vc != null)
                {
                    vc.Dispose();
                    this.valuesInstance = null;
                }
                if (kc != null)
                {
                    kc.Dispose();
                    this.keysInstance = null;
                }
            }
            else
                foreach (var v in this.Values)
                    v.Dispose();
        }

        #endregion

        #region _LockedGroupDeclarationsMasterPass Members

        TMItem _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>.Fetch(object source)
        {
            if (!(source is TSourceItem))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.source, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.source), source.GetType().ToString(), typeof(TSourceItem).ToString());
            for (int i = 0; i < this.sourceData.Length; i++)
                if (this.sourceData[i] == source)
                    return this.Values[i];
            throw new ArgumentOutOfRangeException("source");
        }

        TMItemIdentifier _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>.FetchKey(object source)
        {
            if (!(source is TSourceItem))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.source, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.source), source.GetType().ToString(), typeof(TSourceItem).ToString());
            for (int i = 0; i < this.sourceData.Length; i++)
                if (this.sourceData[i] == source)
                    return this.Keys[i];
            throw new ArgumentOutOfRangeException("source");
        }

        string _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>.FetchName(object source)
        {
            if (nameImpl == null)
                return null;
            if (source is TSourceItem)
                return nameImpl((TSourceItem)source);
            else
                return null;
        }
        #endregion

        protected override ControlledDictionary<TItemIdentifier, TItem>.KeysCollection InitializeKeysCollection()
        {
            if (this.state == USE_FETCH)
                return new _KeysCollection(this);
            else
                return base.InitializeKeysCollection();
        }

        protected override ControlledDictionary<TItemIdentifier, TItem>.ValuesCollection InitializeValuesCollection()
        {
            if (this.state == USE_FETCH)
                return new _ValuesCollection(this);
            else
                return base.InitializeValuesCollection();
        }

        public override bool Contains(KeyValuePair<TItemIdentifier, TItem> item)
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

        protected override KeyValuePair<TItemIdentifier, TItem> OnGetThis(int index)
        {
            if (this.state == USE_FETCH)
                return new KeyValuePair<TItemIdentifier, TItem>(this.Keys[index], this.Values[index]);
            else
                return base.OnGetThis(index);
        }

        public override bool ContainsKey(TItemIdentifier key)
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

        public override IEnumerator<KeyValuePair<TItemIdentifier, TItem>> GetEnumerator()
        {
            if (state != USE_FETCH)
                return base.GetEnumerator();
            else
                return FetchEnumerator();
        }
        private IEnumerator<KeyValuePair<TItemIdentifier, TItem>> FetchEnumerator()
        {
            for (int i = 0; i < this.sourceData.Length; i++)
                yield return new KeyValuePair<TItemIdentifier, TItem>(this.Keys[i], this.Values[i]);
            yield break;
        }

        protected override TItem OnGetThis(TItemIdentifier key)
        {
            if (this.state == USE_FETCH)
            {
                if (this.ContainsKey(key))
                    return this.Values[((_KeysCollection)this.Keys).IndexOf(key)];
                throw new KeyNotFoundException();
            }
            else
                return base[key];
        }

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (decl == null)
                return -1;
            if (decl is TItem)
                return this.IndexOf((TItem)(decl));
            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.decl, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.decl), decl.GetType().ToString(), typeof(TItem).ToString());
        }

        public int IndexOf(TItem decl)
        {
            if (decl == null)
                return -1;
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
