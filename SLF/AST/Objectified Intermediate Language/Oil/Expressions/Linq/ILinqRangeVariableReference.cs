using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    public interface ILinqRangeVariableReference :
        IMemberReferenceExpression
    {
        /// <summary>
        /// Returns the <see cref="ILinqRangeVariable"/> which the
        /// <see cref="ILinqRangeVariableReference"/> refers to.
        /// </summary>
        ILinqRangeVariable Target { get; }
    }
}
