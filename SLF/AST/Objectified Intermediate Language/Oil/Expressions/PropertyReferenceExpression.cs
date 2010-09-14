using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a generic implementation of an expression
    /// which represents a reference to a property.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public class PropertyReferenceExpression<TProperty, TPropertyParent> :
        MemberParentReferenceExpressionBase,
        IPropertyReferenceExpression<TProperty, TPropertyParent>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        private MethodReferenceType referenceType;

        public PropertyReferenceExpression(IMemberParentReferenceExpression source, TProperty member)
        {
            this.Source = source;
            this.Member = member;
        }

        #region IPropertyReferenceExpression<TProperty,TIntermediateProperty,TPropertyParent,TIntermediatePropertyParent> Members

        /// <summary>
        /// Returns the <typeparamref name="TIntermediateProperty"/> member to which the 
        /// <see cref="IPropertyReferenceExpression{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/> refers.
        /// </summary>
        public TProperty Member { get; private set; }

        #endregion

        #region ITypedMemberReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the member
        /// </summary>
        public IType MemberType
        {
            get { return this.Member.PropertyType; }
        }

        IMember ITypedMemberReferenceExpression.Member
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
            set
            {
                if (this.Member is IIntermediateMember)
                    ((IIntermediateMember)(this.Member)).Name = value;
                else
                    this.Rebind(value);
            }
        }

        private void Rebind(string value)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region IPropertyReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IPropertyReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; set; }

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
        /// Returns the type of expression the <see cref="PropertyReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKinds.PropertyReference"/>.</remarks>
        public override ExpressionKind Type 
        {
            get { return ExpressionKinds.PropertyReference; }
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
    /// <typeparam name="TIntermediateProperty">The type of property as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public class PropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> :
        MemberParentReferenceExpressionBase,
        IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent>
        where TPropertySignature :
            IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
        where TPropertySignatureParent :
            IPropertySignatureParentType<TPropertySignature, TPropertySignatureParent>
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        private MethodReferenceType referenceType;

        public PropertySignatureReferenceExpression(IMemberParentReferenceExpression source, TPropertySignature member)
        {
            this.Source = source;
            this.Member = member;
        }

        #region IPropertySignatureReferenceExpression<TPropertySignature,TPropertySignatureParent> Members

        /// <summary>
        /// Returns the <see cref="TProperty"/> member to which the 
        /// <see cref="IPropertySignatureReferenceExpression{TPropertySignature, TPropertySignatureParent}"/> refers.
        /// </summary>
        public TPropertySignature Member { get; private set; }

        #endregion

        #region ITypedMemberReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the member
        /// </summary>
        public IType MemberType
        {
            get { return this.Member.PropertyType; }
        }

        IMember ITypedMemberReferenceExpression.Member
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
            set
            {
                if (this.Member is IIntermediateMember)
                    ((IIntermediateMember)(this.Member)).Name = value;
                else
                    this.Rebind(value);
            }
        }

        private void Rebind(string value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPropertySignatureReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IPropertySignatureReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IPropertySignatureReferenceExpression"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source.ToString(), this.Name);
        }

        /// <summary>
        /// Returns the type of expression the <see cref="PropertyReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKinds.PropertyReference"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.PropertyReference; }
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
                return this.Member.PropertyType;
            }
        }
    }

    /// <summary>
    /// Provides a base implementation of an <see cref="IPropertyReferenceExpression"/>
    /// which references a <see cref="IPropertySignatureMember"/> 
    /// in code by name.
    /// </summary>
    public class PropertyReferenceExpression :
        MemberParentReferenceExpressionBase,
        IPropertyReferenceExpression 
    {
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;
        /// <summary>
        /// Data member for <see cref="Source"/>.
        /// </summary>
        private IMemberParentReferenceExpression source;

        /// <summary>
        /// Creates a new <see cref="PropertyReferenceExpression"/> with the <paramref name="name"/>
        /// and <paramref name="source"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> relative to the
        /// property to retrieve a reference to.</param>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="PropertyReferenceExpression"/> is to be
        /// sourced.</param>
        public PropertyReferenceExpression(string name, IMemberParentReferenceExpression source)
        {
            this.name = name;
            this.source = source;
        }

        #region IPropertyReferenceExpression Members
        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IPropertyReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IPropertyReferenceExpression"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source
        {
            get { return this.source; }
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
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        #endregion

        /// <summary>
        /// Returns the type of expression the <see cref="PropertyReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKinds.PropertyReference"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.PropertyReference; }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.source.ToString(), this.Name);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
