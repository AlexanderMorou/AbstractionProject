using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        partial class CombinedNamespaceDictionary
        {
            /// <summary>
            /// A values collection for a <see cref="CombinedNamespaceDictionary"/>.
            /// </summary>
            public class ValuesCollection :
                IControlledStateCollection<INamespaceDeclaration>,
                IControlledStateCollection
            {
                /// <summary>
                /// The owning dictionary which contains the series of values.
                /// </summary>
                public CombinedNamespaceDictionary owner;

                /// <summary>
                /// Creates a new <see cref="ValuesCollection"/>
                /// for the <paramref name="owner"/> provided.
                /// </summary>
                /// <param name="owner">The <see cref="CombinedNamespaceDictionary"/>
                /// which owns the current <see cref="ValuesCollection"/>.</param>
                public ValuesCollection(CombinedNamespaceDictionary owner)
                {
                    this.owner = owner;
                }

                #region IControlledStateCollection<INamespaceDeclaration> Members

                /// <summary>:
                /// Gets the number of elements contained in the <see cref="ValuesCollection"/>.
                /// </summary>
                /// <returns>
                /// The number of elements contained in the <see cref="ValuesCollection"/>.
                /// </returns>
                public int Count
                {
                    get { return this.owner.groups.Count; }
                }

                /// <summary>
                /// Determines whether the <see cref="ValuesCollection"/> 
                /// contains a specific value.
                /// </summary>
                /// <param name="item">
                /// The namespace declaration to locate in 
                /// the <see cref="ValuesCollection"/>.</param>
                /// <returns>
                /// true if <paramref name="item"/> is found in 
                /// the <see cref="ValuesCollection"/>; otherwise, 
                /// false.
                /// </returns>
                public bool Contains(INamespaceDeclaration item)
                {
                    var name = item.Name;
                    if (!this.owner.ContainsKey(name))
                        return false;
                    int index = this.owner.groups.Keys.GetIndexOf(name);
                    if (this.owner.productions[index] == null ||
                        this.owner.productions[index] != item)
                        if (this.owner.groups[name].Contains(item))
                            return true;
                    return true;
                }

                /// <summary>
                /// Copies the elements of the <see cref="ValuesCollection"/> to an
                /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
                /// index.
                /// </summary>
                ///
                /// <param name="array">
                /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
                /// elements copied from <see cref="ValuesCollection"/>. The 
                /// <see cref="System.Array"/> must
                /// have zero-based indexing.</param>
                /// <param name="arrayIndex">
                /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
                /// <exception cref="System.ArgumentOutOfRangeException">
                /// <paramref name="arrayIndex"/> is less than 0.-or-<paramref name="arrayIndex"/> 
                /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
                /// number of elements in the source <see cref="ValuesCollection"/> is greater 
                /// than the available space from <paramref name="arrayIndex"/> to the 
                /// end of the destination <paramref name="array"/>.</exception>
                /// <exception cref="System.ArgumentNullException">
                /// <paramref name="array"/> is null.</exception>
                /// <exception cref="System.ArgumentException">
                /// <paramref name="array"/> is multidimensional.</exception>
                public void CopyTo(INamespaceDeclaration[] array, int arrayIndex)
                {
                    if (array == null)
                        throw new ArgumentNullException("array");
                    if (array.Rank != 1)
                        throw new ArgumentException("array must be a vector array", "array");
                    if (arrayIndex + this.Count > array.Length ||
                        arrayIndex < 0)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    this.ToArray().CopyTo(array, arrayIndex);
                }

                /// <summary>
                /// Returns the element at the <paramref name="index"/> provided
                /// </summary>
                /// <param name="index">The index of the member to find.</param>
                /// <returns>The instance of <see cref="INamespaceDeclaration"/> 
                /// at the <paramref name="index"/> provided.</returns>
                /// <exception cref="System.ArgumentOutOfRangeException">
                /// <paramref name="index"/> is  beyond the range of the 
                /// <see cref="ValuesCollection"/>.
                /// </exception>
                public INamespaceDeclaration this[int index]
                {
                    get
                    {
                        if (index < 0 || index >= this.Count)
                            throw new ArgumentOutOfRangeException("index");
                        this.owner.CheckIndexAt(index, this.owner.Keys[index]);
                        return this.owner.productions[index];
                    }
                }

                /// <summary>
                /// Translates the <see cref="ValuesCollection"/> into a 
                /// flat <see cref="System.Array"/>
                /// of <see cref="INamespaceDeclaration"/> elements.
                /// </summary>
                /// <returns>A new <see cref="System.Array"/> of <see cref="INamespaceDeclaration"/>
                /// instances.</returns>
                public INamespaceDeclaration[] ToArray()
                {
                    for (int i = 0; i < this.Count; i++)
                        this.owner.CheckIndexAt(i, this.owner.Keys[i]);
                    INamespaceDeclaration[] result = new INamespaceDeclaration[this.Count];
                    this.owner.productions.CopyTo(result, 0);
                    return result;
                }

                #endregion

                #region IEnumerable<INamespaceDeclaration> Members

                public IEnumerator<INamespaceDeclaration> GetEnumerator()
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.owner.CheckIndexAt(i, this.owner.Keys[i]);
                        yield return this.owner.productions[i];
                    }
                    yield break;
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
                    get {
                        return this.Count;
                    }
                }

                bool IControlledStateCollection.Contains(object item)
                {
                    if (!(item is INamespaceDeclaration))
                        throw new ArgumentException("item must be of type 'AllenCopeland.Abstraction.Slf.Abstract.INamespaceDeclaration'", "item");
                    return this.Contains((INamespaceDeclaration)(item));
                }

                void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
                {
                    ((ICollection)(this)).CopyTo(array, arrayIndex);
                }

                object IControlledStateCollection.this[int index]
                {
                    get { return this[index]; }
                }

                #endregion

                #region ICollection Members

                void ICollection.CopyTo(Array array, int index)
                {
                    this.ToArray().CopyTo(array, index);
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
