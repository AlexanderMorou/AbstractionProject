using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Statements;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an 
    /// expression which modifies another expression through
    /// a series of statements.
    /// </summary>
    /// <remarks>Typical implementations would involve
    /// creation of a temporary variable to hold the
    /// wrapped expression for manipulation.</remarks>
    public interface IWorkspaceExpression :
        ITopBlockStatement,
        IExpression
    {
    }
}
