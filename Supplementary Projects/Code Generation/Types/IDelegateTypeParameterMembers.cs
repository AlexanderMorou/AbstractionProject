using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with a delegate type's parameters.
    /// </summary>
    public interface IDelegateTypeParameterMembers : 
        IParameteredParameterMembers<IDelegateTypeParameterMember, CodeTypeDelegate, ITypeParent>
    {
    }
}
