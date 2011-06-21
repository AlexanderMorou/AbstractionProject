using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IWorkspaceExpression :
        IControlledStateCollection<IStatement>,
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
