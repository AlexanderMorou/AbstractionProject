using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a unique identifier
    /// which relates to a <see cref="IModifiedType"/>.
    /// </summary>
    public interface IModifiedTypeUniqueIdentifier :
        IGeneralTypeUniqueIdentifier
    {
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> which 
        /// represents the unmodified type identified by the 
        /// <see cref="IModifiedTypeUniqueIdentifier"/>.
        /// </summary>
        ITypeUniqueIdentifier OriginalIdentifier { get; }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> which contains the 
        /// required modifiers which need processed by the run-time.
        /// </summary>
        IEnumerable<IType> RequiredModifiers { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> which contains the 
        /// optional modifiers which can be ignored by the run-time.
        /// </summary>
        IEnumerable<IType> OptionalModifiers { get; }
    }
}
