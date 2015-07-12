using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines generic properties and methods for working with an
    /// expression which represents a reference to a event signature.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event as it exists in the abstract type system.
    /// </typeparam>
    /// <typeparam name="TEventParameter">
    /// The type of parameters used on the <typeparamref name="TEvent"/>
    /// instances in the abstract typs system.
    /// </typeparam>
    /// <typeparam name="TEventParent">
    /// The type which owns the properties in the abstract type system.
    /// </typeparam>
    public interface IEventReferenceExpression<TEvent, TEventParameter, TEventParent> :
        IBoundMemberReference,
        IEventReferenceExpression
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TEvent"/> member to which the 
        /// <see cref="IEventReferenceExpression{TEvent, TEventParameter, TEventParent}"/> refers.
        /// </summary>
        new TEvent Member { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with an expression 
    /// which represents a reference to an event; unbound, so as the 
    /// target event is malleable.
    /// </summary>
    public interface IUnboundEventReferenceExpression :
        IEventReferenceExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="Name"/> of the expression the
        /// <see cref="IEventReferenceExpression"/> points to.
        /// </summary>
        new string Name { get; set; }
    }

    /// <summary>
    /// Defines properties and methods for working with a reference
    /// which refers to an event.
    /// </summary>
    public interface IEventReferenceExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="Name"/> 
        /// of the expression the <see cref="IEventReferenceExpression"/>
        /// points to.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> 
        /// which contains the source information to accessing
        /// the event.
        /// </summary>
        IMemberParentReferenceExpression Source { get; }
    }
}
