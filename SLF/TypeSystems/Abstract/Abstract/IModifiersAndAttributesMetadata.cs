using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IModifiersAndAttributesMetadata :
        IMetadataEntity
    {
        /// <summary>
        /// Returns a series of <see cref="IType"/> instances
        /// that relate to the required type modifiers emitted 
        /// by a compiler to enforce language-level type-security.
        /// </summary>
        IEnumerable<IType> RequiredModifiers { get; }
        /// <summary>
        /// Returns a series of <see cref="IType"/> instances
        /// that relate to the optional type modifiers emitted 
        /// by a compiler to enforce language-level type-security.
        /// </summary>
        IEnumerable<IType> OptionalModifiers { get; }
    }
}
