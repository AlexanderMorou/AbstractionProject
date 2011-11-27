using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with a typed lambda
    /// expression which contains a statement block body.
    /// </summary>
    public interface ILambdaTypedStatementExpression :
        ILambdaTypedExpression,
        ILambdaStatementExpression
    {
    }
}
