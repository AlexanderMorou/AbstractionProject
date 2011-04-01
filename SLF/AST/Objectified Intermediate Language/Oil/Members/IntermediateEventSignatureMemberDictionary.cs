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
    /// Provides a base event signature dictionary.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the
    /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract class IntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
    IntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
    IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
    IIntermediateEventSignatureMemberDictionary
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
        /// Creates a new <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/>
        /// which contains the <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>.</param>
        public IntermediateEventSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateEventParent parent)
            : base(master, parent)
        {

        }
        /// <summary>
        /// Creates a new <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="items"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/>
        /// which contains the <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>.</param>
        /// <param name="items">The <see cref="Dictionary{TKey, TValue}"/> 
        /// which the <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> wraps.</param>
        public IntermediateEventSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateEventParent parent, IntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> items)
            : base(master, parent, items)
        {

        }
    }

    /// <summary>
    /// Provides a base event signature dictionary.
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
    public abstract class IntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> :
        IntermediateGroupedSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateEventSignatureMemberDictionary
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
        /// Creates a new <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/>
        /// which contains the <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>.</param>
        protected IntermediateEventSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateEventParent parent) :
            base(master, parent)
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="root"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/>
        /// which contains the <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>.</param>
        /// <param name="root">The <see cref="IntermediateEventSignatureMemberDictionary{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>
        /// which the current is based upon.</param>
        protected IntermediateEventSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateEventParent parent, IntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> root) :
            base(master, parent, root)
        {
        }

        #region IIntermediateEventSignatureMemberDictionary<TEvent,TIntermediateEvent,TEventParameter,TIntermediateEventParameter,TEventParent,TIntermediateEventParent> Members

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateEvent"/> with the 
        /// <paramref name="typedName"/> provided
        /// </summary>
        /// <param name="typedName">The <see cref="TypedName"/> which specifies
        /// the event's type and name.</param>
        /// <returns>A new <typeparamref name="TIntermediateEvent"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> is
        /// null or <see cref="String.Empty"/>, or there exists another member within the
        /// current scope with the same name.</exception>
        public TIntermediateEvent Add(TypedName typedName)
        {
            var result = this.GetEvent(typedName);
            if (this.ContainsKey(result.UniqueIdentifier))
                throw new ArgumentException("typedName");
            return result;
        }

        /// <summary>
        /// Gets a new <typeparamref name="TIntermediateEvent"/> with the 
        /// <paramref name="typedName"/> provided.
        /// </summary>
        /// <param name="typedName">The <see cref="TypedName"/> which specifies
        /// the event's type and name.</param>
        /// <returns>A new <typeparamref name="TIntermediateEvent"/> instance.</returns>
        protected abstract TIntermediateEvent GetEvent(TypedName typedName);

        #endregion


        #region IIntermediateEventSignatureMemberDictionary Members

        IIntermediateEventSignatureMember IIntermediateEventSignatureMemberDictionary.Add(TypedName typedName)
        {
            return this.Add(typedName);
        }

        #endregion

    }
}
