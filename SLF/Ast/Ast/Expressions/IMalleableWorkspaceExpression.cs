using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public enum WorkspaceInclusion
    {
        /// <summary>
        /// The active scope of the expression selected by the workspace
        /// is selected implicitly and public instance members become a part of the
        /// active scope.
        /// </summary>
        Implicit,
        /// <summary>
        /// The active scope of the expression selected by the workspace
        /// is selected explicitly through some symbolic indicator.
        /// </summary>
        Explicit,
    }
    /// <summary>
    /// Defines properties and methods for working with an 
    /// expression which modifies another expression through
    /// a series of statements.
    /// </summary>
    /// <remarks>Typical implementations would involve
    /// creation of a temporary variable to hold the
    /// wrapped expression for manipulation.</remarks>
    public interface IMalleableWorkspaceExpression :
        ITopBlockStatement,
        IWorkspaceExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> associated 
        /// to the workspace.
        /// </summary>
        new IExpression Selection { get; set; }
    }
}
