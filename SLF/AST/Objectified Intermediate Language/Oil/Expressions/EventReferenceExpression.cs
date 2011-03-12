using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a generic implementation of an expression
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
    public class EventReferenceExpression<TEvent, TEventParent> :
        MemberParentReferenceExpressionBase,
        IEventReferenceExpression<TEvent, TEventParent>
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        private MethodReferenceType referenceType;
        private string nameCopy;

        public EventReferenceExpression(IMemberParentReferenceExpression source, TEvent member)
        {
            this.Source = source;
            this.Member = member;
        }

        #region IEventReferenceExpression<TEvent,TIntermediateEvent,TEventParent,TIntermediateEventParent> Members

        /// <summary>
        /// Returns the <typeparamref name="TIntermediateEvent"/> member to which the 
        /// <see cref="IEventReferenceExpression{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> refers.
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
                if (this.Member == null)
                    return nameCopy;
                return this.Member.Name;
            }
        }

        #endregion

        #region IEventReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IEventReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IEventReferenceExpression"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source.ToString(), this.Name);
        }

        /// <summary>
        /// Returns the type of expression the <see cref="EventReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKinds.EventReference"/>.</remarks>
        public override ExpressionKinds Type
        {
            get { return ExpressionKinds.EventReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override IType TypeLookupAid
        {
            get
            {
                if (this.Member == null)
                    return base.TypeLookupAid;
                return this.Member.ReturnType;
            }
        }

    }

    /// <summary>
    /// Provides a generic implementation of an expression
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
    public class EventSignatureReferenceExpression<TEvent, TEventParent> :
        MemberParentReferenceExpressionBase,
        IEventSignatureReferenceExpression<TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        private MethodReferenceType referenceType;
        private string nameCopy;

        public EventSignatureReferenceExpression(IMemberParentReferenceExpression source, TEvent member)
        {
            this.Source = source;
            this.Member = member;
        }

        #region IEventSignatureReferenceExpression<TEvent,TEventParent> Members

        /// <summary>
        /// Returns the <see cref="TEvent"/> member to which the 
        /// <see cref="IEventSignatureReferenceExpression{TEvent, TEventParent}"/> refers.
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
        /// that sourced the <see cref="IEventSignatureReferenceExpression"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source.ToString(), this.Name);
        }

        /// <summary>
        /// Returns the type of expression the <see cref="EventReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKinds.EventReference"/>.</remarks>
        public override ExpressionKinds Type
        {
            get { return ExpressionKinds.EventReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override IType TypeLookupAid
        {
            get
            {
                if (this.Member == null)
                    return base.TypeLookupAid;
                return this.Member.ReturnType;
            }
        }
    }
    public class EventReferenceExpression : 
        IEventReferenceExpression
    {

        public EventReferenceExpression(string name, IMemberParentReferenceExpression source)
        {
            this.Source = source;
            this.Name = name;
        }

        #region IEventReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="Name"/> 
        /// of the expression the <see cref="EventReferenceExpression"/>
        /// points to.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> 
        /// which contains the source information to accessing
        /// the event.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        #region IExpression Members

        public ExpressionKinds Type
        {
            get {
                return ExpressionKinds.EventReference;
            }
        }

        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source, this.Name);
        }

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion
    }
}
