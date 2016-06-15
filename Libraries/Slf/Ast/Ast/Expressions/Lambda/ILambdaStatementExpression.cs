using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with a simple lambda expression
    /// which contains a statement body.
    /// </summary>
    public interface ILambdaStatementExpression :
        ILambdaExpression,
        IBlockStatementParent
    {
        /// <summary>
        /// Returns the <see cref="ILambdaBodyStatement"/> that defines the 
        /// <see cref="ILambdaStatementExpression"/>.
        /// </summary>
        new ILambdaBodyStatement Block { get; }
    }
}
