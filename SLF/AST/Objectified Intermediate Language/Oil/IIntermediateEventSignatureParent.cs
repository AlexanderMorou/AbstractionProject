using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IEventSignatureParent<TEvent, TEventParent>
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
        /// <summary>
        /// Returns the <see cref="IIntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>
        /// associated to the current <see cref="IIntermediateEventSignatureParent{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>
        /// implementation.
        /// </summary>
        new IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> Events { get; }
    }
    public interface IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> :
        IIntermediateSignatureParent<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateType,
        IEventSignatureParent<TEvent, TEventParameter, TEventParent>,
        IIntermediateEventSignatureParent
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParameter :
            TEventParameter,
            IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>
        /// associated to the current <see cref="IIntermediateEventSignatureParent{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>
        /// implementation.
        /// </summary>
        new IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> Events { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a language
    /// element known to have event signature members.
    /// </summary>
    public interface IIntermediateEventSignatureParent :
        IEventSignatureParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateEventSignatureMemberDictionary"/>
        /// associated to the current <see cref="IIntermediateEventSignatureParent"/>
        /// implementation.
        /// </summary>
        new IIntermediateEventSignatureMemberDictionary Events { get; }
    }
}