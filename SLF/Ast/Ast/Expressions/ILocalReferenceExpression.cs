using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with a refernce to a method's local.
    /// </summary>
    public interface ILocalReferenceExpression :
        IMemberParentReferenceExpression,
        IUnaryOperationPrimaryTerm,
        IMemberReferenceExpression,
        IAssignTargetExpression
    {
    }
}
