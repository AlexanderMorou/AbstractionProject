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
    /// Defines properties and methods for working with a directed
    /// order by clause which has multiple ordering keys and
    /// directions.
    /// </summary>
    public interface ILinqDirectedOrderByGroupClause :
        ILinqClause
    {
        /// <summary>
        /// Returns the <see cref="LinqOrderingPair"/> collection
        /// that directs the output data set.
        /// </summary>
        ILinqOrderingPairCollection Orderings { get; }
    }

}
