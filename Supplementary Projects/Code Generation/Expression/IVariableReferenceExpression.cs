using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using System.ComponentModel;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    /// <summary>
    /// Defines properties and methods for working with a reference expression that points back to 
    /// a variable.
    /// </summary>
    public interface IVariableReferenceExpression :
        IExpression<CodeVariableReferenceExpression>,
        IMemberParentExpression<CodeVariableReferenceExpression>,
        IMemberReferenceExpression,
        IAssignStatementTarget
    {
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        new IMemberReferenceComment GetReferenceParticle();

    }
}
