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
    /// Defines properties and methods for working with the statement block
    /// body of a lambda expression.
    /// </summary>
    public interface ILambdaBodyStatement :
        IBlockStatement,
        ILambdaBody
    {
        /// <summary>
        /// Returns the <see cref="ILambdaStatementExpression"/> which
        /// parents the current <see cref="ILambdaBodyStatement"/>.
        /// </summary>
        new ILambdaStatementExpression Parent { get; }
    }
}
