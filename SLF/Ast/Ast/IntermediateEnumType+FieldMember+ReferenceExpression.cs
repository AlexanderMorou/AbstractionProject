using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateEnumType
    {
        partial class FieldMember
        {
            private class ReferenceExpression :
                MemberParentReferenceExpressionBase,
                IFieldReferenceExpression<IEnumFieldMember, IEnumType>
            {
                private FieldMember owner;

                public ReferenceExpression(FieldMember owner, IMemberParentReferenceExpression source)
                {
                    if (owner == null)
                        throw new ArgumentNullException("owner");

                    this.owner = owner;
                    this.Source = source;
                }

                #region IFieldReferenceExpression Members

                public IMemberParentReferenceExpression Source { get; private set; }

                #endregion

                #region IMemberReferenceExpression Members

                public string Name
                {
                    get
                    {
                        return this.owner.Name;
                    }
                }

                #endregion

                public override ExpressionKind Type
                {
                    get { return ExpressionKind.FieldReference; }
                }

                public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
                {
                    return visitor.Visit(this, context);
                }
                public override void Visit(IExpressionVisitor visitor)
                {
                    visitor.Visit(this);
                }

                public IEnumFieldMember Member
                {
                    get { return this.owner; }
                }

                public IType MemberType
                {
                    get { return this.Member.Parent; }
                }

                IMember IBoundMemberReference.Member
                {
                    get { return this.Member; }
                }
            }
        }
    }
}
