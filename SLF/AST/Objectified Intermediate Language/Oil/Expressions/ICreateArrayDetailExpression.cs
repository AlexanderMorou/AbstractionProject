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

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an array creation
    /// expression that details the contents of the array.
    /// </summary>
    public interface ICreateArrayDetailExpression :
        ICreateArrayExpression
    {
        /// <summary>
        /// Returns the <see cref="IExpressionCollection"/> used
        /// to instantiate the array.
        /// </summary>
        IMalleableExpressionCollection Details { get; }
    }
}
