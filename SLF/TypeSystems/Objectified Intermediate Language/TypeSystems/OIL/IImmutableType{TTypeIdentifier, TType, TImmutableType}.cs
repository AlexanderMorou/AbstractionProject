using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.TypeSystems.Intermediate;

namespace AllenCopeland.Abstraction.Slf.TypeSystems.Intermediate
{
    public interface IImmutableType<TIdentifier, TType, TImmutableType> :
        IImmutableType
    {
    }
}
