using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public interface ICountableEnumerable :
        IEnumerable
    {
        /// <summary>
        /// Gets the number of elements contained in the <see cref="ICountableEnumerable"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="ICountableEnumerable"/>.
        /// </returns>
        int Count { get; }
    }

    public interface ICountableEnumerable<out T> :
        IEnumerable<T>,
        ICountableEnumerable
    {
    }
}
