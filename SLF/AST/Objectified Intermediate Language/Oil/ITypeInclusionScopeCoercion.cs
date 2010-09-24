using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for coercing a scope by including
    /// a type by its name.
    /// </summary>
    public interface ITypeInclusionScopeCoercion :
        IScopeCoercion
    {
        /// <summary>
        /// Returns the <see cref="IType"/> associated to the
        /// type inclusion to the active scope.
        /// </summary>
        IType IncludedType { get; set; }
    }
}
