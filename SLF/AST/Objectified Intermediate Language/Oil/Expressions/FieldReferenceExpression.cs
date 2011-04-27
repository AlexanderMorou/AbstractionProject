using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a generic implementation of a field reference 
    /// expression that refers to a specific field.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    public class FieldReferenceExpression<TField, TFieldParent> :
        MemberParentReferenceExpressionBase,
        IFieldReferenceExpression<TField, TFieldParent>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        private string nameCopy;
        public FieldReferenceExpression(TField member, IMemberParentReferenceExpression source)
        {
            this.Member = member;
            this.Source = source;
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.FieldReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        #region IFieldReferenceExpression<TField,TIntermediateField,TFieldParent,TIntermediateFieldParent> Members
        /// <summary>
        /// Returns the <typeparamref name="TField"/> associated to the
        /// <see cref="UnboundFieldReferenceExpression{TField, TFieldParent}"/>.
        /// </summary>
        public TField Member { get; private set; }

        #endregion

        #region IBoundMemberReference Members

        public IType MemberType
        {
            get { return this.Member.FieldType; }
        }

        IMember IBoundMemberReference.Member
        {
            get { return this.Member; }
        }

        #endregion

        #region IMemberReferenceExpression Members

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

        #region IFieldReferenceExpression Members

        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override string ToString()
        {
            if (this.Source != null)
                return string.Format("{0}.{1}", this.Source, this.Name);
            else
                return this.Name;
        }

        protected override IType TypeLookupAid
        {
            get
            {
                if (this.Member == null)
                    return base.TypeLookupAid;
                return this.Member.FieldType;
            }
        }
    }


    public class UnboundFieldReferenceExpression :
        MemberParentReferenceExpressionBase,
        IUnboundFieldReferenceExpression
    {

        public UnboundFieldReferenceExpression(string name, IMemberParentReferenceExpression source)
        {
            this.Name = name;
            this.Source = source;
        }

        #region IFieldReferenceExpression Members

        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        #region IMemberReferenceExpression Members

        public string Name { get; set; }

        #endregion

        public override ExpressionKind Type
        {
            get { return ExpressionKind.FieldReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public override string ToString()
        {
            if (this.Source != null)
                return string.Format("{0}.{1}", this.Source, this.Name);
            else
                return this.Name;
        }
    }
}
