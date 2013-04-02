using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using System.ComponentModel;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface IIndexerReferenceExpression :
        IMemberParentExpression<CodeIndexerExpression>,
        IMemberReferenceExpression,
        IAssignStatementTarget
    {
        IExpressionCollection Indices { get; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        new IMemberReferenceComment GetReferenceParticle();
    }
}
