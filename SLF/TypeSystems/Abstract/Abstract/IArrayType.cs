using AllenCopeland.Abstraction.Slf.Compilers;
using System.Collections.Generic;
using System;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /* *
     * There is no abstract implementation of the array type due to its reliance
     * on the primary typing system that will be used in conjunction with the
     * abstract type system.
     * *
     * An example is the CLI type system, its arrays implement interfaces based upon
     * the number of dimensions the array holds.
     * */
    /// <summary>
    /// Defines properties and methods for working with an array type of
    /// a given <see cref="IType.ElementType"/>.
    /// </summary>
    public interface IArrayType :
        IType
    {
        /// <summary>
        /// Returns the array rank of the <see cref="IArrayType"/>.
        /// </summary>
        int ArrayRank { get; }
        /// <summary>
        /// Returns the <see cref="System.Int32"/> series
        /// representing the lower bound values for the 
        /// <see cref="IArrayType"/>.
        /// </summary>
        IControlledCollection<int> LowerBounds { get; }
        /// <summary>
        /// Returns the <see cref="System.Int32"/> series representing
        /// the lengths of the dimensions of the array.
        /// </summary>
        IControlledCollection<uint> Lengths { get; }
        /// <summary>
        /// Returns whether every dimension has a zero-based index.
        /// </summary>
        bool IsZeroBased { get; }
        /// <summary>
        /// Returns the <see cref="ArrayFlags"/> value which denotes
        /// information about the array's structure.
        /// </summary>
        ArrayFlags Flags { get; }
    }

    /// <summary>
    /// Provides information about an array's structure.
    /// </summary>
    [Flags]
    public enum ArrayFlags { 
        /// <summary>
        /// The array is a single-dimensional array with no
        /// lower-bound or length specified.
        /// </summary>
        Vector = 0,
        /// <summary>
        /// The array is a multi-dimensional array with possible
        /// lengths and lower bounds specified.
        /// </summary>
        Multidimensional = 1,
        /// <summary>
        /// The array is a multi-dimensional array with 
        /// lower-bound information specified.
        /// </summary>
        DimensionLowerBounds = 2,
        /// <summary>
        /// The array is a multi-dimensional array with
        /// length information specified.
        /// </summary>
        DimensionLengths = 4,
    }
}
