﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a master dictionary implementation that contains a series 
    /// of subordinate sub-type dictionaries using the same 
    /// <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of key to use in the <see cref="MasterDictionaryBase{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of value to use in the 
    /// <see cref="MasterDictionaryEntry{TEntry}"/> values.</typeparam>
    public abstract class MasterDictionaryBase<TKey, TValue> :
        ControlledDictionary<TKey, MasterDictionaryEntry<TValue>>,
        IMasterDictionary<TKey, TValue>,
        IMasterDictionary
        where TValue :
            class
    {
        #region MasterDictionaryBase<TKey, TValue> Data members

        private List<ISubordinateDictionary> subordinates;
        private IEnumerable<ISubordinateDictionary> subordinates_Enum;

        #endregion

        #region MasterDictionaryBase<TKey, TValue> Constructors
        /// <summary>
        /// Creates a new <see cref="MasterDictionaryBase{TKey, TValue}"/>
        /// with the <paramref name="sibling"/> provided.
        /// </summary>
        /// <param name="sibling">The <see cref="MasterDictionaryBase{TKey, TValue}"/> 
        /// to encapsulate.</param>
        protected MasterDictionaryBase(MasterDictionaryBase<TKey, TValue> sibling)
            : base(sibling)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new <see cref="MasterDictionaryBase{TKey, TValue}"/> 
        /// initialized to a default state.
        /// </summary>
        protected MasterDictionaryBase()
        {
            Initialize();
        }
        #endregion

        private void Initialize()
        {
            this.subordinates = new List<ISubordinateDictionary>();
        }

        /// <summary>
        /// Initializes the subordinates enumerator.
        /// </summary>
        private void InitializeSubordinatesEnum()
        {
            /* *
             * Use an encapsulation of the subordinates
             * so that they can't as easily modify the list
             * without going through the proper
             * channels.
             * */
            this.subordinates_Enum = from item in subordinates
                                     select item;
        }

        /// <summary>
        /// Adds a subordinate to to the <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TSKey">The kind of key within the subordinate
        /// dictionary.</typeparam>
        /// <typeparam name="TSValue">The sub-type of <typeparamref name="TValue"/>
        /// that the elements of the <paramref name="subordinate"/>
        /// consist of.</typeparam>
        /// <param name="subordinate">
        /// The <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/>
        /// which is to become a subordinate of the 
        /// current <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="subordinate"/> is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// thrown when <paramref name="subordinate"/> is not derived from 
        /// <see cref="SubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/> or when
        /// a subordinate of the given <typeparamref name="TSValue"/> 
        /// already exists.</exception>
        protected internal void AddSubordinate<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate)
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            AddSubordinateImpl<TSKey, TSValue>(subordinate);
        }

        internal virtual void AddSubordinateImpl<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate)
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            if (subordinate == null)
                throw new ArgumentNullException("subordinate");
            if (!(subordinate is _ISubordinateDictionaryMasterPass))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.subordinate, ExceptionMessageId.SubordinateInvalid);
            foreach (ISubordinateDictionary isd in this.subordinates)
                if (isd is ISubordinateDictionary<TSKey, TKey, TSValue, TValue>)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.subordinate, ExceptionMessageId.SubordinateSubTypeDuplicate);
            this.subordinates.Add((ISubordinateDictionary)subordinate);
        }

        #region IMasterDictionary<TKey,TValue> Members


        /// <summary>
        /// Returns an enumerable entity that contains the 
        /// <see cref="ISubordinateDictionary"/> instances
        /// managed by the current <see cref="MasterDictionaryBase{TKey, TValue}"/>
        /// </summary>
        public IEnumerable<ISubordinateDictionary> Subordinates
        {
            get
            {
                if (subordinates_Enum == null)
                    InitializeSubordinatesEnum();
                return subordinates_Enum;
            }
        }

        #endregion

        #region Subordinate Notification Methods

        /// <summary>
        /// Notifier for subordinate dictionaries 
        /// indicating that an item has been added.
        /// </summary>
        /// <typeparam name="TSKey">The kind of key within the subordinate
        /// dictionary.</typeparam>
        /// <typeparam name="TSValue">
        /// The specific type of value used in the 
        /// subordinate dictionary, derives from the 
        /// master dictionary's <typeparamref name="TValue"/>.
        /// </typeparam>
        /// <param name="subordinate">
        /// The <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/>
        /// which is a subordinate of the current 
        /// <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </param>
        /// <param name="key">The <typeparamref name="TKey"/> of the item added.</param>
        /// <param name="value">The <typeparamref name="TSValue"/> of the item added.</param>
        protected internal virtual void Subordinate_ItemAdded<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate, TSKey key, TSValue value)
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            base._Add(key, new MasterDictionaryEntry<TValue>((ISubordinateDictionary)subordinate, value));
        }

        protected internal virtual void Subordinate_ItemsAdded<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate, IEnumerable<KeyValuePair<TSKey, TSValue>> items)
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            foreach (var element in items)
                base._Add(element.Key, new MasterDictionaryEntry<TValue>((ISubordinateDictionary)subordinate, element.Value));
        }

        /// <summary>
        /// Notifier for subordinate dictionaries 
        /// indicating that an item has been changed.
        /// </summary>
        /// <typeparam name="TSKey">The kind of key within the subordinate
        /// dictionary.</typeparam>
        /// <typeparam name="TSValue">
        /// The specific type of value used in the 
        /// subordinate dictionary, derives from the 
        /// master dictionary's <typeparamref name="TValue"/>.
        /// </typeparam>
        /// <param name="subordinate">
        /// The <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/>
        /// which is a subordinate of the current 
        /// <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </param>
        /// <param name="key">
        /// The <typeparamref name="TKey"/> of the item changed.
        /// </param>
        /// <param name="value">
        /// The <typeparamref name="TSValue"/> of the item 
        /// changed.
        /// </param>
        protected internal virtual void Subordinate_ItemChanged<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate, TSKey key, TSValue value)
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            base[key] = new MasterDictionaryEntry<TValue>((ISubordinateDictionary)subordinate, value);
        }

        /// <summary>
        /// Notifier for subordinate dictionaries 
        /// indicating that an item has been removed.
        /// </summary>
        /// <typeparam name="TSKey">The kind of key within the subordinate
        /// dictionary.</typeparam>
        /// <typeparam name="TSValue">
        /// The specific type of value used in the 
        /// subordinate dictionary, derives from the 
        /// master dictionary's <typeparamref name="TValue"/>.
        /// </typeparam>
        /// <param name="subordinate">
        /// The <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/>
        /// which is a subordinate of the current 
        /// <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </param>
        /// <param name="key">
        /// The <typeparamref name="TSKey"/> of the item removed.
        /// </param>
        protected internal virtual void Subordinate_ItemRemoved<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate, TSKey key)
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            int index = this.Keys.IndexOf(key);
            /* *
             * The cause behind this is a caching issue.  Since the CLI types
             * are singletons, clearing the cache causes the signatures associated
             * to the members to become invalid since they referenced a type whose
             * underlying system type is now null.
             * */
            if (index == -1)
                return;
            MasterDictionaryEntry<TValue> entry = this.Values[index];

            if (entry.Subordinate != subordinate)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.subordinate, ExceptionMessageId.SubordinateMismatch);
            base._Remove(index);
        }

        /// <summary>
        /// Notifier for subordinate dictionaries 
        /// indicating that the <paramref name="subordinate"/>
        /// has been cleared.
        /// </summary>
        /// <typeparam name="TSValue">
        /// The specific type of value used in the 
        /// subordinate dictionary, derives from the 
        /// master dictionary's <typeparamref name="TValue"/>.
        /// </typeparam>
        /// <param name="subordinate">
        /// The <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/>
        /// which is a subordinate of the current 
        /// <see cref="MasterDictionaryBase{TKey, TValue}"/> that was
        /// cleared.
        /// </param>
        protected internal virtual void Subordinate_Cleared<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate)
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            var clearHelper = new _RemoveHelperCache<TSKey, TSValue>(this);
            var elements = GetSubordinateClearedItems<TSKey, TSValue>(subordinate).ToArray();
            elements.OnAll((kvp) =>
                 clearHelper.__Subordinate_Cleared__removeHelper_cache((ISubordinateDictionary<TSKey, TKey, TSValue, TValue>)kvp.Value.Subordinate, (TSKey)kvp.Key));
        }

        internal virtual  IEnumerable<KeyValuePair<TKey, MasterDictionaryEntry<TValue>>> GetSubordinateClearedItems<TSKey, TSValue>(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate)
            where TSKey : 
                TKey
            where TSValue : 
                TValue
        {
            return from kvp in this
                   where kvp.Value.Subordinate == subordinate &&
                         kvp.Value.Entry is TValue &&
                         kvp.Key is TSKey
                   select kvp;
        }
        private class _RemoveHelperCache<TSKey, TSValue>
            where TSKey :
                TKey
            where TSValue :
                TValue
        {
            internal Action<ISubordinateDictionary<TSKey, TKey, TSValue, TValue>, TSKey> __Subordinate_Cleared__removeHelper_cache;
            private MasterDictionaryBase<TKey, TValue> master;
            public _RemoveHelperCache(MasterDictionaryBase<TKey, TValue> master)
            {
                this.master = master;
                this.__Subordinate_Cleared__removeHelper_cache = new Action<ISubordinateDictionary<TSKey, TKey, TSValue, TValue>, TSKey>(__Subordinate_Cleared__removeHelper);
            }
            /// <remarks>Due to compiler warning CS1911</remarks>
            public void __Subordinate_Cleared__removeHelper(ISubordinateDictionary<TSKey, TKey, TSValue, TValue> subordinate, TSKey k)
            {
                this.master.Subordinate_ItemRemoved<TSKey, TSValue>(subordinate, k);
            }
        }

        #endregion

        /// <summary>
        /// Adds an element of the provided <paramref name="key"/> 
        /// and <paramref name="value"/> to the
        /// <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the
        /// current <paramref name="value"/> inserted.</param>
        /// <param name="value">The <typeparamref name="TValue"/>
        /// to insert.</param>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <see cref="MasterDictionaryEntry{TEntry}.Subordinate"/> in 
        /// <paramref name="value"/> is: null or not a subordinate 
        /// of the <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </exception>
        protected internal override void _Add(TKey key, MasterDictionaryEntry<TValue> value)
        {
            if (value.Subordinate == null)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.SubordinateNull);
            if (!this.subordinates.Contains(value.Subordinate))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.SubordinateDoesNotExist);
            /* *
             * Don't call base class' add, the subordinate will 
             * invoke Subordinate_ItemAdded<TSValue> which will
             * invoke the base add
             * */
            ((_ISubordinateDictionaryMasterPass)(value.Subordinate)).Add(key, value.Entry);
        }

        /// <summary>
        /// Removes all entries from the <see cref="MasterDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        protected internal override void _Clear()
        {
            /* *
             * Again, don't call the base class' clear
             * the subordinates will individually call
             * their clears, removing their elements
             * individually.
             * */
            foreach (ISubordinateDictionary isd in this.subordinates)
                ((_ISubordinateDictionaryMasterPass)isd).Clear();
            
        }

        protected internal override bool _Remove(int index)
        {
            try
            {
                /* *
                 * Don't call base class' add, the subordinate will 
                 * invoke Subordinate_ItemRemoved<TSValue> which will
                 * invoke the base remove
                 * */
                var currentEntry = this[index];
                ((_ISubordinateDictionaryMasterPass)(currentEntry.Value.Subordinate)).Remove(currentEntry.Key);
            }
            catch
            {
                //Key not found exception should result if it doesn't exist?
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sets the value associated with the specified 
        /// <paramref name="key"/>.</summary>
        /// <param name="key">The <typeparamref name="TKey"/>
        /// to look for.</param>
        /// <param name="value">The value to set the element, 
        /// at the <paramref name="key"/> provided, to.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <see cref="MasterDictionaryEntry{TEntry}.Subordinate"/> in 
        /// value is: null, or not a subordinate 
        /// of the <see cref="MasterDictionaryBase{TKey, TValue}"/>,
        /// or tries to alter which subordinate the 
        /// <paramref name="key"/> points to.
        /// </exception>
        protected override void OnSetThis(TKey key, MasterDictionaryEntry<TValue> value)
        {
            if (value.Subordinate == null)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.SubordinateNull);
            if (!this.subordinates.Contains(value.Subordinate))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.SubordinateDoesNotExist);
            if (base[key].Subordinate != value.Subordinate)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.SubordinateCannotChange);
            /* *
             * This is the primary reason that 
             * MasterDictionaryEntry<TEntry> contains
             * a 'value'.
             * */
            ((_ISubordinateDictionaryMasterPass)(this[key].Subordinate))[key] = value.Entry;
        }

        #region IMasterDictionary Members

        IEnumerable IMasterDictionary.Subordinates
        {
            get { return (IEnumerable)this.subordinates; }
        }

        #endregion
    }
}
