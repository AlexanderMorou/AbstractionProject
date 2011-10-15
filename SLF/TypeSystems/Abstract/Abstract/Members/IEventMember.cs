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
    /// Defines generic properties and methods for working with an event member.
    /// </summary>
    /// <typeparam name="TEvent">The type of 
    /// <see cref="IEventMember{TEvent, TEventParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TEventParent">The <see cref="IType{TTypeIdentifier, TType}"/>
    /// which parents the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventMember<TEvent, TEventParent> :
        IEventSignatureMember<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventMember
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an event member.
    /// </summary>
    public interface IEventMember :
        IEventSignatureMember,
        IExtendedInstanceMember,
        IScopedDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IMethodMember"/>
        /// which is responsible for adding a handler
        /// of the event.
        /// </summary>
        IMethodMember OnAddMethod { get; }
        /// <summary>
        /// Returns the <see cref="IMethodMember"/>
        /// which is responsible for removing a handler
        /// of the event.
        /// </summary>
        IMethodMember OnRemoveMethod { get; }
        /// <summary>
        /// Returns the <see cref="IMethodMember"/>
        /// which is responsible for raising the event.
        /// </summary>
        /// <remarks><para>Can be null; not all events use
        /// this functionality.</para>
        /// <para>One such example is C&#9839;, events do not 
        /// support nor do they allow such declaration 
        /// or usage.</para></remarks>
        IMethodMember OnRaiseMethod { get; }
        /// <summary>
        /// Returns whether the <see cref="IEventMember"/>
        /// can be raised through a <see cref="OnRaiseMethod"/>.
        /// </summary>
        /// <remarks>If <see cref="CanRaise"/> is false
        /// normal delegate calling conventions are 
        /// utilized.</remarks>
        bool CanRaise { get; }
    }
}
