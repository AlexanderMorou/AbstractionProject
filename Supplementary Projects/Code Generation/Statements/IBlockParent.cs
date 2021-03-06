using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface IBlockParent :
        IDeclarationTarget
    {
        /// <summary>
        /// Returns the statement block for the <see cref="IBlockParent"/>.
        /// </summary>
        IStatementBlock Statements { get; }
        /// <summary>
        /// Returns a collection of defined label names used for creating new labels.
        /// </summary>
        ICollection<string> DefinedLabelNames { get; }
    }
}
