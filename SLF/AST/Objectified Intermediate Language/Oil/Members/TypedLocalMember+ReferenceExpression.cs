using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
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
                       !this.Owner.LocalType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                        return this.Owner.LocalType;
                    return null;
                }
            }
        }
    }
}
