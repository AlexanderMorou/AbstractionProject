using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    [Serializable]
    public class PropertySignatureMember :
        PropertySignatureMember<ISignatureMemberParentType>,
        IPropertySignatureMember
    {
        public PropertySignatureMember(TypedName nameAndType, ISignatureMemberParentType parentTarget)
            : base(nameAndType, parentTarget)
        {

        }

        public PropertySignatureMember(string name, ISignatureMemberParentType parentTarget)
            : base(name, parentTarget)
        {

        }


        protected override IMemberReferenceExpression OnGetReference(IMemberParentExpression owner)
        {
            return this.GetReference(owner);
        }

    }
}
