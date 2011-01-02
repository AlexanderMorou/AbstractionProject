using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// the body of a lambda expression.
    /// </summary>
    public interface ILambdaBody 
    {
        /// <summary>
        /// Returns the <see cref="ILambdaExpression"/> which
        /// parents the current <see cref="ILambdaBody"/>.
        /// </summary>
        ILambdaExpression Parent { get; }
    }
}
