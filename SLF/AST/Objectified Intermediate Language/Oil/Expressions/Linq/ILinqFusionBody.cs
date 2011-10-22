using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with the body of a
    /// language integrated query expression which has been fused
    /// with another <see cref="ILinqBody"/> that continues the 
    /// query expression.
    /// </summary>
    public interface ILinqFusionBody :
        ILinqBody,
        ILinqClause
    {
        /// <summary>
        /// Returns the <see cref="ILinqRangeVariable"/> representing the
        /// local the current query data is stored into for use in the
        /// <see cref="Next"/> <see cref="ILinqBody"/>.
        /// </summary>
        ILinqRangeVariable Target { get; }

        /// <summary>
        /// Returns/sets the <see cref="ILinqBody"/> which continues the query.
        /// </summary>
        ILinqBody Next { get; set; }
    }

}
