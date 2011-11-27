using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with the parameters of
    /// a lambda expression where the types of the parameters is inferred.
    /// </summary>
    public interface ILambdaTypeInferredExpressionParameterMemberDictionary :
        IIntermediateMemberDictionary<ILambdaTypeInferredExpression, ILambdaTypeInferredExpression, IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember, ILambdaTypeInferredExpressionParameterMember>
    {

    }
}
