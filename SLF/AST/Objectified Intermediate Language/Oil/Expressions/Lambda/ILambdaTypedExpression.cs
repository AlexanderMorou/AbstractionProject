using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
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
