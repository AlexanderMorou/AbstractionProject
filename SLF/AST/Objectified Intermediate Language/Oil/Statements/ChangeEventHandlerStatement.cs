using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
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

        public IEventReferenceExpression TargetEvent { get; private set; }

        public IMethodPointerReferenceExpression SourceMethod { get; private set; }

        private EventHandlerChangeKind changeKind;
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
