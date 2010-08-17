using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an anonymous
    /// method which ignores parameters.
    /// </summary>
    public interface IAnonymousMethodExpression :
        IBlockStatementParent
    {
    }

    /// <summary>
    /// Defines properties and methods for working with an anonymous
    /// method with parameters.
    /// </summary>
    /// <remarks>
    /// Anonymous methods are essentially just lambdas with explicitly
    /// typed parameters, and a statement block body.
    /// </remarks>
    public interface IAnonymousMethodWithParametersExpression :
        ILambdaTypedStatementExpression,
        IAnonymousMethodExpression
    {
    }
}
