using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IPropertyReferenceExpression :
        IMemberParentReferenceExpression,
        IMemberReferenceExpression,
        IAssignTargetExpression,
        IUnaryOperationPrimaryTerm,
        IFusionTermExpression
    {
        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IPropertyReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        MethodReferenceType ReferenceType { get; set; }
        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IPropertyReferenceExpression"/>.
        /// </summary>
        IMemberParentReferenceExpression Source { get; }
    }
}
