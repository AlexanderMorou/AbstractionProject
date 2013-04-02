using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface ISimpleStatement :
        IStatement<CodeExpressionStatement>
    {
        /// <summary>
        /// Returns/sets the simple statement expression that defines the <see cref="ISimpleStatement"/>.
        /// </summary>
        ISimpleStatementExpression Expression { get; }
    }
}
