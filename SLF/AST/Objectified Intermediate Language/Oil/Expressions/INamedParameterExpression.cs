using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a named parameter
    /// expression.
    /// </summary>
    public interface INamedParameterExpression :
        IExpression
    {
        /// <summary>
        /// Returns/sets the name of the parameter the <see cref="INamedParameterExpression"/>
        /// refers to.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Returns/sets the name of the <see cref="IExpression"/> the
        /// <see cref="INamedParameterExpression"/> refers to.
        /// </summary>
        IExpression Expression { get; set; }
    }
}
