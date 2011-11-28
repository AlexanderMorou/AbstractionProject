using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// The kind of change which occurs on the handlers
    /// of an event.
    /// </summary>
    public enum EventHandlerChangeKind
    {
        /// <summary>
        /// A handler for the event was added.
        /// </summary>
        Add,
        /// <summary>
        /// A handler for the event was removed.
        /// </summary>
        Remove,
        /// <summary>
        /// An other method for the event was invoked.
        /// </summary>
        Other,
    }

    /// <summary>
    /// Defines properties and methods for working with a
    /// change within an event handler which operates on expressions
    /// which are assumed to contain both a valid event reference and 
    /// a valid method pointer reference, but it's not verified.
    /// </summary>
    public interface IChangeEventHandlerStatement :
        IChangeEventHandlerStatement<IEventReferenceExpression, IMethodPointerReferenceExpression>
    {
        /// <summary>
        /// Returns/sets the <see cref="IEventReferenceExpression"/>
        /// which denotes the event to add the <see cref="SourceMethod"/>
        /// as a handler.
        /// </summary>
        new IEventReferenceExpression TargetEvent { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IMethodPointerReferenceExpression"/> which denotes
        /// the source and signature of the method in question.
        /// </summary>
        new IMethodPointerReferenceExpression SourceMethod { get; set; }
    }

    public interface IChangeEventHandlerStatement<TEventTarget, TMethodTarget>
        where TEventTarget :
            IExpression
        where TMethodTarget :
            IExpression
    {
        /// <summary>
        /// Returns the <typeparamref name="TEventTarget"/>
        /// which denotes the event to add the <see cref="SourceMethod"/>
        /// as a handler.
        /// </summary>
        TEventTarget TargetEvent { get; }
        /// <summary>
        /// Returns the <typeparamref name="TMethodTarget"/> which denotes
        /// the source and signature of the method in question.
        /// </summary>
        TMethodTarget SourceMethod { get; }
        /// <summary>
        /// Returns the kind of change which occurs as a result of the action implied
        /// by the statement.
        /// </summary>
        EventHandlerChangeKind ChangeKind { get; }
    }
}
