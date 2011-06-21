using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
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
    /// change within an event handler.
    /// </summary>
    public interface IChangeEventHandlerStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IEventReferenceExpression"/>
        /// which denotes the event to add the <see cref="SourceMethod"/>
        /// as a handler.
        /// </summary>
        IEventReferenceExpression TargetEvent { get; }
        /// <summary>
        /// Returns/sets the <see cref="IMethodPointerReferenceExpression"/> which denotes
        /// the source and signature of the method in question.
        /// </summary>
        IMethodPointerReferenceExpression SourceMethod { get; }

        /// <summary>
        /// Returns the kind of change which occurs as a result of the action implied
        /// by the statement.
        /// </summary>
        EventHandlerChangeKind ChangeKind { get; }

    }
}
