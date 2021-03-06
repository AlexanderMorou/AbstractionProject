using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a method return statement.
    /// </summary>
    public interface IMethodReturnStatement
    {
        /// <summary>
        /// Returns/sets the expression relative to the <see cref="IMethodReturnStatement"/>.
        /// </summary>
        IExpression ReturnExpression { get; set; }
    }
}
