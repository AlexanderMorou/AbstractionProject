using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    internal class AutoContextMemberSource :
        MemberParentReferenceExpressionBase,
        ISpecialReferenceExpression,
        ITypeReferenceExpression
    {
        private IInstanceMember member;
        public AutoContextMemberSource(IInstanceMember member)
        {
            this.member = member;

        }

        #region ISpecialReferenceExpression Members

        public SpecialReferenceKind Kind
        {
            get
            {
                if (member.IsStatic || MemberIsConstant())
                    return SpecialReferenceKind.None;
                else
                    return SpecialReferenceKind.This;
            }
            set
            {
                throw new NotSupportedException("Special reference kind cannot be assigned.");
            }
        }

        #endregion

        #region IExpression Members

        public override ExpressionKind Type
        {
            get
            {
                if (member.IsStatic || MemberIsConstant())
                {
                    return ExpressionKind.TypeReference;
                }
                else
                    return ExpressionKind.ThisReference;
            }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            if (member.IsStatic || MemberIsConstant())
                visitor.Visit((ITypeReferenceExpression)this);
            else
                visitor.Visit((ISpecialReferenceExpression)this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            if (member.IsStatic || MemberIsConstant())
                return visitor.Visit((ITypeReferenceExpression)this, context);
            else
                return visitor.Visit((ISpecialReferenceExpression)this, context);
        }

        #endregion

        #region ITypeReferenceExpression Members

        public IType ReferenceType
        {
            get
            {
                if (member.IsStatic || MemberIsConstant())
                    return (member.Parent as IType);
                else
                    return null;
            }
        }

        #endregion

        public override string ToString()
        {
            if (member.IsStatic || MemberIsConstant())
            {
                var refType = this.ReferenceType;
                if (refType == null)
                    return string.Empty;
                return refType.BuildTypeName(true, false, TypeParameterDisplayMode.DebuggerStandard);
            }
            else
                return "this";
        }

        private bool MemberIsConstant()
        {
            return member is IClassFieldMember && ((IClassFieldMember)(member)).Constant ||
                   member is IStructFieldMember && ((IStructFieldMember)(member)).Constant;
        }
        protected override IType TypeLookupAid
        {
            get
            {
                if (this.member != null)
                {
                    var memberParent = member.Parent as IType;
                    if (memberParent != null)
                        return memberParent;
                }
                return base.TypeLookupAid;
            }
        }
    }
}
