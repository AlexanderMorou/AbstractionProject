using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        /// with the <paramref name="master"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="MasterDictionaryBase{TKey, TMValue}"/>
        /// in which the <see cref="SubordinateDictionary{TKey, TSValue, TValue}"/> resides.</param>
        /// <param name="sibling">The <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>
        /// which is the sibling of the new <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>
        /// and therefore contains the same dictionary of elements.</param>
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
        /// <param name="value">The <typeparamref name="TSValue"/>
        /// to insert.</param>
        protected internal override void _Add(TKey key, TSValue value)
        {
            if (this.Master != null)
                this.master.Subordinate_ItemAdded(this, key, value);
            base._Add(key, value);
        }

        /// <summary>
        /// Removes an element with the specified <paramref name="index"/>
        /// from the <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value of the ordinal index of 
        /// the <typeparamref name="TSValue"/> to remove.</param>
        /// <returns>true if the element was successfully removed; false otherwise.</returns>
        protected internal override bool _Remove(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException();
            var key = this.Keys[index];
            if (this.Master != null)
                this.master.Subordinate_ItemRemoved(this, key);
            return base._Remove(index);
        }

        /// <summary>
        /// Removes all entries from the 
        /// <see cref="SubordinateDictionary{TKey, TSValue, TMValue}"/>.
        /// </summary>
        protected internal override void _Clear()
        {
            if (this.Master != null)
                this.master.Subordinate_Cleared(this);
            base._Clear();
        }

        /// <summary>
        /// Gets/sets the value associated with the specified 
        /// <paramref name="key"/>.</summary>
        /// <param name="key">The 
        /// <typeparamref name="TKey"/> 
        /// to look for.</param>
        /// <returns>A <typeparamref name="TSValue"/> relative 
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
