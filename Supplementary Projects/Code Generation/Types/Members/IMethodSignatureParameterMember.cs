using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Reflection;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public interface IMethodSignatureParameterMember :
        IParameteredParameterMember<IMethodSignatureParameterMember, CodeMemberMethod, ISignatureMemberParentType>//,
        //IFauxableReliant<ParameterInfo, MethodInfo>
    {
    }
}
