using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement based about a condition.
    /// </summary>
    public interface IConditionStatement :
        IConditionBlock
    {
        /// <summary>
        /// The <see cref="IStatementBlock"/> associated to the false half of the 
        /// <see cref="IConditionStatement"/>.
        /// </summary>
        IStatementBlock FalseBlock { get; }
    }
}
