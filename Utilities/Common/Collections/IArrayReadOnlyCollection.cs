using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    internal interface IArrayReadOnlyCollection<T> :
        IControlledCollection<T>
    {
        /// <summary>
        /// Returns the <typeparamref name="T"/> array of the 
        /// elements represented by the <see cref="IArrayReadOnlyCollection{T}"/>.
        /// </summary>
        T[] InternalArray { get; }
        /// <summary>
        /// Returns whether the <see cref="IArrayReadOnlyCollection{T}"/> is an aggregation
        /// of other arrays.
        /// </summary>
        bool IsAggregate { get; }
        /// <summary>
        /// Returns the series of <paramref name="T"/> sets which make up the
        /// <see cref="IArrayReadOnlyCollection"/>.
        /// </summary>
        T[][] AggregateArrays { get; }
    }
}
