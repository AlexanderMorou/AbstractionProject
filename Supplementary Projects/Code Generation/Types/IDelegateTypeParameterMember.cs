using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.Reflection;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with a parameter of a delegate type.
    /// </summary>
    public interface IDelegateTypeParameterMember :
        IParameteredParameterMember<IDelegateTypeParameterMember, CodeTypeDelegate, ITypeParent>//,
        //IFauxableReliant<ParameterInfo, Type>
    {
    }
}
