using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// language integrated query which orders the output
    /// data set by the keys provided.
    /// </summary>
    public interface ILinqOrderByGroupClause :
        ILinqClause
    {
        /// <summary>
        /// Returns the <see cref="IExpressionCollection"/> which
        /// denotes the data-sources to obtain the keys for ordering
        /// the output data set.
        /// </summary>
        IExpressionCollection OrderingKeys { get; }
    }
}
