using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines generic properties and methods for working with a locked
    /// linked list.
    /// </summary>
    /// <typeparam name="T">The type of element contained within
    /// the <see cref="ILockedLinkedListNode{T}"/> elements of the list.</typeparam>
    public interface ILockedLinkedList<T> :
        ICountableEnumerable<ILockedLinkedListNode<T>>
    {
        /// <summary>
        /// Returns the first <see cref="ILockedLinkedListNode{T}"/> within the set.
        /// </summary>
        ILockedLinkedListNode<T> First { get; }
        /// <summary>
        /// Returns the first <see cref="ILockedLinkedListNode{T}"/> within the set.
        /// </summary>
        ILockedLinkedListNode<T> Last { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the index of the
        /// <paramref name="node"/> provided.
        /// </summary>
        /// <param name="node">The <see cref="ILockedLinkedListNode{T}"/> to
        /// retrieve the index of.</param>
        /// <returns>A <see cref="Int32"/> value denoting the index of the
        /// <paramref name="node"/> provided.</returns>
        int IndexOf(ILockedLinkedListNode<T> node);
    }
}
