using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface IWrappingExpression :
        IExpression
    {
        /// <summary>
        /// Returns the expression the <see cref="IWrappingExpression"/> represents.
        /// </summary>
        IExpression Reference { get; }
    }
}
