using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of
    /// intermediate event signature members.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the
    /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IEventSignatureMemberDictionary<TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with a base series of
    /// intermediate event signature members.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameter used by the event in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParameter">The type of parameter used by the event 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the
    /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> :
        IIntermediateSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateGroupedMemberDictionary<TEventParent, TIntermediateEventParent, TEvent, TIntermediateEvent>,
        IEventSignatureMemberDictionary<TEvent, TEventParameter, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEvent :
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
            TEvent
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParameter :
            IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
            TEventParameter
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParent :
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
            TEventParent
    {
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateEvent"/> with the 
        /// <paramref name="typedName"/> provided
        /// </summary>
        /// <param name="typedName">The <see cref="TypedName"/> which specifies
        /// the event's type and name.</param>
        /// <returns>A new <typeparamref name="TIntermediateEvent"/> instance.</returns>
        TIntermediateEvent Add(TypedName typedName);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// intermediate event signature members.
    /// </summary>
    public interface IIntermediateEventSignatureMemberDictionary :
        IIntermediateSignatureMemberDictionary,
        IEventSignatureMemberDictionary
    {
        /// <summary>
        /// Adds a new <see cref="IIntermediateEventSignatureMember"/> with the 
        /// <paramref name="typedName"/> provided
        /// </summary>
        /// <param name="typedName">The <see cref="TypedName"/> which specifies
        /// the event's type and name.</param>
        /// <returns>A new <see cref="IIntermediateEventSignatureMember"/> instance.</returns>
        IIntermediateEventSignatureMember Add(TypedName typedName);
    }
}
