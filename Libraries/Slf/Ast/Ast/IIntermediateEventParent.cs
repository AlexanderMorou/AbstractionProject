using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for a language element that is understood to have
    /// events.
    /// </summary>
    /// <typeparam name="TEvent">The type of <see cref="IEventMember{TEvent, TEventParent}"/> in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of <see cref="IIntermediateEventMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> in
    /// the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type of <see cref="IEventParent{TEvent, TEventParent}"/> which is understood
    /// to contain a series of <typeparamref name="TEvent"/> instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type of<see cref="IIntermediateEventParent{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> which
    /// is understood to contain a series of <typeparamref name="TIntermediateEvent"/> instances.</typeparam>
    public interface IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IEventParent<TEvent, TEventParent>,
        IIntermediateEventParent
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateEventMemberDictionary{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/>
        /// associated to the current <typeparamref name="TIntermediateEventParent"/>
        /// implementation.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasEvents")]
#endif
        new IIntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> Events { get; }
        new bool HasEvents { get; }
    }
    /// <summary>
    /// Defines properties and methods for a language element that is 
    /// understood to have events.
    /// </summary>
    public interface IIntermediateEventParent :
        IEventParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateEventMemberDictionary"/> associated
        /// to the current <see cref="IIntermediateEventParent"/>.
        /// </summary>
        new IIntermediateEventMemberDictionary Events { get; }
        bool HasEvents { get; }
    }
}
