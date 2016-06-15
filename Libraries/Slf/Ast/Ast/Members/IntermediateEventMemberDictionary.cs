using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    public abstract class IntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IIntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
        IIntermediateEventMemberDictionary
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TIntermediateEvent :
            IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
            TEvent
        where TEventParent :
            IEventParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            class,
            IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
            TEventParent
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateEventMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/>
        /// which contains the <see cref="IntermediateEventMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>.</param>
        public IntermediateEventMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateEventParent parent)
            : base(master, parent)
        {
            
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateEventMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="root"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/>
        /// which contains the <see cref="IntermediateEventMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>.</param>
        /// <param name="root">The <see cref="Dictionary{TKey, TValue}"/> 
        /// which the <see cref="IntermediateEventMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> wraps.</param>
        public IntermediateEventMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateEventParent parent, IntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> root)
            : base(master, parent, root)
        {

        }

        #region IIntermediateEventMemberDictionary Members

        IIntermediateEventMember IIntermediateEventMemberDictionary.Add(TypedName nameAndDelegateType)
        {
            return this.Add(nameAndDelegateType);
        }

        IIntermediateEventMember IIntermediateEventMemberDictionary.Add(string name, TypedNameSeries eventSignature)
        {
            return this.Add(name, eventSignature);
        }

        #endregion

        /// <summary>
        /// Obtains a <typeparamref name="TIntermediateEvent"/> to fulfill an <see cref="Add(String, TypedNameSeries)"/>
        /// request.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the event.</param>
        /// <param name="eventSignature">The <see cref="TypedNameSeries"/> that designates
        /// the names and types of the parameters of a delegate signature that will be
        /// generated upon translation.</param>
        /// <returns>A <typeparamref name="TIntermediateEvent"/> instance.</returns>
        protected abstract TIntermediateEvent GetEvent(string name, TypedNameSeries eventSignature);

        #region IIntermediateEventMemberDictionary<TEvent,TIntermediateEvent,TEventParent,TIntermediateEventParent> Members

        /// <summary>
        /// Adds a <typeparamref name="TIntermediateEvent"/> with the <paramref name="name"/> and
        /// <paramref name="eventSignature"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the event.</param>
        /// <param name="eventSignature">The <see cref="TypedNameSeries"/> that designates
        /// the names and types of the parameters of a delegate signature that will be
        /// generated upon translation.</param>
        /// <returns>A <typeparamref name="TIntermediateEvent"/> instance.</returns>
        public TIntermediateEvent Add(string name, TypedNameSeries eventSignature)
        {
            var result = this.GetEvent(name, eventSignature);
            this.AddDeclaration(result);
            return result;
        }

        #endregion

    }
}
