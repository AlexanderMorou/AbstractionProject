using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for a parameter direction expression.
    /// </summary>
    public interface IDirectionExpression :
        IExpression
    {
        /// <summary>
        /// Returns/sets the means to which a parameter passed to a method call
        /// is coerced.
        /// </summary>
        ParameterDirection Direction { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> 
        /// to be directed.
        /// </summary>
        IExpression Directed { get; set; }
    }
}
