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
    /// <typeparam name="TIdentifier">The specific kind of unique identifier
    /// used for comparison purposes.</typeparam>
    public interface IGenericTypeUniqueIdentifier<TIdentifier> :
        ITypeUniqueIdentifier<TIdentifier>,
        IGenericParamParentUniqueIdentifier<TIdentifier>
        where TIdentifier :
            IGenericTypeUniqueIdentifier<TIdentifier>
    {
    }
}
