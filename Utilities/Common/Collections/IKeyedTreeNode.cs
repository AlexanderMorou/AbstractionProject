using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines properties and methods for working with a node within
    /// a keyed tree.
    /// </summary>
    /// <typeparam name="TKey">The kind of type used to represent
    /// the key used to determine the branching lookup condition.</typeparam>
    /// <typeparam name="TValue">The type of value used to represent the
    /// node items.</typeparam>
    /// <typeparam name="TNode">The kind of node used within the current
    /// hierarchy.</typeparam>
    public interface IKeyedTreeNode<TKey, TValue, TNode> :
        IKeyedTree<TKey, TValue, TNode>,
        IControlledKeyedTreeNode<TKey, TValue, TNode>
        where TNode :
            IKeyedTreeNode<TKey, TValue, TNode>,
            new()
    {
        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> which represents
        /// the current <see cref="IKeyedTreeNode{TKey, TValue, TNode}"/>.
        /// </summary>
        new TValue Value { get; set; }
    }
}
