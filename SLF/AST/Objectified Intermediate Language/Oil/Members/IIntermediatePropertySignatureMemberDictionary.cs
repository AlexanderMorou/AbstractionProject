using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
    public interface IIntermediatePropertySignatureMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IIntermediateGroupedMemberDictionary<TPropertyParent, TIntermediatePropertyParent, TProperty, TIntermediateProperty>,
        IPropertySignatureMemberDictionary<TProperty, TPropertyParent>
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
    /// Defines properties and methods for working with a series of intermediate
    /// property signatures.
    /// </summary>
    public interface IIntermediatePropertySignatureMemberDictionary :
        IPropertySignatureMemberDictionary
    {
        /// <summary>
        /// Adds a new <see cref="IIntermediatePropertySignatureMember"/> instance with
        /// the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <returns>A new <see cref="IIntermediatePropertySignatureMember"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        IIntermediatePropertySignatureMember Add(TypedName nameAndType);
        /// <summary>
        /// Adds a new <see cref="IIntermediatePropertySignatureMember"/> instance with
        /// the <paramref name="nameAndType"/>, <paramref name="canGet"/>, 
        /// and <paramref name="canSet"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <param name="canGet">Whether the property can be read.</param>
        /// <param name="canSet">Whether the property can be written.</param>
        /// <returns>A new <see cref="IIntermediatePropertySignatureMember"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        IIntermediatePropertySignatureMember Add(TypedName nameAndType, bool canGet, bool canSet);
    }
}
