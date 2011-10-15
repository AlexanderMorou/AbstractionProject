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
    public interface IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        ICreatableParent<TCtor, TType>,
        IIntermediateCreatableParent
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableParent<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="IIntermediateCreatableParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.
        /// </summary>
        new IIntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> Constructors { get; }
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateCtor"/> which 
        /// represents the type-initializer (static constructor)
        /// for the type.
        /// </summary>
        new TIntermediateCtor TypeInitializer { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a creatable (instantiable) type.
    /// </summary>
    public interface IIntermediateCreatableParent :
        IIntermediateCreatableSignatureParent,
        IIntermediateSignatureParent,
        IIntermediateType,
        ICreatableParent
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="IIntermediateCreatableParent"/>.
        /// </summary>
        new IIntermediateConstructorMemberDictionary Constructors { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateConstructorMember"/> which 
        /// represents the type-initializer (static constructor)
        /// for the type.
        /// </summary>
        new IIntermediateConstructorMember TypeInitializer { get; }
    }
}
