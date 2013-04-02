using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface ICreateNewObjectExpression :
        IMemberParentExpression<CodeObjectCreateExpression>
    {
        /// <summary>
        /// Returns/sets the reference of the type to create.
        /// </summary>
        ITypeReference NewType { get; set; }
        /// <summary>
        /// Returns the arguments used to create the object.
        /// </summary>
        IExpressionCollection Arguments { get; }
    }
}
