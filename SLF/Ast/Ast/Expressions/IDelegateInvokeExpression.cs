using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with
    /// an expression that invokes a delegate.
    /// </summary>
    public interface IDelegateInvokeExpression :
        IMemberParentReferenceExpression,
        IStatementExpression
    {
        /// <summary>
        /// Returns the <see cref="IDelegateReferenceExpression"/>
        /// which denotes the target of the invocation.
        /// </summary>
        IDelegateReferenceExpression Reference { get; }
    }
}
