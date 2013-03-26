using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Provides a generic implementation of an expression
    /// which represents a reference to a event signature.
    /// </summary>
    /// <typeparam name="TEvent">The type of event as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameters used on the <typeparamref name="TEvent"/>
    /// instances in the abstract typs system.</typeparam>
    /// <typeparam name="TEventParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    public class EventReferenceExpression<TEvent, TEventParameter, TEventParent> :
        IEventReferenceExpression<TEvent, TEventParameter, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {

        public EventReferenceExpression(IMemberParentReferenceExpression source, TEvent member)
        {
            this.Source = source;
            this.Member = member;
        }

        #region IEventSignatureReferenceExpression<TEvent,TEventParent> Members

        /// <summary>
        /// Returns the <typeparamref name="TEvent"/> member to which the 
        /// <see cref="EventReferenceExpression{TEvent, TEventParameter, TEventParent}"/> refers.
        /// </summary>
        public TEvent Member { get; private set; }

        #endregion

        #region IBoundMemberReference Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the member
        /// </summary>
        public IType MemberType
        {
            get { return this.Member.ReturnType; }
        }

        IMember IBoundMemberReference.Member
        {
            get { return this.Member; }
        }

        #endregion

        #region IMemberReferenceExpression Members

        /// <summary>
        /// Returns/sets the name of the member to reference.
        /// </summary>
        public string Name
        {
            get
            {
                return this.Member.Name;
            }
        }

        #endregion

        #region IEventSignatureReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="EventReferenceExpression{TEvent, TEventParameter, TEventParent}"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source.ToString(), this.Name);
        }

        #region IExpression Members

        /// <summary>
        /// Returns the type of expression the <see cref="EventReferenceExpression{TEvent, TEventParameter, TEventParent}"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.EventReference"/>.</remarks>
        public ExpressionKind Type
        {
            get { return ExpressionKind.EventReference; }
        }

        /// <summary>
        /// Visits the <see cref="IExpressionVisitor"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IExpressionVisitor"/>
        /// to visit.</param>
        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Visit<TResult>(IExpressionVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }

        #endregion

        #region ISourceElement Members

        /// <summary>
        /// Returns/sets the filename associated to the <see cref="EventReferenceExpression{TEvent, TEventParameter, TEventParent}"/>.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes
        /// the start point of the <see cref="EventReferenceExpression{TEvent, TEventParameter, TEventParent}"/>.
        /// </summary>
        public LineColumnPair? Start { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes the
        /// end point of the <see cref="EventReferenceExpression{TEvent, TEventParameter, TEventParent}"/>.
        /// </summary>
        public LineColumnPair? End { get; set; }

        #endregion
    }
    public class UnboundEventReferenceExpression :
        IUnboundEventReferenceExpression
    {

        public UnboundEventReferenceExpression(string name, IMemberParentReferenceExpression source)
        {
            this.Source = source;
            this.Name = name;
        }

        #region IEventReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="Name"/> 
        /// of the expression the <see cref="UnboundEventReferenceExpression"/>
        /// points to.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> 
        /// which contains the source information to accessing
        /// the event.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        #region IExpression Members

        public ExpressionKind Type
        {
            get
            {
                return ExpressionKind.EventReference;
            }
        }

        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Visit<TResult>(IExpressionVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source, this.Name);
        }

        #region ISourceElement Members

        /// <summary>
        /// Returns/sets the filename associated to the <see cref="UnboundEventReferenceExpression"/>.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes
        /// the start point of the <see cref="UnboundEventReferenceExpression"/>.
        /// </summary>
        public LineColumnPair? Start { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes the
        /// end point of the <see cref="UnboundEventReferenceExpression"/>.
        /// </summary>
        public LineColumnPair? End { get; set; }

        #endregion
    }
}
