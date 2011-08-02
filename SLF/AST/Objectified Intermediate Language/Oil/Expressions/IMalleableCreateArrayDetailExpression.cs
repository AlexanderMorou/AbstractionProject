using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a malleable create array expression.
    /// </summary>
    public interface IMalleableCreateArrayDetailExpression :
        IMalleableCreateArrayExpression,
        IMalleableCreateArrayNestedDetailExpression,
        ICreateArrayDetailExpression
    {
    }
}
