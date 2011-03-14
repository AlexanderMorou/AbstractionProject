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
    /// How the intermediate code for the event is managed.
    /// </summary>
    public enum IntermediateEventManagementType
    {
        /// <summary>
        /// The event automatically generates the management code for OnAddMethod, and
        /// OnRemoveMethod.
        /// </summary>
        Automatic,
        /// <summary>
        /// Manually define the management code for the OnAddMethod and OnRemoveMethod.
        /// </summary>
        Manual,
    }
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate event signature
    /// member.
    /// </summary>
    /// <typeparam name="TEvent">The type of event used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the <typeparamref name="TIntermediateEvent"/>
    /// instances in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IIntermediateEventMember,
        IEventMember<TEvent, TEventParent>
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
        /// Returns the <see cref="IIntermediateMethodMember"/>
        /// which is responsible for adding a handler
        /// of the event.
        /// </summary>
        /// <remarks>Parameters are read-only when <see cref="SignatureSource"/>
        /// is <see cref="EventSignatureSource.Delegate"/>.</remarks>
        new IIntermediateMethodMember OnAddMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodMember"/>
        /// which is responsible for removing a handler
        /// of the event.
        /// </summary>
        /// <remarks>Parameters are read-only when <see cref="SignatureSource"/>
        /// is <see cref="EventSignatureSource.Delegate"/>.</remarks>
        new IIntermediateMethodMember OnRemoveMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodMember"/>
        /// which is responsible for raising the event.
        /// </summary>
        /// <remarks><para>Can be null; not all events use
        /// this functionality.</para>
        /// <para>One such example is C&#9839;, events do not 
        /// support nor do they allow such declaration 
        /// or usage.</para></remarks>
        new IIntermediateMethodMember OnRaiseMethod { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate event
    /// member.
    /// </summary>
    public interface IIntermediateEventMember :
        IIntermediateEventSignatureMember,
        IIntermediateExtendedInstanceMember,
        IIntermediateScopedDeclaration,
        IEventMember
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodMember"/>
        /// which is responsible for adding a handler
        /// of the event.
        /// </summary>
        /// <remarks>Parameters are read-only when <see cref="SignatureSource"/>
        /// is <see cref="EventSignatureSource.Delegate"/>.</remarks>
        new IIntermediateMethodMember OnAddMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodMember"/>
        /// which is responsible for removing a handler
        /// of the event.
        /// </summary>
        /// <remarks>Parameters are read-only when <see cref="SignatureSource"/>
        /// is <see cref="EventSignatureSource.Delegate"/>.</remarks>
        new IIntermediateMethodMember OnRemoveMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodMember"/>
        /// which is responsible for raising the event.
        /// </summary>
        /// <remarks><para>Can be null; not all events use
        /// this functionality.</para>
        /// <para>One such example is C&#9839;, events do not 
        /// support nor do they allow such declaration 
        /// or usage.</para></remarks>
        new IIntermediateMethodMember OnRaiseMethod { get; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateEventMember"/> will emit
        /// the necessary code for the raise method.
        /// </summary>
        /// <remarks>Support for this will be an external method in languages
        /// which don't support methods beyond add and remove, such as C&#9839;.</remarks>
        bool EmitRaiseMethod { get; set; }
        /// <summary>
        /// Returns/sets the type of management the event receives</summary>
        /// <remarks>If set to <see cref="IntermediateEventManagementType.Automatic"/>, 
        /// <see cref="OnAddMethod"/>, <see cref="OnRemoveMethod"/>, and <see cref="OnRaiseMethod"/>
        /// will be locked, and immutable.</remarks>
        IntermediateEventManagementType GenerationType { get; set; }
    }
}
