using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public class ControlledStateKeyedTree<TKey, TValue, TNode> :
        ControlledStateDictionary<TKey, TNode>,
        IControlledStateKeyedTree<TKey, TValue, TNode>
        where TNode :
            IControlledStateKeyedTreeNode<TKey, TValue, TNode>
    {
        public ControlledStateKeyedTree()
        {
        }
        public ControlledStateKeyedTree(IEnumerable<KeyValuePair<TKey, TNode>> entries)
            : base(entries)
        {
        }
    }
}
