using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface IBreakTargetStatement :
        IBlockedStatement
    {
        /// <summary>
        /// Returns/sets whether the <see cref="IBreakTargetStatement"/> includes 
        /// </summary>
        bool UtilizeBreakMeasures { get; set; }
        /// <summary>
        /// Returns the exit label defined for break points.
        /// </summary>
        IBreakTargetExitPoint ExitLabel { get; }
        IStatementBlockLocalMember BreakLocal { get; }

    }
}
