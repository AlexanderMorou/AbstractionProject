using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class LocalMember
    {
        private class ReferenceExpression :
            MemberParentReferenceExpressionBase,
            ILocalReferenceExpression
        {
            private LocalMember owner;
            public ReferenceExpression(LocalMember owner)
            {
                this.owner = owner;
            }

            public override ExpressionKind Type
            {
                get { return ExpressionKinds.LocalReference; }
            }

            public override void Visit(IExpressionVisitor visitor)
            {
                if (visitor == null)
                    throw new ArgumentNullException("visitor");
                visitor.Visit(this);
            }

            #region ILocalReferenceExpression Members

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
        }
    }
}
