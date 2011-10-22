using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public interface IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateSignatureParent<IGeneralSignatureMemberUniqueIdentifier, TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateCreatableSignatureParent,
        ICreatableParent<TCtor, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TCtor
        where TType :
            ICreatableParent<TCtor, TType>
        where TIntermediateType :
            IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TType
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="IIntermediateCreatableSignatureParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.
        /// </summary>
        new IIntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> Constructors { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a creatable (instantiable) type.
    /// </summary>
    public interface IIntermediateCreatableSignatureParent :
        IIntermediateSignatureParent,
        ICreatableParent
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="IIntermediateCreatableSignatureParent"/>.
        /// </summary>
        new IIntermediateConstructorSignatureMemberDictionary Constructors { get; }
    }
}
