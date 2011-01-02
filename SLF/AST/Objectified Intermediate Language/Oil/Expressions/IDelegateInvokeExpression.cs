using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IDelegateInvokeExpression :
        IMemberParentReferenceExpression,
        IStatementExpression
    {
        /// <summary>
        /// Returns the 
        /// </summary>
        IDelegateReferenceExpression Reference { get; }
    }
}
