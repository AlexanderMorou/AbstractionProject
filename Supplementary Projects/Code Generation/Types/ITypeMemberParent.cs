using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    public interface ITypeMemberParent :
        ITypeParent,
        IMemberParentType,
        IResourceable
    {
    }
}
