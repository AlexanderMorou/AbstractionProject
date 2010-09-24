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
    /// Defines properties and methods for working with a series of intermediate
    /// event members.
    /// </summary>
    /// <typeparam name="TEvent">The type of event member in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event member in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the event members
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the event
    /// members in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IEventMemberDictionary<TEvent, TEventParent>
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TIntermediateEvent :
            IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
            TEvent
        where TEventParent :
            IEventParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
            TEventParent
    {
        /// <summary>
        /// Adds a <typeparamref name="TIntermediateEvent"/> with the <paramref name="nameAndDelegateType"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndDelegateType">The name of the event and its subsequent delegate type.</param>
        /// <returns>A <typeparamref name="TIntermediateEvent"/> instance.</returns>
        new TIntermediateEvent Add(TypedName nameAndDelegateType);
        /// <summary>
        /// Adds a <typeparamref name="TIntermediateEvent"/> with the <paramref name="name"/> and
        /// <paramref name="eventSignature"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the event.</param>
        /// <param name="eventSignature">The <see cref="TypedNameSeries"/> that designates
        /// the names and types of the parameters of a delegate signature that will be
        /// generated upon translation.</param>
        /// <returns>A <typeparamref name="TIntermediateEvent"/> instance.</returns>
        new TIntermediateEvent Add(string name, TypedNameSeries eventSignature);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of intermediate event members.
    /// </summary>
    public interface IIntermediateEventMemberDictionary :
        IEventMemberDictionary
    {
        /// <summary>
        /// Adds a <see cref="IIntermediateEventMember"/> with the <paramref name="nameAndDelegateType"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndDelegateType">The name of the event and its subsequent delegate type.</param>
        /// <returns>A <see cref="IIntermediateEventMember"/> instance.</returns>
        IIntermediateEventMember Add(TypedName nameAndDelegateType);
        /// <summary>
        /// Adds a <see cref="IIntermediateEventMember"/> with the <paramref name="name"/> and
        /// <paramref name="eventSignature"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the event.</param>
        /// <param name="eventSignature">The <see cref="TypedNameSeries"/> that designates
        /// the names and types of the parameters of a delegate signature that will be
        /// generated upon translation.</param>
        /// <returns>A <see cref="IIntermediateEventMember"/> instance.</returns>
        IIntermediateEventMember Add(string name, TypedNameSeries eventSignature);
    }
}
