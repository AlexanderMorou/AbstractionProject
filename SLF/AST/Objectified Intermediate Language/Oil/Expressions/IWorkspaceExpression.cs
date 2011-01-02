using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
