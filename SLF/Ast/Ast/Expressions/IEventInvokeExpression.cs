using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for invoking an event.
    /// </summary>
    public interface IEventInvokeExpression :
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns the <see cref="IEventReferenceExpression"/> which the 
        /// <see cref="IEventInvokeExpression"/> invokes.
        /// </summary>
        IEventReferenceExpression Reference { get; }
        /// <summary>
        /// Returns the <see cref="IExpressionCollection"/> used
        /// to invoke the <see cref="Reference"/> event.
        /// </summary>
        IExpressionCollection Parameters { get; }
    }
}