using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// A constuctor parameter member.
    /// </summary>
    [Serializable]
    internal class ConstructorParameterMember :
        ParameteredParameterMember<IConstructorParameterMember, CodeConstructor, IMemberParentType>,
        IConstructorParameterMember
    {
        public ConstructorParameterMember(TypedName nameAndType, IConstructorMember parentTarget)
            :base(nameAndType, parentTarget)
        {
        }

    }
}
