using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities
{
    /// <summary>
    /// Defines properties and methods for working with an instance which is cloneable.
    /// </summary>
    /// <typeparam name="T">The type of cloneable object that is to be
    /// duplicated.</typeparam>
    public interface ICloneable<T> :
        ICloneable
        where T :
            ICloneable<T>
    {
        /// <summary>
        /// Creates a new object that is a copy of the current object.
        /// </summary>
        /// <returns>A new <typeparamref name="T"/> instance that is a
        /// copy of the current <see cref="ICloneable{T}"/>.
        /// </returns>
        new T Clone();
    }
}
