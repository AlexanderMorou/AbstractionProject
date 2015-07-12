using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate 
    /// type's generic parameter.
    /// </summary>
    /// <typeparam name="TTypeIdentifier">The kind of type identifier used
    /// to differentiate the <typeparamref name="TIntermediateType"/>
    /// instance from its siblings.</typeparam>
    /// <typeparam name="TType">The type of generic type in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of generic type in
    /// the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType> :
        IIntermediateGenericParameter<IGenericTypeParameter<TTypeIdentifier, TType>, IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateType,
        IGenericTypeParameter<TTypeIdentifier, TType>,
        IIntermediateGenericTypeParameter
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            TType,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate generic
    /// type's type-parameter.
    /// </summary>
    public interface IIntermediateGenericTypeParameter :
        IIntermediateGenericParameter,
        IGenericTypeParameter
    {
        /// <summary>
        /// Returns/sets the <see cref="GenericParameterVariance"/>
        /// which relates to whether targets receiving generic variants of the
        /// closed-generic sources can be more or less derived, or require
        /// exact matches.
        /// </summary>
        new GenericParameterVariance Variance { get; set; }
    }
}
