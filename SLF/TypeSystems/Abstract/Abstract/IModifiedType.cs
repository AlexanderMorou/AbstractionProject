using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a <see cref="IModifiedType"/>.
    /// </summary>
    public interface IModifiedType :
        IType
    {
        /// <summary>
        /// Returns the <see cref="IReadOnlyCollection{T}"/> which designates the
        /// <see cref="TypeModification"/> instances which modify the current 
        /// <see cref="IModifiedType"/> that must be managed by tools and compilers.
        /// </summary>
        IReadOnlyCollection<TypeModification> Modifiers { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> which designates the
        /// types which modify the current <see cref="IModifiedType"/> that must be managed
        /// by tools and compilers.
        /// </summary>
        IEnumerable<IType> RequiredModifiers { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> which designates the
        /// types which modify the current <see cref="IModifiedType"/> that may be ignored
        /// by tools and compilers.
        /// </summary>
        IEnumerable<IType> OptionalModifiers { get; }
        /// <summary>
        /// Returns the unique identifier for the current
        /// <see cref="IModifiedType"/> in its general case form.
        /// </summary>
        new IModifiedTypeUniqueIdentifier UniqueIdentifier { get; }
    }
}
