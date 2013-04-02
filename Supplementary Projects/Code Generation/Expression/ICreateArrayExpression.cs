using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface ICreateArrayExpression :
        IExpression<CodeArrayCreateExpression>
    {
        IExpression SizeExpression { get; set; }

        int Size { get; set; }

        IExpressionCollection Initializers { get; }
        ITypeReference ArrayType { get; set; }
    }
}
