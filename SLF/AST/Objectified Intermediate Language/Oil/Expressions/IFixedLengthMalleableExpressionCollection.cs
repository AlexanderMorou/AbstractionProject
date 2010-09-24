using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a malleable 
    /// series of expressions which is fixed in length.
    /// </summary>
    public interface IFixedLengthMalleableExpressionCollection :
        IExpressionCollection
    {
        /// <summary>
        /// Returns/sets an <see cref="IExpression"/> element
        /// at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based <see cref="Int32"/> value indicating the index
        /// of the element to retrieve/store.</param>
        /// <returns>A <see cref="IExpression"/> instance at the <paramref name="index"/>
        /// specified.</returns>
        new IExpression this[int index] { get; set; }
    }
}
