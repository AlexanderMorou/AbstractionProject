using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a condition block.
    /// </summary>
    public interface IConditionBlock :
        IBlockedStatement<CodeConditionStatement>
    {
        /// <summary>
        /// Returns/sets the condition for the <see cref="IConditionBlock"/>.
        /// </summary>
        IExpression Condition { get; set; }
    }
}
