using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface IThrowStatement :
        IStatement
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/> which denotes information
        /// about the exception.
        /// </summary>
        IExpression ExceptionExpression { get; }
    }
}
