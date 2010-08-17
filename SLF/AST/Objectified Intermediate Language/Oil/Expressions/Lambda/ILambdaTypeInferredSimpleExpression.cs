using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
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
