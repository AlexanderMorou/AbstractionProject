using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a
    /// signature event member.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current
    /// implementation.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of
    /// the <typeparamref name="TEvent"/> instances in the current
    /// implementation.</typeparam>
    public interface IEventSignatureMember<TEvent, TEventParent> :
        IEventSignatureMember<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with a 
    /// signature event member.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current 
    /// implementation.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameters used
    /// on the <typeparamref name="TEvent"/>
    /// instances in the abstract typs system.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of
    /// the <typeparamref name="TEvent"/> instances in the current
    /// implementation.</typeparam>
    public interface IEventSignatureMember<TEvent, TEventParameter, TEventParent> :
        ISignatureMember<TEvent, TEventParameter, TEventParent>,
        IEventSignatureMember
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with
    /// the signature of an event.
    /// </summary>
    public interface IEventSignatureMember :
        ISignatureMember
    {
        /// <summary>
        /// Returns the <see cref="EventSignatureSource"/> which
        /// designates where the <see cref="IEventSignatureMember"/>'s 
        /// signature is sourced.
        /// </summary>
        /// <remarks>For compiled events this is always 
        /// <see cref="EventSignatureSource.Delegate"/>.</remarks>
        EventSignatureSource SignatureSource { get; }
        /// <summary>
        /// Returns the <see cref="IDelegateType"/> from 
        /// which the <see cref="IEventMember"/>'s 
        /// signature is sourced.
        /// </summary>
        /// <remarks><para>An <see cref="IEventSignatureMember"/>'s
        /// <see cref="SignatureType"/> can be an auto-generated
        /// type.</para>
        /// <para>If the <see cref="SignatureType"/> is generated,
        /// then the type yielded may or may not be emitted based upon
        /// the specifics of the target translation language.
        /// </para><para>The primary reason for this is:
        /// Visual Basic can auto-generate the associated type, where 
        /// as C&#9839; requires the signature to be a type already 
        /// defined.  C&#9839; will emit an auto-generated type upon 
        /// translation if <see cref="SignatureSource"/> is 
        /// <see cref="EventSignatureSource.Declared"/> and Visual 
        /// Basic will not.</para></remarks>
        IDelegateType SignatureType { get; }
        ///// <summary>
        ///// Returns the <see cref="IMethodSignatureMember"/>
        ///// which is responsible for adding a handler
        ///// of the event.
        ///// </summary>
        //IMethodSignatureMember OnAddMethod { get; }
        ///// <summary>
        ///// Returns the <see cref="IMethodSignatureMember"/>
        ///// which is responsible for removing a handler
        ///// of the event.
        ///// </summary>
        //IMethodSignatureMember OnRemoveMethod { get; }

    }
}
