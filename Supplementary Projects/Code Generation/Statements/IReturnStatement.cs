using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface IReturnStatement :
        IStatement<CodeMethodReturnStatement>
    {
        /// <summary>
        /// Returns/sets the result of the call.
        /// </summary>
        IExpression Result { get; set; }
    }
}
