using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with a lambda with
    /// a statement block body where the types of the parameters are
    /// inferred.
    /// </summary>
    public interface ILambdaTypeInferredStatementExpression :
        ILambdaTypeInferredExpression,
        ILambdaStatementExpression
    {
    }
}
