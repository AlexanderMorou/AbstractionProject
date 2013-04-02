using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface ISwitchStatement :
        IStatement<CodeSnippetStatement>
    {
        IExpression CaseSwitch { get; set; }
        ISwitchStatementCases Cases { get; }
    }
}
