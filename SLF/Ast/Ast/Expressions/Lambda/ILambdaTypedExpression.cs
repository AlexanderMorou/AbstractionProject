using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    public interface ILambdaTypedExpression :
        IIntermediateParameterParent<ILambdaTypedExpression, ILambdaTypedExpression, ILambdaTypedExpressionParameterMember, ILambdaTypedExpressionParameterMember>,
        ILambdaExpression
    {
        /// <summary>
        /// Returns the <see cref="ILambdaSignatureType"/> of the
        /// <see cref="ILambdaExpression"/>.
        /// </summary>
        ILambdaSignatureType Signature { get; }
        /// <summary>
        /// Returns the parameters of the <see cref="ILambdaTypedExpression"/>.
        /// </summary>
        new ILambdaTypedExpressionParameterMemberDictionary Parameters { get; }
    }
}
