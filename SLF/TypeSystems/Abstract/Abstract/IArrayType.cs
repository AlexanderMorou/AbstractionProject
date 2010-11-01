using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
        /// <remarks>
        /// Arrays which define a specialized lower bounds will have
        /// associated field, parameter, property, and return type
        /// marked with the <see cref="LowerBoundTargetAttribute"/>.
        /// </remarks>
        int[] LowerBounds { [return: LowerBoundTarget(0)] get; }
        /// <summary>
        /// Returns whether every dimension has a zero-based index.
        /// </summary>
        bool IsZeroBased { get; }
        /// <summary>
        /// Returns whether the <see cref="IArrayType"/>
        /// is a single-dimensional zero-based indexing array.
        /// </summary>
        bool IsVectorArray { get; }
    }
}
