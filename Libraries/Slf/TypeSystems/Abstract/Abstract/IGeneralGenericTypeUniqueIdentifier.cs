using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IGeneralGenericTypeUniqueIdentifier :
        IGenericTypeUniqueIdentifier,
        IGeneralTypeUniqueIdentifier,
        IEquatable<IGeneralGenericTypeUniqueIdentifier>
    {
    }
}
