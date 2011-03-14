using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines generic properties and methods for working with
    /// an expression that refers to a specific field.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateFieldParent">The type which owns the fields
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IFieldReferenceExpression<TField, TFieldParent> :
        IBoundMemberReference,
        IFieldReferenceExpression
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        /// <summary>
        /// Returns the member associated to the
        /// <see cref="IFieldReferenceExpression"/>.
        /// </summary>
        new TField Member { get; }
    }

    /// <summary>
    /// Defines properties and methods for working 
    /// an expression that refers to a field.
    /// </summary>
    public interface IFieldReferenceExpression :
        IMemberReferenceExpression,
        IMemberParentReferenceExpression,
        IAssignTargetExpression,
        IUnaryOperationPrimaryTerm
    {
        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> 
        /// from which the <see cref="IFieldReferenceExpression"/>
        /// was sourced.
        /// </summary>
        IMemberParentReferenceExpression Source { get; }
    }
}
