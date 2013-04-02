using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections
{
    public interface IControlledStateKeyedTree<TKey, TValue, TNode> :
        IControlledStateDictionary<TKey, TNode>
        where TNode :
            IControlledStateKeyedTreeNode<TKey, TValue, TNode>
    {
    }
}
