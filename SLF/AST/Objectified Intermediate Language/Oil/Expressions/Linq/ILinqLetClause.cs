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
    /// Defines properties and methods for working with a language integrated
    /// query let clause for assigning a range variable to a given value for
    /// later use in the query.
    /// </summary>
    public interface ILinqLetClause :
        ILinqClause
    {
        /// <summary>
        /// Returns/sets the name of the range variable defined by the
        /// <see cref="ILinqLetClause"/>.
        /// </summary>
        string RangeVariableName { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> to assign to the
        /// range variable.
        /// </summary>
        IExpression RangeSource { get; set; }
    }
}