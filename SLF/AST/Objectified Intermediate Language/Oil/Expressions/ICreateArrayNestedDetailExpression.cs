using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface ICreateArrayNestedDetailExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="IExpressionCollection"/> used
        /// to instantiate the array.
        /// </summary>
        IMalleableExpressionCollection Details { get; }
    }
}
