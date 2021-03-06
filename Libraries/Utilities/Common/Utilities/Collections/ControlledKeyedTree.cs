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
    /// Provides a base implementation of a controlled state
    /// keyed tree.
    /// </summary>
    /// <typeparam name="TKey">The type of keys within the tree.</typeparam>
    /// <typeparam name="TValue">The types of values, or leaves, within the nodes of the tree.</typeparam>
    /// <typeparam name="TNode">The types of nodes that represent the branches
    /// of the tree.</typeparam>
    public class ControlledKeyedTree<TKey, TValue, TNode> :
        ControlledDictionary<TKey, TNode>,
        IControlledKeyedTree<TKey, TValue, TNode>
        where TNode :
            IControlledKeyedTreeNode<TKey, TValue, TNode>
    {
        /// <summary>
        /// Creates a new <see cref="ControlledKeyedTree{TKey, TValue, TNode}"/>
        /// initialized to a default state.
        /// </summary>
        public ControlledKeyedTree()
        {
        }
        /// <summary>
        /// Creates a new <see cref="ControlledKeyedTree{TKey, TValue, TNode}"/>
        /// initialized to a default state with the <paramref name="entries"/>
        /// to contain by default.
        /// </summary>
        /// <param name="entries">The <see cref="IEnumerable{T}"/>
        /// of key value pairs which represent the initial set of data
        /// to contain within the tree.</param>
        public ControlledKeyedTree(IEnumerable<KeyValuePair<TKey, TNode>> entries)
            : base(entries)
        {
        }
    }
}
