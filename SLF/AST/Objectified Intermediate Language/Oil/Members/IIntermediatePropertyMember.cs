using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate property member.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IIntermediateMember<TPropertyParent, TIntermediatePropertyParent>,
        IIntermediatePropertyMember,
        IPropertyMember<TProperty, TPropertyParent>
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
    }
    /// <summary>
    /// Defines properties and methods for working with a property member.
    /// </summary>
    public interface IIntermediatePropertyMember :
        IIntermediatePropertySignatureMember,
        IIntermediateExtendedInstanceMember,
        IIntermediateScopedDeclaration,
        IPropertyMember
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IIntermediatePropertyMember"/>.
        /// </summary>
        new IIntermediatePropertyMethodMember GetMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IIntermediatePropertyMember"/>.
        /// </summary>
        new IIntermediatePropertySetMethodMember SetMethod { get; }
    }
}
