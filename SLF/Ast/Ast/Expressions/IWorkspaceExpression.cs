using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IWorkspaceExpression :
        IControlledCollection<IStatement>,
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/> associated 
        /// to the workspace.
        /// </summary>
        IExpression Selection { get; }
        /// <summary>
        /// Returns the <see cref="WorkspaceInclusion"/>
        /// which denotes how the scope of the type of
        /// the expression under <see cref="Selection"/>
        /// is merged with the statements within the
        /// <see cref="IWorkspaceExpression"/>.
        /// </summary>
        WorkspaceInclusion ScopeInclusion { get; }
    }
}
