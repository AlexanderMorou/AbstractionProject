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
    /// Defines properties and methods for working with a controlled state
    /// keyed tree.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    public class ControlledKeyedTreeNode<TKey, TValue, TNode> :
        ControlledKeyedTree<TKey, TValue, TNode>,
        IControlledKeyedTreeNode<TKey, TValue, TNode>
        where TNode :
            ControlledKeyedTreeNode<TKey, TValue, TNode>
    {
        public ControlledKeyedTreeNode()
        {
        }
        public ControlledKeyedTreeNode(IEnumerable<KeyValuePair<TKey, TNode>> entries)
            : base(entries)
        {
        }

        #region IControlledKeyedTreeNode<TKey,TValue,TNode> Members

        public TValue Value { get; protected set; }

        #endregion

    }
}
