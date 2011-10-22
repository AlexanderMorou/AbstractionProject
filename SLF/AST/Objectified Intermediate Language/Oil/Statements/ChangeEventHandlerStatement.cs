using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides a base statement which represents a
    /// change within an event handler which operates on expressions
    /// which are assumed to contain both a valid event reference and 
    /// a valid method pointer reference, but it's not verified.
    /// </summary>
    public class ChangeEventHandlerStatement : 
        StatementBase,
        IChangeEventHandlerStatement
    {

        public ChangeEventHandlerStatement(IEventReferenceExpression targetEvent, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression sourceMethod, IStatementParent parent)
            : base(parent)
        {
            this.TargetEvent = targetEvent;
            this.ChangeKind = changeKind;
            this.SourceMethod = sourceMethod;
        }
        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region IChangeEventHandlerStatement Members

        /// <summary>
        /// Returns/sets the <see cref="IEventReferenceExpression"/>
        /// which denotes the event to add the <see cref="SourceMethod"/>
        /// as a handler.
        /// </summary>
        public IEventReferenceExpression TargetEvent { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IMethodPointerReferenceExpression"/> which denotes
        /// the source and signature of the method in question.
        /// </summary>
        public IMethodPointerReferenceExpression SourceMethod { get; set; }

        private EventHandlerChangeKind changeKind;

        /// <summary>
        /// Returns the kind of change which occurs as a result of the action implied
        /// by the statement.
        /// </summary>
        public EventHandlerChangeKind ChangeKind
        {
            get
            {
                return this.changeKind;
            }
            set
            {
                if (value == EventHandlerChangeKind.Other)
                    throw new ArgumentOutOfRangeException("value must be Add or Remove; ChangeEventHandlerStatement does not contain sufficient context to call other methods.");
                this.changeKind = value;
            }
        }

        #endregion

        public override string ToString()
        {
            switch (ChangeKind)
            {
                case EventHandlerChangeKind.Add:
                    return string.Format("{0} += {1}", this.TargetEvent, this.SourceMethod);
                case EventHandlerChangeKind.Remove:
                    return string.Format("{0} -= {1}", this.TargetEvent, this.SourceMethod);
            }
            return string.Empty;
        }
    }
}
