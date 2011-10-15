using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of
    /// intermediate property signatures.
    /// </summary>
    /// <typeparam name="TProperty">The type of property signature used in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property signature used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which acts as the parent of the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which acts as the parent of the 
    /// properties in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IIntermediateGroupedMemberDictionary<TPropertyParent, TIntermediatePropertyParent, IGeneralMemberUniqueIdentifier, TProperty, TIntermediateProperty>,
        IPropertyMemberDictionary<TProperty, TPropertyParent>
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
        /// Adds a new <typeparamref name="TIntermediateProperty"/> instance with
        /// the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <returns>A new <typeparamref name="TIntermediateProperty"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        TIntermediateProperty Add(TypedName nameAndType);
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateProperty"/> instance with
        /// the <paramref name="nameAndType"/>, <paramref name="canGet"/>, 
        /// and <paramref name="canSet"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <param name="canGet">Whether the property can be read.</param>
        /// <param name="canSet">Whether the property can be written.</param>
        /// <returns>A new <typeparamref name="TIntermediateProperty"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        TIntermediateProperty Add(TypedName nameAndType, bool canGet, bool canSet);
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// series of property members.
    /// </summary>
    public interface IIntermediatePropertyMemberDictionary :
        IIntermediatePropertySignatureMemberDictionary,
        IPropertyMemberDictionary
    {
        /// <summary>
        /// Adds a new <see cref="IIntermediatePropertyMember"/> instance with
        /// the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <returns>A new <see cref="IIntermediatePropertyMember"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        new IIntermediatePropertyMember Add(TypedName nameAndType);
        /// <summary>
        /// Adds a new <see cref="IIntermediatePropertyMember"/> instance with
        /// the <paramref name="nameAndType"/>, <paramref name="canGet"/>, 
        /// and <paramref name="canSet"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <param name="canGet">Whether the property can be read.</param>
        /// <param name="canSet">Whether the property can be written.</param>
        /// <returns>A new <see cref="IIntermediatePropertyMember"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        new IIntermediatePropertyMember Add(TypedName nameAndType, bool canGet, bool canSet);
    }
}
