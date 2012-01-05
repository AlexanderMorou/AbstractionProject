using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public interface IIntermediateModifiersAndAttributesMetadata :
        IIntermediateCustomAttributedEntity,
        IModifiersAndAttributesMetadata
    {
        /// <summary>
        /// Returns a <see cref="ITypeCollection"/>
        /// that relate to the required type modifiers emitted 
        /// by a compiler to enforce language-level type-security.
        /// </summary>
        new ITypeCollection RequiredModifiers { get; }
        /// <summary>
        /// Returns a <see cref="ITypeCollection"/>
        /// that relate to the optional type modifiers emitted 
        /// by a compiler to enforce language-level type-security.
        /// </summary>
        new ITypeCollection OptionalModifiers { get; }
    }
}
