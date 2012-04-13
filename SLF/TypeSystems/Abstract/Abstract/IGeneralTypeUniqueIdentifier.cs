using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// general type's unique identifier which is
    /// either a nested type or a top-level type, but not
    /// a generic parameter.
    /// </summary>
    public interface IGeneralTypeUniqueIdentifier :
        ITypeUniqueIdentifier,
        IGeneralDeclarationUniqueIdentifier,
        IEquatable<IGeneralTypeUniqueIdentifier>
    {
        /// <summary>
        /// Returns the <see cref="IAssemblyUniqueIdentifier"/>
        /// which identifies the assembly from which the type is derived.
        /// </summary>
        IAssemblyUniqueIdentifier Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralDeclarationUniqueIdentifier"/> which
        /// designates the namespace from which the <see cref="IType"/> the 
        /// <see cref="IGeneralTypeUniqueIdentifier"/> is derived.
        /// </summary>
        IGeneralDeclarationUniqueIdentifier Namespace { get; }
    }
}
