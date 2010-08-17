using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a unary operation expression
    /// </summary>
    public interface IUnaryOperationExpression :
        INaryOperandExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IUnaryOperationPrimaryTerm"/> that the 
        /// <see cref="IUnaryOperationExpression"/> operates on.
        /// </summary>
        IUnaryOperationPrimaryTerm Term { get; set; }
    }
}
