using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface IDefaultValueExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="IType"/> to return the default value of.
        /// </summary>
        ITypeReference DefaultType { get; }
    }
}
