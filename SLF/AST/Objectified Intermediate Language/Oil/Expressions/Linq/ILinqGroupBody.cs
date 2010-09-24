using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with the body
    /// of a language integrated query in which the results are grouped
    /// via a key selector.
    /// </summary>
    public interface ILinqGroupBody :
        ILinqSelectBody
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which acts
        /// as the key for grouping the <see cref="ILinqSelectBody.Selection"/>s.
        /// </summary>
        IExpression Key { get; set; }
    }
}
