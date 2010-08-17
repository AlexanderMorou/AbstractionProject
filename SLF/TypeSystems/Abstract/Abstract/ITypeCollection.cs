using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a type-reference list.
    /// </summary>
    public interface ITypeCollection :
        ITypeCollectionBase
    {
        /// <summary>
        /// Inserts and returns the <see cref="IType"/> instances translated from <paramref name="types"/>.
        /// </summary>
        /// <param name="types">The <see cref="IType"/> array to insert as a series.</param>
        void AddRange(IType[] types);
        /// <summary>
        /// Inserts and returns the <see cref="IType"/> instances translated from <paramref name="types"/>.
        /// </summary>
        /// <param name="types">The <see cref="IType"/> series to insert.</param>
        void AddRange(IEnumerable<IType> types);

        /// <summary>
        /// Gets or sets the element at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="IType"/> to 
        /// get or set.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the 
        /// <see cref="ITypeCollection"/>.</exception>
        new IType this[int index] { get; set; }

        /// <summary>
        /// Inserts an <paramref name="item"/> into the 
        /// <see cref="ITypeCollection"/> at the specified 
        /// <paramref name="index"/>.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which <see cref="IType"/> 
        /// should be inserted.</param>
        /// <param name="item">The <see cref="IType"/> to insert into the 
        /// <see cref="ITypeCollection"/>.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in 
        /// the <see cref="ITypeCollection"/>.</exception>
        void Insert(int index, IType item);
        /// <summary>
        /// Removes the <see cref="IType"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the 
        /// <see cref="IType"/> to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// index is not a valid index in the <see cref="ITypeCollection"/>.
        /// </exception>
        void RemoveAt(int index);

        /// <summary>
        /// Adds an <see cref="IType"/> to the <see cref="ITypeCollection"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the <see cref="ITypeCollection"/>.
        /// </param>
        void Add(IType item);

        /// <summary>
        /// Removes the first occurrence of the specified <see cref="IType"/>
        /// <paramref name="item"/> from the <see cref="ITypeCollection"/>.
        /// </summary>
        /// <param name="item">
        /// The <see cref="IType"/> to remove from the 
        /// <see cref="ITypeCollection"/>.</param>
        /// <returns>true if <paramref name="item"/> was successfully 
        /// removed from the <see cref="ITypeCollection"/>;
        /// otherwise, false; also false if the <paramref name="item"/> 
        /// is not found in the <see cref="ITypeCollection"/>.
        /// </returns>
        bool Remove(IType item);

        /// <summary>
        /// Removes all types from the <see cref="ITypeCollection"/>.
        /// </summary>
        void Clear();

    }
}
