using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.TypeSystems.Intermediate
{
    public class ImmutableGenericType<TTypeIdentifier, TType> :
        ImmutableTypeBase<TTypeIdentifier, TType>,
        IGenericType<TTypeIdentifier, TType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier
        where TType :
            IGenericType<TTypeIdentifier, TType>
    {
    }
}
