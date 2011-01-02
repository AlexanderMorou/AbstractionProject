using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a root implementation of 
    /// <see cref="ISubordinateDictionary{TKey, TSValue, TMValue}"/>
    /// as a subordinate of a larger 
    /// <see cref="MasterDictionaryBase{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of key used in the 
    /// subordinate and master dictionaries.</typeparam>
    /// <typeparam name="TSValue">The sub-type of 
    /// <typeparamref name="TMValue"/> that the 
    /// <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/> 
    /// uses for its elements.</typeparam>
    /// <typeparam name="TMValue"></typeparam>
    [DebuggerDisplay("Count: {Count}")]
    public partial class SubordinateDictionary<TKey, TSValue, TMValue> :
        ControlledStateDictionary<TKey, TSValue>,
        ISubordinateDictionary<TKey, TSValue, TMValue>,
        ISubordinateDictionary,
        _ISubordinateDictionaryMasterPass
        where TMValue :
            class
        where TSValue :
            TMValue
    {
        private int version;
        bool needsRefresh = false;

        /// <summary>
        /// Data member for <see cref="Master"/>.
        /// </summary>
        private MasterDictionaryBase<TKey, TMValue> master;

        /// <summary>
        /// Creates a new <see cref="SubordinateDictionary{TKey, TSValue, TValue}"/> 
        /// with the <paramref name="master"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="MasterDictionaryBase{TKey, TMValue}"/>
        /// in which the <see cref="SubordinateDictionary{TKey, TSValue, TValue}"/> resides.</param>
        protected SubordinateDictionary(MasterDictionaryBase<TKey, TMValue> master) 
            : base()
        {
            if (master == null)
                return;
            this.master = master;
            this.master.AddSubordinate(this);
        }

        /// <summary>
        /// Creates a new <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>
        /// with the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="MasterDictionaryBase{TKey, TMValue}"/>
        /// in which the <see cref="SubordinateDictionary{TKey, TSValue, TValue}"/> resides.</param>
        /// <param name="items">The <see cref="Dictionary{TKey, TSValue}"/>
        /// to wrap.</param>
        protected SubordinateDictionary(MasterDictionaryBase<TKey, TMValue> master, SubordinateDictionary<TKey, TSValue, TMValue> sibling)
            : base(sibling)
        {
            this.master = master;
        }

        #region ISubordinateDictionary<TKey,TSValue,TMValue> Members

        public MasterDictionaryBase<TKey, TMValue> Master
        {
            get
            {
                return this.master;
            }
        }

        /// <summary>
        /// Returns the <see cref="IMasterDictionary{TKey, TValue}"/> which
        /// contains and moderates the current
        /// <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>.
        /// </summary>
        IMasterDictionary<TKey, TMValue> ISubordinateDictionary<TKey,TSValue,TMValue>.Master
        {
            get { return this.Master; }
        }

        #endregion

        /// <summary>
        /// Adds an element of the provided <paramref name="key"/>
        /// and <paramref name="value"/> to the
        /// <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the
        /// current <paramref name="value"/> inserted.</param>
        /// <param name="value">The <typeparamref name="TValue"/>
        /// to insert.</param>
        protected internal override void _Add(TKey key, TSValue value)
        {
            if (this.Master != null)
                this.master.Subordinate_ItemAdded(this, key, value);
            base._Add(key, value);
        }

        /// <summary>
        /// Removes an element with the specified <paramref name="key"/>
        /// from the  <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>.
        /// </summary>
        /// <param name="key">The key of the <typeparamref name="TValue"/> to remove.</param>
        /// <returns>true if the element was successfully removed; false otherwise.</returns>
        protected internal virtual bool Remove(TKey key)
        {
            if (this.Master != null)
                this.master.Subordinate_ItemRemoved(this, key);
            return base._Remove(key);
        }

        /// <summary>
        /// Removes all entries from the 
        /// <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>.
        /// </summary>
        protected internal override void _Clear()
        {
            if (this.Master != null)
                this.master.Subordinate_Cleared(this);
            this.version = 0;
            this.needsRefresh = false;
            base._Clear();
        }

        /// <summary>
        /// Gets/sets the value associated with the specified 
        /// <paramref name="key"/>.</summary>
        /// <param name="key">The 
        /// <typeparamref name="TKey"/> 
        /// to look for.</param>
        /// <returns>A <typeparamref name="TValue"/> relative 
        /// to <paramref name="key"/>.</returns>
        public new virtual TSValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            protected set
            {
                if (this.Master != null)
                    this.master.Subordinate_ItemChanged(this, key, value);
                base[key] = value;
            }
        }

        #region ISubordinateDictionary Members

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return (IMasterDictionary)this.Master; }
        }

        #endregion

        /// <summary>
        /// Converts the current <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/> to
        /// a string form.
        /// </summary>
        /// <returns>A <see cref="System.String"/> representing the current <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>
        /// instance.</returns>
        public override string ToString()
        {
            return string.Format("{0} of {1} ({2})", this.Count, typeof(TSValue).Name, typeof(TMValue).Name);
        }

        #region _ISubordinateDictionaryMasterPass Members

        void _ISubordinateDictionaryMasterPass.Add(object key, object value)
        {
            base._Add((TKey)key, (TSValue)value);
        }

        void _ISubordinateDictionaryMasterPass.Clear()
        {
            this.version = 0;
            this.needsRefresh = false;
            this._Clear();
        }

        void _ISubordinateDictionaryMasterPass.Remove(object key)
        {
            base._Remove((TKey)key);
        }

        object _ISubordinateDictionaryMasterPass.this[object key]
        {
            set
            {
                base[(TKey)key] = (TSValue)value;
            }
        }

        #endregion

        protected override ControlledStateDictionary<TKey, TSValue>.KeysCollection InitializeKeysCollection()
        {
            return new KeysCollection(this);
        }

        protected virtual TKey RekeyElement(KeyValuePair<TKey, TSValue> kvp)
        {
            return kvp.Key;
        }

    }
}
