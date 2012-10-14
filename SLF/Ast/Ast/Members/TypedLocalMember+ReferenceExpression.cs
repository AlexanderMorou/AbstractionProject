using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class TypedLocalMember
    {
        protected new class ReferenceExpression :
            LocalMember.ReferenceExpression
        {
            
            public ReferenceExpression(TypedLocalMember owner)
                : base(owner)
            {

            }

            protected new TypedLocalMember Owner
            {
                get
                {
                    return (TypedLocalMember)base.Owner;
                }
            }

            protected override IType TypeLookupAid
            {
                get
                {
                    if (this.Owner != null &&
                        this.Owner.LocalType != null &&
                       !this.Owner.LocalType.Equals(Owner.Parent.IdentityManager.ObtainTypeReference(Owner.Parent.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(RuntimeCoreType.VoidType))))
                        return this.Owner.LocalType;
                    return null;
                }
            }
        }
    }
}
