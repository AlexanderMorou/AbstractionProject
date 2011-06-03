using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
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
            IBoundLocalReferenceExpression
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
                get { return ExpressionKind.LocalReference; }
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
            }

            #endregion

            public override string ToString()
            {
                return this.Name;
            }

            #region IBoundMemberReference Members

            IType IBoundMemberReference.MemberType
            {
                get
                {
                    switch (this.owner.TypingMethod)
                    {
                        case LocalTypingKind.Implicit:
                            return this.Owner.InferredType;
                        case LocalTypingKind.Dynamic:
                            return CommonTypeRefs.Object;
                        case LocalTypingKind.Explicit:
                            return ((ITypedLocalMember)this.Owner).LocalType;
                        default:
                            return null;
                    }
                }
            }

            IMember IBoundMemberReference.Member
            {
                get { return this.Owner; }
            }

            #endregion
        }
    }
}
