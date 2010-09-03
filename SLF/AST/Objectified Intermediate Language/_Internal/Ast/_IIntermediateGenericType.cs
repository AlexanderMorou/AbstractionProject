using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    interface _IIntermediateGenericType
    {
        void ItemAdded(IGenericParameter parameter);
        void ItemRemoved(IGenericParameter parameter);
        void Rearranged(int from, int to);
    }
}
