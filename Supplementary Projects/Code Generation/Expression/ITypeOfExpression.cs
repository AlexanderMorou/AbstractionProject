using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface ITypeOfExpression :
        IMemberParentExpression<CodeTypeOfExpression>
    {
        ITypeReference TypeReference
        {
            get;
            set;
        }

    }
}
