using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// parameter members on a typed lambda expression.
    /// </summary>
    public interface ILambdaTypedExpressionParameterMemberDictionary :
        IIntermediateParameterMemberDictionary<ILambdaTypedExpression, ILambdaTypedExpression, ILambdaTypedExpressionParameterMember, ILambdaTypedExpressionParameterMember>
    {
    }
}
