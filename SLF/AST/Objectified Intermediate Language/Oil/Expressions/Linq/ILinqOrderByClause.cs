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
    /// Defines properties and methods for working with
    /// a language integrated query order-by clause with
    /// default (<seealso cref="LinqOrderByDirection.Ascending"/>)
    /// directional semantics.
    /// </summary>
    public interface ILinqOrderByClause :
        ILinqClause
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which
        /// determines the ordering key for comparison to order the
        /// data series.
        /// </summary>
        IExpression OrderKey { get; set; }
    }
}
