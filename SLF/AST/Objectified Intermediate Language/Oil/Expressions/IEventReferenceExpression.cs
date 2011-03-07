using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines generic properties and methods for working with an expression
    /// which represents a reference to a event signature.
    /// </summary>
    /// <typeparam name="TEvent">The type of event as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IEventSignatureReferenceExpression<TEvent, TEventParent> :
        IBoundMemberReference,
        IEventReferenceExpression
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TEvent"/> member to which the 
        /// <see cref="IEventSignatureReferenceExpression"/> refers.
        /// </summary>
        new TEvent Member { get; }
    }
    /// <summary>
    /// Defines generic properties and methods for working with an expression
    /// which represents a reference to a event.
    /// </summary>
    /// <typeparam name="TEvent">The type of event as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IEventReferenceExpression<TEvent, TEventParent> :
        IBoundMemberReference,
        IEventReferenceExpression
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateEvent"/> member to which the 
        /// <see cref="IEventReferenceExpression{TEvent, TEventParent}"/> refers.
        /// </summary>
        new TEvent Member { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a reference which refers to an event.
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
