using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with a simple lambda
    /// expression where the types of the parameters are inferred.
    /// </summary>
    public interface ILambdaTypeInferredSimpleExpression :
        ILambdaTypeInferredExpression,
        ILambdaSimpleExpression
    {
    }
}
