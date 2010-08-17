using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class AssemblyWorkspace
    {
        /// <summary>
        /// Provides a combined namespace dictionary for an
        /// assembly workspace which unifies the namespace
        /// names for a given depth of the namespace tree.
        /// </summary>
        partial class CombinedNamespaceDictionary
        {
            /// <summary>
            /// A keys collection for a <see cref="CombinedNamespaceDictionary"/>.
            /// </summary>
            public class KeysCollection :
                IControlledStateCollection<string>,
                IControlledStateCollection
            {
                /// <summary>
                /// The owning dictionary which contains the series of keys.
                /// </summary>
                public CombinedNamespaceDictionary owner;

                /// <summary>
                /// Creates a new <see cref="KeysCollection"/>
                /// with the <paramref name="owner"/> provided.
                /// </summary>
                /// <param name="owner">The <see cref="CombinedNamespaceDictionary"/>
                /// which owns the current <see cref="KeysCollection"/>.</param>
                public KeysCollection(CombinedNamespaceDictionary owner)
                {
                    this.owner = owner;
                }

                #region IControlledStateCollection<string> Members

                /// <summary>:
                /// Gets the number of elements contained in the <see cref="KeysCollection"/>.
                /// </summary>
                /// <returns>
                /// The number of elements contained in the <see cref="KeysCollection"/>.
                /// </returns>
                public int Count
                {
                    get { return this.owner.Count; }
                }

                /// <summary>
                /// Determines whether the <see cref="KeysCollection"/> contains a specific 
                /// value.
                /// </summary>
                /// <param name="item">
                /// The object to locate in the <see cref="KeysCollection"/>.</param>
                /// <returns>
                /// true if <paramref name="item"/> is found in the <see cref="KeysCollection"/>;
                /// otherwise, false.
                /// </returns>
                public bool Contains(string item)
                {
                    return this.owner.groups.Keys.Contains(item);
                }

                /// <summary>
                /// Copies the elements of the <see cref="KeysCollection"/> to an
                /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
                /// index.
                /// </summary>
                ///
                /// <param name="array">
                /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
                /// elements copied from <see cref="KeysCollection"/>. The 
                /// <see cref="System.Array"/> must
                /// have zero-based indexing.</param>
                /// <param name="arrayIndex">
                /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
                /// <exception cref="System.ArgumentOutOfRangeException">
                /// <paramref name="arrayIndex"/> is less than 0.-or-<paramref name="arrayIndex"/> 
                /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
                /// number of elements in the source <see cref="KeysCollection"/> is greater 
                /// than the available space from <paramref name="arrayIndex"/> to the 
                /// end of the destination <paramref name="array"/>.</exception>
                /// <exception cref="System.ArgumentNullException">
                /// <paramref name="array"/> is null.</exception>
                /// <exception cref="System.ArgumentException">
                /// <paramref name="array"/> is multidimensional.</exception>
                public void CopyTo(string[] array, int arrayIndex)
                {
                    if (array.Rank != 1)
                        throw new ArgumentException("array must be a vector array.", "array");
                    if (arrayIndex < 0 || arrayIndex + this.Count > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    this.owner.groups.Keys.CopyTo(array, arrayIndex);
                }

                /// <summary>
                /// Returns the element at the <paramref name="index"/> provided.
                /// </summary>
                /// <param name="index">The <paramref name="index"/> of the member to find.</param>
                /// <returns>The instance of <see cref="String"/> at the index provided.</returns>
                /// <exception cref="System.ArgumentOutOfRangeException">
                /// <paramref name="index"/> is  beyond the range of the 
                /// <see cref="KeysCollection"/>.
                /// </exception>
                public string this[int index]
                {
                    get {
                        if (index < 0 || index >= this.Count)
                            throw new ArgumentOutOfRangeException("index");
                        return this.owner.groups.Keys.ElementAt(index); }
                }

                /// <summary>
                /// Translates the <see cref="KeysCollection"/> into a flat <see cref="System.Array"/>
                /// of <see cref="String"/> elements.
                /// </summary>
                /// <returns>A new <see cref="System.Array"/> of <see cref="String"/> instances.</returns>
                public string[] ToArray()
                {
                    return this.owner.groups.Keys.ToArray();
                }

                #endregion

                #region IEnumerable<string> Members

                /// <summary>
                /// Returns an enumerator that iterates through the <see cref="KeysCollection"/>.
                /// </summary>
                /// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through
                /// the <see cref="KeysCollection"/>.</returns>
                public IEnumerator<string> GetEnumerator()
                {
                    return this.owner.groups.Keys.GetEnumerator();
                }

                #endregion

                #region IEnumerable Members

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.GetEnumerator();
                }

                #endregion

                #region IControlledStateCollection Members

                int IControlledStateCollection.Count
                {
                    get { return this.Count; }
                }

                bool IControlledStateCollection.Contains(object item)
                {
                    if (!(item is string))
                        throw new ArgumentException("item must be of type 'System.String'.","item");
                    return this.Contains((string)item);
                }

                void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
                {
                    ((ICollection)this).CopyTo(array, arrayIndex);
                }

                object IControlledStateCollection.this[int index]
                {
                    get {
                        return this[index];
                    }
                }

                #endregion

                #region ICollection Members

                void ICollection.CopyTo(Array array, int index)
                {
                    if (index + this.Count > array.Length)
                        throw new ArgumentOutOfRangeException("index and member count beyond array's length.", "index");
                    int i = index;
                    foreach (var key in this.owner.groups.Keys)
                    {
                        array.SetValue(key, i);
                        i++;
                    }
                }

                int ICollection.Count
                {
                    get { return this.Count; }
                }

                bool ICollection.IsSynchronized
                {
                    get { return false; }
                }

                object ICollection.SyncRoot
                {
                    get { return null; }
                }

                #endregion
            }
        }
    }
}
