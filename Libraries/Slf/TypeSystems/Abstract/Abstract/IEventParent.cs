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
    /// Defines generic properties and methods for working with
    /// the parent of a series of <typeparamref name="TEvent"/>
    /// instances as a <typeparamref name="TEventParent"/>.
    /// </summary>
    /// <typeparam name="TEvent">The type of 
    /// <see cref="IEventMember{TEvent, TEventParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TEventParent">The <see cref="IType{TTypeIdentifier, TType}"/>
    /// which parents the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventParent<TEvent, TEventParent> :
        IEventSignatureParent<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventParent
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Returns the <see cref="IEventMemberDictionary{TEvent, TEventParent}"/>
        /// associated to the current <typeparamref name="TEventParent"/>
        /// implementation.
        /// </summary>
        new IEventMemberDictionary<TEvent, TEventParent> Events { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a type
    /// which is the parent of a series of events.
    /// </summary>
    public interface IEventParent :
        IEventSignatureParent
    {
    }
}
