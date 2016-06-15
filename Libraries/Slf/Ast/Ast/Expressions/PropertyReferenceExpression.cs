using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Provides a generic implementation of an expression
    /// which represents a reference to a property.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    [DebuggerDisplay("{ToString(),nq} ({TypeLookupAidString,nq})")]
    public class PropertyReferenceExpression<TProperty, TPropertyParent> :
        MemberParentReferenceExpressionBase,
        IPropertyReferenceExpression<TProperty, TPropertyParent>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
    {

        public PropertyReferenceExpression(IMemberParentReferenceExpression source, TProperty member, MethodReferenceType referenceType = MethodReferenceType.VirtualMethodReference)
        {
            this.Source = source;
            this.Member = member;
            this.ReferenceType = referenceType;
        }

        #region IPropertyReferenceExpression<TProperty,TIntermediateProperty,TPropertyParent,TIntermediatePropertyParent> Members

        /// <summary>
        /// Returns the <typeparamref name="TProperty"/> member to which the 
        /// <see cref="PropertyReferenceExpression{TProperty, TPropertyParent}"/> refers.
        /// </summary>
        public TProperty Member { get; private set; }

        #endregion

        #region IBoundMemberReference Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the member
        /// </summary>
        public IType MemberType
        {
            get { return this.Member.PropertyType; }
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

        #region IPropertyReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IPropertyReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; private set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IPropertyReferenceExpression"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source.ToString(), this.Name);
        }

        /// <summary>
        /// Returns the type of expression the 
        /// <see cref="PropertyReferenceExpression{TProperty, TPropertyParent}"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.PropertyReference"/>.</remarks>
        public override ExpressionKind Type 
        {
            get { return ExpressionKind.PropertyReference; }
        }

        public override void Accept(IExpressionVisitor visitor) 
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        private string TypeLookupAidString
        {
            get
            {
                return TypeLookupAid.ToString();
            }
        }
        protected override IType TypeLookupAid
        {
            get
            {
                if (this.Member == null)
                    return base.TypeLookupAid;
                return this.Member.PropertyType;
            }
        }

    }

    /// <summary>
    /// Provides a generic implementation of an expression
    /// which represents a reference to a property signature.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    public class PropertySignatureReferenceExpression<TProperty, TPropertyParent> :
        MemberParentReferenceExpressionBase,
        IPropertySignatureReferenceExpression<TProperty, TPropertyParent>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParent<TProperty, TPropertyParent>
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        public PropertySignatureReferenceExpression(IMemberParentReferenceExpression source, TProperty member, MethodReferenceType referenceType = MethodReferenceType.VirtualMethodReference)
        {
            this.ReferenceType = referenceType;
            this.Source = source;
            this.Member = member;
        }

        #region IPropertySignatureReferenceExpression<TProperty,TPropertyParent> Members

        /// <summary>
        /// Returns the <typeparamref name="TProperty"/> member to which the 
        /// <see cref="IPropertySignatureReferenceExpression{TProperty, TPropertyParent}"/> refers.
        /// </summary>
        public TProperty Member { get; private set; }

        #endregion

        #region IBoundMemberReference Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the member
        /// </summary>
        public IType MemberType
        {
            get { return this.Member.PropertyType; }
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

        #region IPropertySignatureReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="PropertySignatureReferenceExpression{TProperty, TPropertyParent}"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; private set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="PropertySignatureReferenceExpression{TProperty, TPropertyParent}"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source.ToString(), this.Name);
        }

        /// <summary>
        /// Returns the type of expression the <see cref="PropertySignatureReferenceExpression{TProperty, TPropertyParent}"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.PropertyReference"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKind.PropertyReference; }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        private string TypeLookupAidString
        {
            get
            {
                return TypeLookupAid.ToString();
            }
        }

        protected override IType TypeLookupAid
        {
            get
            {
                if (this.Member == null)
                    return base.TypeLookupAid;
                return this.Member.PropertyType;
            }
        }
    }

}
