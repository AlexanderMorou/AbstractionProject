using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a reference
    /// to a range variable defined within a language integrated query
    /// expression.
    /// </summary>
    public interface IRangeVariableReferenceExpression :
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> representing the
        /// range variable's name.
        /// </summary>
        string Name { get; set; }
    }
}
