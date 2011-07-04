using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public class ControlledStateKeyedTreeNode<TKey, TValue, TNode> :
        ControlledStateKeyedTree<TKey, TValue, TNode>,
        IControlledStateKeyedTreeNode<TKey, TValue, TNode>
        where TNode :
            ControlledStateKeyedTreeNode<TKey, TValue, TNode>
    {
        public ControlledStateKeyedTreeNode()
        {
        }
        public ControlledStateKeyedTreeNode(IEnumerable<KeyValuePair<TKey, TNode>> entries)
            : base(entries)
        {
        }

        #region IControlledStateKeyedTreeNode<TKey,TValue,TNode> Members

        public TValue Value { get; protected set; }

        #endregion

    }
}
