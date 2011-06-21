using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// Defines properties and methods for an assignment expression.
    /// </summary>
    /// <remarks>The <see cref="ICSharpAssignExpression"/> uses
    /// <see cref="BinaryOperationAssociativity.Right"/> recursion.</remarks>
    public interface ICSharpAssignExpression :
        IBinaryOperationExpression<ICSharpConditionalExpression, ICSharpAssignExpression>,
        ICSharpExpression,
        IAssignmentExpression
    {
    }
}
