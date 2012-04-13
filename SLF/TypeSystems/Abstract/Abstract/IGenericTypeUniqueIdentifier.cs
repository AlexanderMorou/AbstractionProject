using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a generic type unique identifier.
    /// </summary>
    public interface IGenericTypeUniqueIdentifier :
        ITypeUniqueIdentifier,
        IGenericParamParentUniqueIdentifier
    {
    }
}
