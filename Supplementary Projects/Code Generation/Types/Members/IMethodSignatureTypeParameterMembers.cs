using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public interface IMethodSignatureTypeParameterMembers :
        IMethodSignatureTypeParameterMembers<IMethodSignatureParameterMember, IMethodSignatureTypeParameterMember, CodeMemberMethod, ISignatureMemberParentType>
    {
    }
}
