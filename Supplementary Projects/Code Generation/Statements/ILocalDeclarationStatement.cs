using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a local variable declaration.
    /// </summary>
    public interface ILocalDeclarationStatement :
        IStatement<CodeVariableDeclarationStatement>
    {
        /// <summary>
        /// Returns/sets the referenced member the <see cref="ILocalDeclarationStatement"/>
        /// declares.
        /// </summary>
        IStatementBlockLocalMember ReferencedMember { get; }
    }
}
