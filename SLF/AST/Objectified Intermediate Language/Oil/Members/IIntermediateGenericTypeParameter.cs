using System;
using System.Collections.Generic;
using System.Text;
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
    /// Defines generic properties and methods for working with an intermediate generic
    /// type's type-parameter.
    /// </summary>
    /// <typeparam name="TGenericType">The type of generic type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateGenericType">The type of generic type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateGenericTypeParameter<TGenericType, TIntermediateGenericType> :
        IIntermediateGenericParameter<IGenericTypeParameter<TGenericType>, IIntermediateGenericTypeParameter<TGenericType, TIntermediateGenericType>, TGenericType, TIntermediateGenericType>,
        IIntermediateType,
        IGenericTypeParameter<TGenericType>,
        IIntermediateGenericTypeParameter
        where TGenericType :
            IGenericType<TGenericType>
        where TIntermediateGenericType :
            TGenericType,
            IIntermediateGenericType<TGenericType, TIntermediateGenericType>
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
