using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the signature
    /// of an intermediate event member.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the
    /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IEventSignatureMember<TEvent, TEventParent>
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
    /// Defines generic properties and methods for working with the signature
    /// of a base intermediate event member.
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
    public interface IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> :
        IIntermediateSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateEventSignatureMember,
        IEventSignatureMember<TEvent, TEventParameter, TEventParent>
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
    }
    /// <summary>
    /// Defines properties and methods for working with the signature
    /// of an intermediate event member.
    /// </summary>
    public interface IIntermediateEventSignatureMember :
        IIntermediateSignatureMember,
        IEventSignatureMember
    {
        /// <summary>
        /// Returns/sets the <see cref="EventSignatureSource"/> which designates 
        /// where the <see cref="IIntermediateEventSignatureMember"/>'s 
        /// signature is sourced.
        /// </summary>
        /// <remarks>If <see cref="SignatureSource"/> is <see cref="EventSignatureSource.Delegate"/>
        /// and is changed to <see cref="EventSignatureSource.Declared"/>,
        /// a new type will be generated that matches the original
        /// <see cref="SignatureType"/> when the object model is translated
        /// for the first time.</remarks>
        new EventSignatureSource SignatureSource { get; set; }
        /// <summary>
        /// Returns the <see cref="IDelegateType"/> from 
        /// which the <see cref="IEventMember"/>'s 
        /// signature is sourced.
        /// </summary>
        /// <remarks><para>An <see cref="IIntermediateEventMember"/>'s
        /// <see cref="SignatureType"/> can be an auto-generated
        /// type.</para>
        /// <para>If the <see cref="SignatureType"/> is generated,
        /// then the type yielded may or may not be emitted
        /// based upon the specifics of the target 
        /// translation language.</para><para>The primary reason for this is:
        /// Visual Basic can auto-generate the associated type, where as
        /// C&#9839; requires the signature to be a type already defined.  
        /// In short: C&#9839; will emit an auto-generated type upon translation if
        /// <see cref="SignatureSource"/> is <see cref="EventSignatureSource.Declared"/> and
        /// Visual Basic will not.</para></remarks>
        new IDelegateType SignatureType { get; set; }
    }
}
