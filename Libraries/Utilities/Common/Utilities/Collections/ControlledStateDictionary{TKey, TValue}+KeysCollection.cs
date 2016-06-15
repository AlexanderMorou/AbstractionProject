using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class ControlledDictionary<TKey, TValue>
    {
        /// <summary>
        /// Provides a <see cref="IControlledCollection{T}"/> relative
        /// to the <see cref="Keys"/> of the <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </summary>
        public class KeysCollection :
            IControlledCollection<TKey>,
            IControlledCollection
        {
            private SharedLocals locals;
            /// <summary>
            /// Creates a new <see cref="KeysCollection"/> with the
            /// <paramref name="locals"/> provided.
            /// </summary>
            /// <param name="locals">The <see cref="SharedLocals"/> which
            /// denote the data source to use.</param>
            private KeysCollection(SharedLocals locals)
            {
                this.locals = locals;
            }

            /// <summary>
            /// Creates a new <see cref="KeysCollection"/> with the <paramref name="localOwner"/>
            /// provided.
            /// </summary>
            /// <param name="localOwner">The <see cref="ControlledDictionary"/>
            /// which contains the <see cref="KeysCollection"/>.</param>
            protected internal KeysCollection(ControlledDictionary<TKey, TValue> localOwner)
                : this(localOwner.locals)
            {
            }
            #region IControlledCollection<TKey> Members

            /// <summary>
            /// Gets the number of elements contained in the <see cref="KeysCollection"/>.</summary>
            /// <returns>
            /// The number of elements contained in the <see cref="KeysCollection"/>.</returns>
            public virtual int Count
            {
                get { return this.locals.Count; }
            }

            /// <summary>
            /// Determines whether the <see cref="KeysCollection"/> contains a specific 
            /// value.</summary>
            /// <param name="item">
            /// The object to locate in the <see cref="KeysCollection"/>.</param>
            /// <returns>
            /// true if <paramref name="item"/> is found in the <see cref="KeysCollection"/>;
            /// otherwise, false.
            /// </returns>
            public virtual bool Contains(TKey item)
            {
                lock (this.locals.syncObject)
                    return this.locals.orderings.ContainsKey(item);
            }

            /// <summary>
            /// Copies the elements of the <see cref="KeysCollection"/> to an
            /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
            /// index.
            /// </summary>
            /// <param name="array">
            /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
            /// elements copied from <see cref="KeysCollection"/>. The 
            /// <see cref="System.Array"/> must
            /// have zero-based indexing.</param>
            /// <param name="arrayIndex">
            /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// <paramref name="arrayIndex"/> is less than 0.</exception>
            /// <exception cref="System.ArgumentNullException">
            /// <paramref name="array"/> is null.</exception>
            /// <exception cref="System.ArgumentException">
            /// <paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> 
            /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
            /// number of elements in the source <see cref="KeysCollection"/> is greater 
            /// than the available space from <paramref name="arrayIndex"/> to the 
            /// end of the destination <paramref name="array"/>.-or-Type <typeparamref name="TKey"/> 
            /// cannot be cast automatically to the type of the destination
            /// <paramref name="array"/>.</exception>
            public virtual void CopyTo(TKey[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                lock (this.locals.syncObject)
                    this.locals.orderings.Keys.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Returns the element at the index provided
            /// </summary>
            /// <param name="index">The index of the element to get.</param>
            /// <returns>The instance of <typeparamref name="TKey"/> at the index provided.</returns>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// <paramref name="index"/> is  beyond the range of the 
            /// <see cref="KeysCollection"/>.
            /// </exception>
            public TKey this[int index]
            {
                get
                {
                    return OnGetKey(index);
                }
                internal protected set
                {
                    OnSetKey(index, value);
                }
            }

            /// <summary>
            /// Obtains the <typeparamref name="TKey"/> at the
            /// <paramref name="index"/> provided.
            /// </summary>
            /// <param name="index">The <see cref="Int32"/> value which denotes
            /// the ordinal index of the <typeparamref name="TKey"/> to retrieve.</param>
            /// <returns>The <typeparamref name="TKey"/> at the <paramref name="index"/>
            /// provided.</returns>
            /// <exception cref="System.ArgumentOutOfRangeException">thrown when
            /// the <paramref name="index"/> is less than zero, or greater than or equal
            /// to the number of items within the <see cref="KeysCollection"/>.</exception>
            protected virtual TKey OnGetKey(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                return this.locals.entries[index].Key;
            }

            /// <summary>
            /// Sets the value of a key within the <see cref="KeysCollection"/>.
            /// </summary>
            /// <param name="index">The <see cref="Int32"/> value which denotes the
            /// ordinal index of the <paramref name="key"/> to change.</param>
            /// <param name="key">The <typeparamref name="TKey"/> that denotes the
            /// new value of the key at the <paramref name="index"/> provided.</param>
            /// <exception cref="System.ArgumentOutOfRangeException">thrown when
            /// the <paramref name="index"/> is less than zero, or greater than or equal
            /// to the number of items within the <see cref="KeysCollection"/>.</exception>
            protected internal virtual void OnSetKey(int index, TKey key)
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                lock (this.locals.syncObject)
                {
                    var currentElement = this.locals.entries[index];
                    if (currentElement.Key.Equals(key))
                        return;
                    if (this.locals.orderings.ContainsKey(key))
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.DuplicateKeyExists);
                    this.locals.orderings.Remove(currentElement.Key);
                    this.locals.orderings.Add(key, index);
                    this.locals.entries[index] = new KeyValuePair<TKey, TValue>(key, currentElement.Value);
                }
            }

            /// <summary>
            /// Translates the <see cref="KeysCollection"/> into a flat <see cref="System.Array"/>
            /// of <typeparamref name="TKey"/> elements.
            /// </summary>
            /// <returns>A new <see cref="System.Array"/> of <typeparamref name="TKey"/> instances.</returns>
            public virtual TKey[] ToArray()
            {
                TKey[] result;
                lock (this.locals.syncObject)
                {
                    result = new TKey[this.Count];
                    this.locals.orderings.Keys.CopyTo(result, 0);
                }
                return result;
            }

            /// <summary>
            /// Returns the <see cref="Int32"/> ordinal index of the 
            /// <paramref name="key"/> provided.
            /// </summary>
            /// <param name="key">The <typeparamref name="TKey"/>
            /// instance to find within the <see cref="KeysCollection"/>.</param>
            /// <returns>-1 if the <paramref name="key"/> was not found within
            /// the <see cref="KeysCollection"/>; a positive <see cref="Int32"/>
            /// value indicating the ordinal index of <paramref name="key"/>
            /// otherwise.</returns>
            public virtual int IndexOf(TKey key)
            {
                int index;
                lock (this.locals.syncObject)
                    if (this.locals.orderings.TryGetValue(key, out index))
                        return index;
                return -1;
            }
            #endregion

            #region IEnumerable<TKey> Members

            public virtual IEnumerator<TKey> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                    yield return this.locals.entries[i].Key;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IControlledCollection Members


            bool IControlledCollection.Contains(object item)
            {
                if (item == null)
                    return false;
                if (!(item is TKey))
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.item, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.item), item.GetType().ToString(), typeof(TKey).GetType().ToString());
                return this.Contains((TKey)item);
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex)
            {
                GeneralCopyTo(array, arrayIndex);
            }

            protected virtual void GeneralCopyTo(Array array, int arrayIndex)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                lock (this.locals.syncObject)
                    for (int i = 0; i < this.Count; i++)
                        array.SetValue(this.locals.entries[i].Key, i + arrayIndex);
            }

            object IControlledCollection.this[int index]
            {
                get { return this[index]; }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is TKey)
                    return this.IndexOf((TKey)element);
                return -1;
            }
            #endregion

            #region ICollection Members


            public bool IsSynchronized
            {
                get { return true; }
            }

            public object SyncRoot
            {
                get { return this.locals.syncObject; }
            }

            #endregion

        }

    }
}
