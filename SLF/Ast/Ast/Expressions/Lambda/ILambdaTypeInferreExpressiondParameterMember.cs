using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with the parameter
    /// of a lambda type inferred expression.
    /// </summary>
    public interface ILambdaTypeInferredExpressionParameterMember :
        IIntermediateMember<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpression, ILambdaTypeInferredExpression>
    {
    }
}
