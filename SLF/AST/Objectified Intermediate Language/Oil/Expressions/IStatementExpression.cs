using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression which can 
    /// function as a statement.
    /// </summary>
    public interface IStatementExpression :
        IExpression
    {
        /// <summary>
        /// Returns whether the <see cref="IStatementExpression"/> is valid as a statement in its 
        /// current form.
        /// </summary>
        bool ValidAsStatement { get; }
    }
}
