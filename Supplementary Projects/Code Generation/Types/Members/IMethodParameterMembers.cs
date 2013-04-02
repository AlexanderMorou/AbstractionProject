using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// Defines properties and methods for working with a method's 
    /// parameters.
    /// </summary>
    public interface IMethodParameterMembers :
        IParameteredParameterMembers<IMethodParameterMember, CodeMemberMethod, IMemberParentType>
    {
    }
}
