using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with an <see cref="IIntermediateType"/>
    /// which contains generic parameters.
    /// </summary>
    /// <typeparam name="TGenericType">The generic type as it exists 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateGenericType">The type of generic type as it
    /// exists in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericType<TGenericType, TIntermediateGenericType> :
        IIntermediateType,
        IIntermediateGenericParameterParent<IGenericTypeParameter<TGenericType>, IIntermediateGenericTypeParameter<TGenericType, TIntermediateGenericType>, TGenericType, TIntermediateGenericType>,
        IIntermediateGenericType,
        IGenericType<TGenericType>
        where TGenericType :
            IGenericType<TGenericType>
        where TIntermediateGenericType :
            IIntermediateGenericType<TGenericType, TIntermediateGenericType>,
            TGenericType
    {
    }

    /// <summary>
    /// Defines properties and methods for working with an <see cref="IIntermediateType"/>
    /// which contains generic parameters.
    /// </summary>
    public interface IIntermediateGenericType :
        IIntermediateType,
        IIntermediateGenericParameterParent,
        IGenericType
    {
    }
}
