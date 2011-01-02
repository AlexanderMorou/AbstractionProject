using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines generic properties and methods for working with an expression
    /// which represents a reference to a property signature.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IPropertySignatureReferenceExpression<TProperty, TPropertyParent> :
        ITypedMemberReferenceExpression,
        IPropertyReferenceExpression
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParentType<TProperty, TPropertyParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TProperty"/> member to which the 
        /// <see cref="IPropertySignatureReferenceExpression"/> refers.
        /// </summary>
        new TProperty Member { get; }
    }
    /// <summary>
    /// Defines generic properties and methods for working with an expression
    /// which represents a reference to a property.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IPropertyReferenceExpression<TProperty, TPropertyParent> :
        ITypedMemberReferenceExpression,
        IPropertyReferenceExpression
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateProperty"/> member to which the 
        /// <see cref="IPropertyReferenceExpression{TProperty, TPropertyParent}"/> refers.
        /// </summary>
        new TProperty Member { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with an expression
    /// which represents a reference to a property.
    /// </summary>
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
