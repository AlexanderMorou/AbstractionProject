using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{

    internal interface ITypeModifierSetEntry :
        IEquatable<ITypeModifierSetEntry>,
        IEnumerable<TypeModification>
    {
        int Count { get; }
        IEnumerable<bool> EnumerateRequirementState();
        IEnumerable<IType> EnumerateModifierTypes();

    }
}
