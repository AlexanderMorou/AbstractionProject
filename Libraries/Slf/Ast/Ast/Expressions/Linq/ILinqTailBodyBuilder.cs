using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for building a language integrated query
    /// expression's tail element.
    /// </summary>
    public interface ILinqTailBodyBuilder
    {
        /// <summary>
        /// Places the results of the query into a range variable for
        /// expression continuation.
        /// </summary>
        /// <param name="intoRangeName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the current query results into.</param>
        /// <returns>A new <see cref="ILinqFusionBodyBuilder"/>
        /// which denotes a fusion between two expression 
        /// body boundaries.</returns>
        ILinqFusionBodyBuilder Into(string intoRangeName);
        /// <summary>
        /// Builds the <see cref="ILinqBody"/> from the chain of
        /// clauses and fusions.
        /// </summary>
        /// <returns>A new <see cref="ILinqBody"/> instance.</returns>
        ILinqExpression Build();
    }
}
