using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working 
    /// an expression that refers to a field.
    /// </summary>
    public interface IFieldReferenceExpression :
        IMemberReferenceExpression,
        IMemberParentReferenceExpression,
        /*ILinkableExpression,*/
        IAssignTargetExpression,
        IUnaryOperationPrimaryTerm
    {
        /// <summary>
        /// Returns the member associated to the
        /// <see cref="IFieldReferenceExpression"/>.
        /// </summary>
        IFieldMember AssociatedMember { get; }
        /// <summary>
        /// Returns the <see cref="IMemberReferenceExpression"/> 
        /// from which the <see cref="IFieldReferenceExpression"/>
        /// was sourced.
        /// </summary>
        IMemberReferenceExpression Source { get; }
    }
}
