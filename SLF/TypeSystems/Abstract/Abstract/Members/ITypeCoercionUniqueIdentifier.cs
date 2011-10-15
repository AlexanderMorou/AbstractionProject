using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface ITypeCoercionUniqueIdentifier :
        IMemberUniqueIdentifier<ITypeCoercionUniqueIdentifier>,
        IGeneralMemberUniqueIdentifier
    {
        /// <summary>
        /// Returns whether the conversion overload is implicit or explicit.
        /// </summary>
        TypeConversionRequirement Requirement { get; }
        /// <summary>
        /// Returns whether the conversion overload is from the containing type or 
        /// to the containing type.
        /// </summary>
        TypeConversionDirection Direction { get; }
        /// <summary>
        /// Returns the type which is coerced by the overload.
        /// </summary>
        IType CoercionType { get; }
    }
}
