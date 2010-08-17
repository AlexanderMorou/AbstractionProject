using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
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
        partial class GroupedTypeDictionary<TType>
            where TType :
                class,
                IType<TType>
        {
            /// <summary>
            /// Provides a values collection for a series of grouped types.
            /// </summary>
            protected internal new class ValuesCollection :
                SubordinateDictionary<string, TType, IType>.ValuesCollection
            {
                private GroupedTypeDictionary<TType> owner;

                /// <summary>
                /// Creates a new <see cref="ValuesCollection"/> associated
                /// to the <paramref name="owner"/> provided.
                /// </summary>
                /// <param name="owner">The <see cref="GroupedTypeDictionary{TType}"/>
                /// which owns the current <see cref="ValuesCollection"/>.</param>
                protected internal ValuesCollection(GroupedTypeDictionary<TType> owner)
                    : base(null)
                {
                    this.owner = owner;
                }

                /// <summary>
                /// Returns the number of elements within the 
                /// <see cref="ValuesCollection"/>.
                /// </summary>
                public override int Count
                {
                    get
                    {
                        return this.owner.groups.Count;
                    }
                }

                /// <summary>
                /// Returns the <see cref="IEnumerator{T}"/> for the
                /// <see cref="TType"/> series associated
                /// to the <see cref="ValuesCollection"/>.
                /// </summary>
                /// <returns>An <see cref="IEnumerator{T}"/> for the
                /// <typeparamref name="TType"/> series associated
                /// to the <see cref="ValuesCollection"/>.</returns>
                public override IEnumerator<TType> GetEnumerator()
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.owner.CheckIndexAt(i, this.owner.groups.Keys.ElementAt(i));
                        yield return this.owner.productions[i];
                    }
                    yield break;
                }

                /// <summary>
                /// Returns the <typeparamref name="TType"/> instance
                /// relative to the <paramref name="index"/> provided.
                /// </summary>
                /// <param name="index">The <see cref="Int32"/>
                /// which denotes which element to retrieve.</param>
                /// <returns>A <typeparamref name="TType"/> instance
                /// relative to the <paramref name="index"/> provided.</returns>
                public override TType this[int index]
                {
                    get
                    {
                        this.owner.CheckIndexAt(index, this.owner.groups.Keys.ElementAt(index));
                        return this.owner.productions[index];
                    }
                }

                /// <summary>
                /// Copies the <typeparamref name="TType"/> instances
                /// from the current <see cref="ValuesCollection"/> to the
                /// <paramref name="array"/> provided.
                /// </summary>
                /// <param name="array">The array of <typeparamref name="TType"/>
                /// instances to receive the values of the current
                /// <see cref="ValuesCollection"/>.</param>
                /// <param name="arrayIndex">The zero-based index to start copying
                /// the values into the <paramref name="array"/>.</param>
                public override void CopyTo(TType[] array, int arrayIndex)
                {
                    if (arrayIndex < 0  || 
                        this.Count + arrayIndex > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.owner.CheckIndexAt(i, this.owner.groups.Keys.ElementAt(i));
                        array[arrayIndex + i] = this.owner.productions[i];
                    }
                }

                /// <summary>
                /// Determines whether the <paramref name="item"/> exists
                /// within the <see cref="ValuesCollection"/>.
                /// </summary>
                /// <param name="item">The <typeparamref name="TType"/>
                /// instance to check the existance of within the 
                /// <see cref="ValuesCollection"/>.</param>
                /// <returns>true if the <paramref name="item"/>
                /// exists within the <see cref="ValuesCollection"/>;
                /// false, otherwise.</returns>
                public override bool Contains(TType item)
                {
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.productions[i] == item)
                            return true;
                    foreach (var name in this.owner.groups.Keys)
                        if (this.owner.groups[name].Contains(item))
                            return true;
                    return false;
                }

                /// <summary>
                /// Flattens the <see cref="ValuesCollection"/> into an
                /// array of <typeparamref name="TType"/> instances
                /// and returns the result.
                /// </summary>
                /// <returns>An array of <typeparamref name="TType"/>
                /// instances.</returns>
                public override TType[] ToArray()
                {
                    TType[] result = new TType[this.Count];
                    this.CopyTo(result, 0);
                    return result;
                }
            }
        }
    }
}
