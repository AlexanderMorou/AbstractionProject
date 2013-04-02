using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface ICastExpression :
        IExpression<CodeCastExpression>,
        IMemberParentExpression<CodeCastExpression>
    {
        IExpression Target { get; set; }
        ITypeReference Type { get; set; }
    }
}
