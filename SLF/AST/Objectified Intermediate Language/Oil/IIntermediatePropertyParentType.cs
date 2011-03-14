using System;
using System.Collections.Generic;
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

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with the parent of a series of properties.
    /// </summary>
    /// <typeparam name="TProperty">The type of property member used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property member used in the
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type used as the parent of a series of properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type used as the parent of a series of properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediatePropertyParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IPropertyParentType<TProperty, TPropertyParent>,
        IIntermediatePropertyParentType
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
        /// Returns the <see cref="IIntermediatePropertyMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>
        /// contained on the current <see cref="IIntermediatePropertyParentType{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>.
        /// </summary>
        new IIntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> Properties { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with the parent of a series
    /// of intermediate properties.
    /// </summary>
    public interface IIntermediatePropertyParentType :
        IIntermediateMemberParent,
        IIntermediateType,
        IPropertyParentType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMemberDictionary"/> contained on the current <see cref="IIntermediatePropertyParentType"/>
        /// </summary>
        new IIntermediatePropertyMemberDictionary Properties { get; }
    }
}
