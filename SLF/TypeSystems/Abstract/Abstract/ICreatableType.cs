using System;
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
    /// Defines generic properties and methods for working with a type that has constructors.
    /// </summary>
    /// <typeparam name="TCtor">The type of <see cref="IConstructorMember{TCtor, TCtorParent}"/> that
    /// represents the <typeparamref name="TType"/>'s constructors.</typeparam>
    /// <typeparam name="TType">The type of <see cref="ICreatableType{TCtor, TType}"/> in the
    /// current implementation that contains <typeparamref name="TCtor"/> instances.</typeparam>
    public interface ICreatableType<TCtor, TType> :
        ISignatureParent<TCtor, IConstructorParameterMember<TCtor, TType>, TType>,
        ICreatableType,
        IType<TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TType :
            ICreatableType<TCtor, TType>
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="ICreatableType{TCtor, TCtorParent}"/>.
        /// </summary>
        new IConstructorMemberDictionary<TCtor, TType> Constructors { get; }
        /// <summary>
        /// Returns the <typeparamref name="TCtor"/> which 
        /// represents the type-initializer (static constructor)
        /// for the type.
        /// </summary>
        new TCtor TypeInitializer { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a <see cref="ICreatableType"/>.
    /// </summary>
    public interface ICreatableType :
        ISignatureParent,
        IType
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="ICreatableType"/>.
        /// </summary>
        IConstructorMemberDictionary Constructors { get; }
        /// <summary>
        /// Returns the <see cref="IConstructorMember"/> which 
        /// represents the type-initializer (static constructor)
        /// for the type.
        /// </summary>
        IConstructorMember TypeInitializer { get; }
    }
}
