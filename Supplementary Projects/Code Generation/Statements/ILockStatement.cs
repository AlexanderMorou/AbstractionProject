using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface ILockStatement :
        IBlockedStatement<CodeSnippetStatement>
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/> which is locked by the
        /// <see cref="ILockStatement"/>.
        /// </summary>
        IExpression LockedExpression { get; }
    }
}
