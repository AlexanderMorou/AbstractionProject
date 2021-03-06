using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright � 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines properties and methods for working with a collection which is tightly controlled.
    /// </summary>
    public interface IControlledCollection :
        ICountableEnumerable
    {
        /// <summary>
        /// Determines whether the <see cref="IControlledCollection"/> contains a specific 
        /// value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="IControlledCollection"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="IControlledCollection"/>;
        /// otherwise, false.
        /// </returns>
        bool Contains(object item);

        /// <summary>
        /// Copies the elements of the <see cref="IControlledCollection"/> to an
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
        /// index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
        /// elements copied from <see cref="IControlledCollection"/>. The 
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
        /// number of elements in the source <see cref="IControlledCollection"/> is greater 
        /// than the available space from <paramref name="arrayIndex"/> to the 
        /// end of the destination <paramref name="array"/>.-or-the underlying <see cref="Type"/>
        /// of the elements of the <see cref="IControlledCollection "/>
        /// cannot be cast automatically to the type of the destination.
        /// <paramref name="array"/>.</exception>
        /// <exception cref="System.ArrayTypeMismatchException">thrown when the type
        /// of the <paramref name="array"/> does not match the underlying type of
        /// the elements of the <see cref="IControlledCollection"/>.</exception>
        void CopyTo(Array array, int arrayIndex = 0);

        /// <summary>
        /// Returns the element at the <paramref name="index"/> provided
        /// </summary>
        /// <param name="index">The index of the member to find.</param>
        /// <returns>The instance of <see cref="System.Object"/> at the <paramref name="index"/>
        /// provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is  beyond the range of the 
        /// <see cref="IControlledCollection"/>.
        /// </exception>
        object this[int index] { get; }

        /// <summary>
        /// Returns the <see cref="Int32"/> ordinal index of the 
        /// <paramref name="element"/> provided.
        /// </summary>
        /// <param name="element">The <see cref="Object"/>
        /// instance to find within the <see cref="IControlledCollection"/>.</param>
        /// <returns>-1 if the <paramref name="element"/> was not found within
        /// the <see cref="IControlledCollection"/>; a positive <see cref="Int32"/>
        /// value indicating the ordinal index of <paramref name="element"/>
        /// otherwise.</returns>
        int IndexOf(object element);
    }
}
