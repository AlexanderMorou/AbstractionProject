using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a base implementation of a 
    /// <see cref="IKeyedTreeNode{TKey, TValue, TNode}"/>
    /// which uses the simple <see cref="KeyedTreeNode{TKey, TValue}"/>
    /// for the nodes.
    /// </summary>
    /// <typeparam name="TKey">The kind of type used to represent
    /// the key used to determine the branching lookup condition.</typeparam>
    /// <typeparam name="TValue">The type of value used to represent the
    /// node items.</typeparam>
    public class KeyedTreeNode<TKey, TValue> :
        KeyedTree<TKey, TValue>,
        IKeyedTreeNode<TKey, TValue, KeyedTreeNode<TKey, TValue>>
    {
        /// <summary>
        /// Creates a new <see cref="KeyedTreeNode{TKey, TValue}"/>
        /// initialized to its default state.
        /// </summary>
        public KeyedTreeNode()
        {
        }

        /// <summary>
        /// Creates a new <see cref="KeyedTreeNode{TKey, TValue}"/>
        /// with a series of 
        /// <see cref="KeyValuePair{TKey, TValue}"/>
        /// from which to initialize the <see cref="KeyedTreeNode{TKey, TValue}"/>.
        /// </summary>
        /// <param name="entries">A series of 
        /// <see cref="KeyValuePair{TKey, TValue}"/>
        /// from which to initialize the <see cref="KeyedTreeNode{TKey, TValue}"/></param>
        public KeyedTreeNode(IEnumerable<KeyValuePair<TKey, KeyedTreeNode<TKey, TValue>>> entries)
            : base(entries)
        {
        }

        #region IKeyedTreeNode<TKey,TValue,KeyedTreeNode<TKey,TValue>> Members

        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> which represents
        /// the current <see cref="KeyedTreeNode{TKey, TValue}"/>.
        /// </summary>
        public TValue Value { get; set; }

        #endregion

    }
}
