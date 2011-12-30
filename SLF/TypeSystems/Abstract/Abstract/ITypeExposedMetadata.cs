using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IExposedTypeMetadata :
        ICustomAttributedDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IType"/> detailed by the
        /// <see cref="ITypeExposedMetadata"/>.
        /// </summary>
        IType DesignatedType { get; }
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
