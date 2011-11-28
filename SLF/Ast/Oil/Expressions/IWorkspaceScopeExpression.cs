using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// The <see cref="IWorkspaceScopeExpression"/>
    /// functions by explicitly accessing the immediate
    /// <see cref="IMalleableWorkspaceExpression"/> in which it is 
    /// contained.
    /// </summary>
    /// <remarks>
    /// Visual Basic.NET's With statement functionality.
    /// </remarks>
    public interface IWorkspaceScopeExpression :
        IMemberParentReferenceExpression
    {

    }

}
