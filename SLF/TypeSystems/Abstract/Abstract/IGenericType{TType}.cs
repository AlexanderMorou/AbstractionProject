﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a <see cref="IType{TType}"/> which
    /// contains generic parameters.
    /// </summary>
    /// <typeparam name="TType">The type of <see cref="IType{TType}"/> in the current
    /// implementation.</typeparam>
    public interface IGenericType<TType> :
        IType<TType>,
        IGenericParamParent<IGenericTypeParameter<TType>, TType>,
        IGenericType
        where TType :
            IGenericType<TType>
    {
        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericType{TType}"/>
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ILockedTypeCollection"/> 
        /// used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericType{TType}"/>'s 
        /// <seealso cref="IGenericType.IsGenericTypeDefinition"/>
        /// is false.</exception>
        new TType MakeGenericType(ITypeCollectionBase typeParameters);
        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericType{TType}"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericType{TType}"/>'s 
        /// <seealso cref="IGenericType.IsGenericTypeDefinition"/>
        /// is false.</exception>
        new TType MakeGenericType(params IType[] typeParameters);
    }
}