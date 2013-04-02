using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    /// <summary>
    /// Refers to a property of an <see cref="IMemberParentType"/>.
    /// </summary>
    public interface IPropertyReferenceExpression :
        IAssignStatementTarget,
        IExpression<CodePropertyReferenceExpression>,
        IMemberReferenceExpression,
        IMemberParentExpression<CodePropertyReferenceExpression>
    {
        new IPropertyReferenceComment GetReferenceParticle();

    }
}
