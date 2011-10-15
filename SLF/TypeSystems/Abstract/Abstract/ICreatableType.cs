using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
    /// represents the <typeparamref name="TCtorParent"/>'s constructors.</typeparam>
    /// <typeparam name="TCtorParent">The type of <see cref="ICreatableParent{TCtor, TCtorParent}"/> in the
    /// current implementation that contains <typeparamref name="TCtor"/> instances.</typeparam>
    public interface ICreatableParent<TCtor, TCtorParent> :
        ISignatureParent<IGeneralSignatureMemberUniqueIdentifier, TCtor, IConstructorParameterMember<TCtor, TCtorParent>, TCtorParent>,
        ICreatableParent
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="ICreatableParent{TCtor, TCtorParent}"/>.
        /// </summary>
        new IConstructorMemberDictionary<TCtor, TCtorParent> Constructors { get; }
        /// <summary>
        /// Returns the <typeparamref name="TCtor"/> which 
        /// represents the type-initializer (static constructor)
        /// for the type.
        /// </summary>
        new TCtor TypeInitializer { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a <see cref="ICreatableParent"/>.
    /// </summary>
    public interface ICreatableParent :
        ISignatureParent,
        IType
    {
        /// <summary>
        /// Returns the constructors contained by the <see cref="ICreatableParent"/>.
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
