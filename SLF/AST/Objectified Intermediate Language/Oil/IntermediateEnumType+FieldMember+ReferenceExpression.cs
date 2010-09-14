using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateEnumType
    {
        partial class FieldMember
        {
            private class ReferenceExpression :
                MemberParentReferenceExpressionBase,
                IFieldReferenceExpression
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
                    set
                    {
                        this.owner.Name = value;
                    }
                }

                #endregion

                public override ExpressionKind Type
                {
                    get { return ExpressionKinds.FieldReference; }
                }

                public override void Visit(IExpressionVisitor visitor)
                {
                    visitor.Visit(this);
                }
            }
        }
    }
}
