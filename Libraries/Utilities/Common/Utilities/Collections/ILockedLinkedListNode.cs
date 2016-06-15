using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines properties and methods for working with a locked linked
    /// list node.
    /// </summary>
    /// <typeparam name="T">The type of element represented by the
    /// locked linked list node.</typeparam>
    public interface ILockedLinkedListNode<T>
    {
        /// <summary>
        /// Returns the first element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> First { get; }
        /// <summary>
        /// Returns the last element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> Last { get; }
        /// <summary>
        /// Returns the previous element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> Previous { get; }
        /// <summary>
        /// Returns the next element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> Next { get; }
        /// <summary>
        /// Returns the <typeparamref name="T"/> instance which
        /// is represented by the <see cref="ILockedLinkedListNode{T}"/>.
        /// </summary>
        T Element { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> index within the set 
        /// the <see cref="ILockedLinkedListNode{T}"/> is at.
        /// </summary>
        int Index { get; }
    }
}
