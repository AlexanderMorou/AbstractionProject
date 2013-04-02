using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface IBlockedStatement :
        IStatement,
        IBlockParent,
        IStatementBlockInsertBase
    {
        /// <summary>
        /// Returns the <see cref="IStatementBlock"/> containing the 
        /// statements within the blocked statement.
        /// </summary>
        new IStatementBlock Statements { get; }
    }
}
