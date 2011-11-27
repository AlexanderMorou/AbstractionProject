using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement that's simply an expression.
    /// </summary>
    public interface IExpressionStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the expression represented by the <see cref="IExpressionStatement"/>.
        /// </summary>
        IStatementExpression Expression { get; set; }
    }
}
