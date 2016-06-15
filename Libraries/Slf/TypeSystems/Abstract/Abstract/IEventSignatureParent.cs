using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with the parent
    /// of a series of events as an <see cref="IType"/>.
    /// </summary>
    public interface IEventSignatureParent :
        IType
    {
        /// <summary>
        /// Returns the <see cref="IEventSignatureMemberDictionary"/>
        /// associated to the current <see cref="IEventSignatureParent"/>
        /// implementation.
        /// </summary>
        IEventSignatureMemberDictionary Events { get; }
    }
    /// <summary>
    /// Defines generic properties and methods for working with
    /// the parent of a series of <typeparamref name="TEvent"/>
    /// instances as a <typeparamref name="TEventParent"/>.
    /// </summary>
    /// <typeparam name="TEvent">The type of 
    /// <see cref="IEventSignatureMember{TEvent, TEventParent}"/>
    /// in the current implementation as only the signature
    /// of the <typeparamref name="TEvent"/>.</typeparam>
    /// <typeparam name="TEventParent">The <see cref="IType{TTypeIdentifier, TType}"/>
    /// which parents the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureParent<TEvent, TEventParent> :
        IEventSignatureParent<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Returns the <see cref="IEventSignatureMemberDictionary{TEvent, TEventParentIdentifier, TEventParent}"/>
        /// associated to the current <typeparamref name="TEventParent"/>
        /// implementation.
        /// </summary>
        new IEventSignatureMemberDictionary<TEvent, TEventParent> Events { get; }
    }
    /// <summary>
    /// Defines generic properties and methods for working with
    /// the parent of a series of <typeparamref name="TEvent"/>
    /// instances as a <typeparamref name="TEventParent"/>.
    /// </summary>
    /// <typeparam name="TEvent">The type of 
    /// <see cref="IEventSignatureMember{TEvent, TEventParameter, TEventParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TEventParameter">The type of 
    /// <see cref="IEventSignatureParameterMember{TEvent, TEventParameter, TEventParent}"/>
    /// in the current implementation that acts as a parameter
    /// of <typeparamref name="TEvent"/> instances.</typeparam>
    /// <typeparam name="TEventParent">The <see cref="IType{TTypeIdentifier, TType}"/>
    /// which parents the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureParent<TEvent, TEventParameter, TEventParent> :
        ISignatureParent<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventParameter, TEventParent>,
        IEventSignatureParent
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
        /// <summary>
        /// Returns the <see cref="IEventSignatureMemberDictionary{TEvent, TEventParameter, TEventParentIdentifier, TEventParent}"/>
        /// associated to the current <typeparamref name="TEventParent"/>
        /// implementation.
        /// </summary>
        new IEventSignatureMemberDictionary<TEvent, TEventParameter, TEventParent> Events { get; }
    }
}
