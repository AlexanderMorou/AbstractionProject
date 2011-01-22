using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
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
