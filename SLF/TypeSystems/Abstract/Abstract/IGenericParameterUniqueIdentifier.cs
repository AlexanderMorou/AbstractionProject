using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IGenericParameterUniqueIdentifier :
        ITypeUniqueIdentifier,
        IMemberUniqueIdentifier,
        IGeneralTypeUniqueIdentifier,
        IGeneralMemberUniqueIdentifier,
        IEquatable<IGenericParameterUniqueIdentifier>
    {
        /// <summary>
        /// The <see cref="Int32"/> index of the generic parameter.
        /// </summary>
        /// <remarks>If null, the name is the proper name of the generic paramter;
        /// if filled the name is !T{Position} (for types) or !!T{Position} (for non-types)
        /// depending on whether the parameter is for a type.</remarks>
        int? Position { get; }
        /// <summary>
        /// Returns whether the generic parameter is defined on
        /// a type.
        /// </summary>
        /// <remarks>Returns true if the generic parameter
        /// is defined upon a type; false, if it is defined upon
        /// a member of some other kind.</remarks>
        bool IsTypeParameter { get; }
    }
}
