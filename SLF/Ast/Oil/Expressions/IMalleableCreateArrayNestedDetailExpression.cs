using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with
    /// the malleable details of a given rank of an array
    /// creation expression.
    /// </summary>
    public interface IMalleableCreateArrayNestedDetailExpression :
        ICreateArrayNestedDetailExpression
    {
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/>
        /// used to instantiate the array.
        /// </summary>
        new IMalleableExpressionCollection Details { get; }
    }
}
