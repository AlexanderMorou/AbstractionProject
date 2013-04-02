using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for a valid target of an <see cref="IAssignStatement"/>.
    /// </summary>
    public interface IAssignStatementTarget :
        IExpression
    {

    }
}
