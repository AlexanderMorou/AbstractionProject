using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class LocalMember
    {
        protected class ReferenceExpression :
            MemberParentReferenceExpressionBase,
            ILocalReferenceExpression
        {
            private LocalMember owner;
            public ReferenceExpression(LocalMember owner)
            {
                this.owner = owner;
            }

            protected LocalMember Owner
            {
                get
                {
                    return this.owner;
                }
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

            public override string ToString()
            {
                return this.Name;
            }
        }
    }
}
