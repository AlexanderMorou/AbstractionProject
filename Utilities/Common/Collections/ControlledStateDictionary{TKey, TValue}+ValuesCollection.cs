using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class ControlledDictionary<TKey, TValue>
    {
        public class ValuesCollection :
            IControlledCollection<TValue>,
            IControlledCollection
        {
            private SharedLocals locals;
            private ValuesCollection(SharedLocals locals)
            {
                this.locals = locals;
            }

            protected internal ValuesCollection(ControlledDictionary<TKey, TValue> localOwner)
                : this(localOwner.locals)
            {
            }
            #region IControlledCollection<TValue> Members

            /// <summary>
            /// Returns the <see cref="Int32"/> ordinal index of the 
            /// <paramref name="element"/> provided.
            /// </summary>
            /// <param name="element">The <typeparamref name="TValue"/>
            /// instance to find within the <see cref="ValuesCollection"/>.</param>
            /// <returns>-1 if the <paramref name="element"/> was not found within
            /// the <see cref="ValuesCollection"/>; a positive <see cref="Int32"/>
            /// value indicating the ordinal index of <paramref name="element"/>
            /// otherwise.</returns>
            public virtual int IndexOf(TValue element)
            {
                for (int i = 0; i < this.Count; i++)
                    if (object.ReferenceEquals(element, this.locals.entries[i].Value))
                        return i;
                return -1;
            }

            /// <summary>
            /// Gets the number of elements contained in the <see cref="ValuesCollection"/>.</summary>
            /// <returns>
            /// The number of elements contained in the <see cref="ValuesCollection"/>.</returns>
            public virtual int Count
            {
                get { return this.locals.Count; }
            }

            /// <summary>
            /// Determines whether the <see cref="ValuesCollection"/> contains a specific 
            /// value.</summary>
            /// <param name="item">
            /// The object to locate in the <see cref="ValuesCollection"/>.</param>
            /// <returns>
            /// true if <paramref name="item"/> is found in the <see cref="ValuesCollection"/>;
            /// otherwise, false.
            /// </returns>
            public virtual bool Contains(TValue item)
            {
                for (int i = 0; i < this.Count; i++)
                    if (object.ReferenceEquals(item, this.locals.entries[i].Value))
                        return true;
                return false;
            }

            /// <summary>
            /// Copies the elements of the <see cref="ValuesCollection"/> to an
            /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
            /// index.
            /// </summary>
            /// <param name="array">
            /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
            /// elements copied from <see cref="ValuesCollection"/>. The 
            /// <see cref="System.Array"/> must have zero-based indexing.</param>
            /// <param name="arrayIndex">
            /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// <paramref name="arrayIndex"/> is less than 0.</exception>
            /// <exception cref="System.ArgumentNullException">
            /// <paramref name="array"/> is null.</exception>
            /// <exception cref="System.ArgumentException">
            /// <paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> 
            /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
            /// number of elements in the source <see cref="ValuesCollection"/> is greater 
            /// than the available space from <paramref name="arrayIndex"/> to the 
            /// end of the destination <paramref name="array"/>.</exception>
            public virtual void CopyTo(TValue[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                lock (this.locals.syncObject)
                    for (int i = 0; i < this.Count; i++)
                        array[arrayIndex + i] = this.locals.entries[i].Value;
            }

            /// <summary>
            /// Returns the element at the index provided
            /// </summary>
            /// <param name="index">The index of the element to get.</param>
            /// <returns>The instance of <typeparamref name="TValue"/> at the index provided.</returns>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// <paramref name="index"/> is  beyond the range of the 
            /// <see cref="ValuesCollection"/>.
            /// </exception>
            public TValue this[int index]
            {
                get
                {
                    return OnGetValue(index);
                }
                internal protected set
                {
                    OnSetValue(index, value);
                }
            }

            protected internal virtual void OnSetValue(int index, TValue value)
            {
                this.locals.entries[index] = new KeyValuePair<TKey, TValue>(this.locals.entries[index].Key, value);
            }

            protected virtual TValue OnGetValue(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.locals.entries[index].Value;
            }

            /// <summary>
            /// Translates the <see cref="ValuesCollection"/> into a flat <see cref="System.Array"/>
            /// of <typeparamref name="TValue"/> elements.
            /// </summary>
            /// <returns>A new <see cref="System.Array"/> of <typeparamref name="TValue"/> instances.</returns>
            public virtual TValue[] ToArray()
            {
                TValue[] result;
                lock (this.locals.syncObject)
                {
                    result = new TValue[this.Count];
                    Parallel.For(0, this.Count, i =>
                        result[i] = this.locals.entries[i].Value);
                }
                return result;
            }

            #endregion

            #region IEnumerable<TValue> Members
            /// <summary>
            /// Obtains an enumerator which steps through the elements
            /// contained within the <see cref="ValuesCollection"/>.
            /// </summary>
            /// <returns>A <see cref="IEnumerator{T}"/> which steps 
            /// through the elements contained within the <see cref="ValuesCollection"/>.</returns>
            public virtual IEnumerator<TValue> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                    yield return this.locals.entries[i].Value;
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
                if (!(item is TValue))
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.item, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.item), item.GetType().ToString(), typeof(TValue).GetType().ToString());
                return this.Contains((TValue)item);
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
                        array.SetValue(this.locals.entries[i].Value, i + arrayIndex);
            }

            object IControlledCollection.this[int index]
            {
                get { return this[index]; }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is TValue)
                    return this.IndexOf((TValue)element);
                return -1;
            }
            #endregion

            #region ICollection Members

            /// <summary>
            /// Returns whether the <see cref="ValuesCollection"/> is
            /// synchronized.
            /// </summary>
            /// <remarks>Returns true.</remarks>
            public bool IsSynchronized
            {
                get { return true; }
            }

            /// <summary>
            /// Obtains the <see cref="Object"/> which represents the
            /// synchronization object to the <see cref="ValuesCollection"/>.
            /// </summary>
            /// <remarks>Internally locked where needed.</remarks>
            public object SyncRoot
            {
                get { return this.locals.syncObject; }
            }

            #endregion

        }
    }
}
