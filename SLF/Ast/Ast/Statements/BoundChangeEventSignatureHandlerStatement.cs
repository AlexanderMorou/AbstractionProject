using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Provides a generic base statement which represents a
    /// change within an event handler which operates on expressions
    /// which are assumed to contain both a valid event reference and 
    /// a valid method pointer reference, but it's not verified.
    /// </summary>
    /// <typeparam name="TEvent">The type of event as it exists in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameter
    /// contained within the events.</typeparam>
    /// <typeparam name="TEventParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
    /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
    /// instances.</typeparam>
    public class BoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> :
        StatementBase,
        IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        private IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent;
        private IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> sourceMethod;
        private EventHandlerChangeKind changeKind;

        public BoundChangeEventSignatureHandlerStatement(IStatementParent parent, IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, EventHandlerChangeKind changeKind,  IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> sourceMethod)
            : base(parent)
        {
            this.targetEvent = targetEvent;
            this.changeKind = changeKind;
            this.sourceMethod = sourceMethod;
        }

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region IChangeEventHandlerStatement<IEventReferenceExpression<TEvent,TEventParameter,TEventParent>,IMethodPointerReferenceExpression<TSignatureParameter,TSignature,TSignatureParent>> Members

        public IEventReferenceExpression<TEvent, TEventParameter, TEventParent> TargetEvent
        {
            get { return this.targetEvent; }
        }

        public IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> SourceMethod
        {
            get { return this.sourceMethod; }
        }

        public EventHandlerChangeKind ChangeKind
        {
            get { return this.changeKind; }
        }

        #endregion
    }
}
