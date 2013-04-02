using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
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
    public interface IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IPropertyParent<TProperty, TPropertyParent>,
        IIntermediatePropertyParent
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>
        /// contained on the current <see cref="IIntermediatePropertyParent{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>.
        /// </summary>
        new IIntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> Properties { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with the parent of a series
    /// of intermediate properties.
    /// </summary>
    public interface IIntermediatePropertyParent :
        IIntermediateMemberParent,
        IPropertyParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMemberDictionary"/> contained on the current <see cref="IIntermediatePropertyParent"/>
        /// </summary>
        new IIntermediatePropertyMemberDictionary Properties { get; }
    }
}
