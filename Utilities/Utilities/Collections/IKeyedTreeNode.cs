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
    public interface IKeyedTreeNode<TKey, TValue, TNode> :
        IKeyedTree<TKey, TValue, TNode>,
        IControlledStateKeyedTreeNode<TKey, TValue, TNode>
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
