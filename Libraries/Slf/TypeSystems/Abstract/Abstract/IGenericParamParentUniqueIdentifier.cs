using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IGenericParamParentUniqueIdentifier
    {
        /// <summary>
        /// Returns whether the type represented by the
        /// <see cref="IGenericTypeUniqueIdentifier"/>
        /// is generic or not.
        /// </summary>
        bool IsGenericConstruct { get; }
        /// <summary>
        /// Returns the number of type-parameters represented
        /// by the unique identifier of the generic type.
        /// </summary>
        int TypeParameters { get; }
    }
}
