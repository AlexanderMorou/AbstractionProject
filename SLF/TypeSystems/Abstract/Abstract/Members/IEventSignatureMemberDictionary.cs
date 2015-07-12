using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of event signature members.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current implementation.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureMemberDictionary<TEvent, TEventParent> :
        IEventSignatureMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of event signature members.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current implementation.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameters used on the <typeparamref name="TEvent"/>
    /// instances in the abstract typs system.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureMemberDictionary<TEvent, TEventParameter, TEventParent> :
        ISignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventParameter, TEventParent>,
        IGroupedMemberDictionary<TEventParent, IGeneralSignatureMemberUniqueIdentifier, TEvent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
        /// <summary>
        /// Searches for the <typeparamref name="TEvent"/> instance that match 
        /// the <paramref name="searchCriteria"/> with the given
        /// <paramref name="eventName"/>.
        /// </summary>
        /// <param name="searchCriteria">The <see cref="IDelegateType"/> that designates the signature to look for.</param>
        /// <param name="eventName">The <see cref="String"/> value representing the unique
        /// identifier of the event that matches the <paramref name="searchCriteria"/>.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TEvent"/> instances that matched the <paramref name="searchCriteria"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="eventName"/> or <paramref name="searchCriteria"/> is null.</exception>
        TEvent Find(string eventName, IDelegateType searchCriteria);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IEventSignatureMember"/>
    /// instances.
    /// </summary>
    public interface IEventSignatureMemberDictionary :
        ISignatureMemberDictionary
    {
        /// <summary>
        /// Searches for the <see cref="IEventSignatureMember"/> instance that match 
        /// the <paramref name="searchCriteria"/> with the given
        /// <paramref name="eventName"/>.
        /// </summary>
        /// <param name="searchCriteria">The <see cref="IDelegateType"/> that designates the signature to look for.</param>
        /// <param name="eventName">The <see cref="String"/> value representing the unique
        /// identifier of the event that matches the <paramref name="searchCriteria"/>.</param>
        /// <returns>A <see cref="IEventSignatureMember"/> which matched the <paramref name="searchCriteria"/>
        /// and <paramref name="eventName"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="eventName"/> or <paramref name="searchCriteria"/> is null.</exception>
        IEventSignatureMember Find(string eventName, IDelegateType searchCriteria);
    }
}
