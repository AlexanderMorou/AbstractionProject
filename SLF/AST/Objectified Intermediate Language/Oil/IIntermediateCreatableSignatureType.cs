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
    /// Defines generic properties and methods for working with a creatable (instantiable) type.
    /// </summary>
    /// <typeparam name="TCtor">The type of constructor member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of constructor member in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TType">The type of creatable type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of creatable type in the intermediate abstract syntax
    /// tree.</typeparam>
    public interface IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateSignatureParent<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateCreatableSignatureType,
        ICreatableType<TCtor, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="IIntermediateCreatableSignatureType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.
        /// </summary>
        new IIntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> Constructors { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a creatable (instantiable) type.
    /// </summary>
    public interface IIntermediateCreatableSignatureType :
        IIntermediateSignatureParent,
        IIntermediateType,
        ICreatableType
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="IIntermediateCreatableSignatureType"/>.
        /// </summary>
        new IIntermediateConstructorSignatureMemberDictionary Constructors { get; }
    }
}
