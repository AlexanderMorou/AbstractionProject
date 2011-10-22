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
    /// Defines generic properties and methods for working 
    /// with a node within a controlled state keyed tree.
    /// </summary>
    /// <typeparam name="TKey">The kind of type used to represent
    /// the key used to determine the branching lookup condition.</typeparam>
    /// <typeparam name="TValue">The type of value used to represent the
    /// node items.</typeparam>
    /// <typeparam name="TNode">The kind of node used within the current
    /// hierarchy.</typeparam>
    public interface IControlledStateKeyedTreeNode<TKey, TValue, TNode> :
        IControlledStateKeyedTree<TKey, TValue, TNode>
        where TNode :
            IControlledStateKeyedTreeNode<TKey, TValue, TNode>
    {
        /// <summary>
        /// Returns the <typeparamref name="TValue"/> which represents
        /// the current <see cref="IControlledStateKeyedTreeNode{TKey, TValue, TNode}"/>.
        /// </summary>
        TValue Value { get; }
    }
}
