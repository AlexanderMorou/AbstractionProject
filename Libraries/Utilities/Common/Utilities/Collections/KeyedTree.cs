﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a keyed tree implementation which uses
    /// a simple <see cref="KeyedTreeNode{TKey, TValue}"/>
    /// for the nodes.
    /// </summary>
    /// <typeparam name="TKey">The kind of type used to represent
    /// the key used to determine the branching lookup condition.</typeparam>
    /// <typeparam name="TValue">The type of value used to represent the
    /// node items.</typeparam>
    public class KeyedTree<TKey, TValue> :
        KeyedTree<TKey, TValue, KeyedTreeNode<TKey, TValue>>
    {
        /// <summary>
        /// Creates a new <see cref="KeyedTree{TKey, TValue}"/>
        /// initialized to its default state.
        /// </summary>
        public KeyedTree()
        {
        }

        /// <summary>
        /// Creates a new <see cref="KeyedTreeNode{TKey, TValue}"/>
        /// with a series of <see cref="KeyValuePair{TKey, TValue}"/>
        /// from which to initialize the <see cref="KeyedTree{TKey, TValue}"/>.
        /// </summary>
        /// <param name="entries">A series of <see cref="KeyValuePair{TKey, TValue}"/>
        /// from which to initialize the <see cref="KeyedTree{TKey, TValue}"/></param>
        public KeyedTree(IEnumerable<KeyValuePair<TKey, KeyedTreeNode<TKey, TValue>>> entries)
            : base(entries)
        {
        }
    }

    /// <summary>
    /// Provides a base implementation of a keyed tree structure.
    /// </summary>
    /// <typeparam name="TKey">The kind of type used to represent
    /// the key used to determine the branching lookup condition.</typeparam>
    /// <typeparam name="TValue">The type of value used to represent the
    /// node items.</typeparam>
    /// <typeparam name="TNode">The kind of node used within the current
    /// hierarchy.</typeparam>
    public class KeyedTree<TKey, TValue, TNode> :
        ControlledKeyedTree<TKey, TValue, TNode>,
        IKeyedTree<TKey, TValue, TNode>
        where TNode :
            IKeyedTreeNode<TKey, TValue, TNode>,
            new()
    {
        /// <summary>
        /// Creates a new <see cref="KeyedTree{TKey, TValue, TNode}"/>
        /// initialized to its default state.
        /// </summary>
        public KeyedTree()
        {
        }

        /// <summary>
        /// Creates a new <see cref="KeyedTree{TKey, TValue, TNode}"/>
        /// with a base set of <paramref name="entries"/>
        /// which provide the base set of data.
        /// </summary>
        /// <param name="entries">The <see cref="IEnumerable{T}"/>
        /// of <see cref="KeyValuePair{TKey, TValue}"/> </param>
        public KeyedTree(IEnumerable<KeyValuePair<TKey, TNode>> entries)
            : base(entries)
        {
        }
        #region IKeyedTree<TKey,TValue,TNode> Members

        /// <summary>
        /// Adds a new <typeparamref name="TNode"/> to the
        /// <see cref="KeyedTree{TKey, TValue, TNode}"/>
        /// with the <paramref name="key"/>, and <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to associate to the
        /// <typeparamref name="TNode"/> to create.</param>
        /// <param name="value">The <typeparamref name="TValue"/>
        /// to associate to the new node.</param>
        /// <returns>A new <typeparamref name="TNode"/> instance
        /// which is represented by the <paramref name="key"/>
        /// and <paramref name="value"/> provided.</returns>
        public virtual TNode Add(TKey key, TValue value)
        {
            var result = new TNode();
            result.Value = value;
            this._Add(key, result);
            return result; 
        }

        /// <summary>
        /// Adds a series of <typeparamref name="TNode"/> instances
        /// based off of the <see cref="IEnumerable{T}"/> of
        /// <see cref="KeyValuePair{TKey, TValue}"/> elements.
        /// </summary>
        /// <param name="entries">The series of <see cref="KeyValuePair{TKey, TValue}"/>
        /// series.</param>
        /// <returns>A series of <typeparamref name="TNode"/>
        /// instances relative to the <paramref name="entries"/>
        /// provided.</returns>
        public TNode[] AddRange(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            return (from e in entries
                    select this.Add(e.Key, e.Value)).ToArray();
        }

        /// <summary>
        /// Clears the <see cref="IKeyedTree{TKey, TValue, TNode}"/>.
        /// </summary>
        public void Clear()
        {
            this._Clear();
        }

        /// <summary>
        /// Removes a <typeparamref name="TNode"/> element
        /// based off of the <paramref name="target"/> <typeparamref name="TKey"/> provided.
        /// </summary>
        /// <param name="target">The <typeparamref name="TKey"/>
        /// which represents the identifying key associated to the 
        /// <typeparamref name="TNode"/> to remove.</param>
        /// <returns>true if an element was found and removed; false otherwise.</returns>
        public bool Remove(TKey target)
        {
            return this._Remove(target);
        }

        #endregion

    }
}
