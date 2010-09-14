using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
    public interface IPropertySignatureReferenceExpression<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        ITypedMemberReferenceExpression,
        IPropertyReferenceExpression
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertySignatureParentType<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertySignatureParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
    {
        /// <summary>
        /// Returns the <see cref="TIntermediateProperty{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/> member to which the 
        /// <see cref="IPropertySignatureReferenceExpression"/> refers.
        /// </summary>
        new TIntermediateProperty Member { get; }
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
    public interface IPropertyReferenceExpression<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        ITypedMemberReferenceExpression,
        IPropertyReferenceExpression
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertyParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
    {
        /// <summary>
        /// Returns the <see cref="TIntermediateProperty"/> member to which the 
        /// <see cref="IPropertyReferenceExpression{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/> refers.
        /// </summary>
        new TIntermediateProperty Member { get; }
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
