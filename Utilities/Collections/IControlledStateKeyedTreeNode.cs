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
    public interface IControlledStateKeyedTreeNode<TKey, TValue, TNode> :
        IControlledStateKeyedTree<TKey, TValue, TNode>
        where TNode :
            IControlledStateKeyedTreeNode<TKey, TValue, TNode>
    {
        /// <summary>
        /// Returns the <typeparamref name="TValue"/> which represents
        /// the current <see cref="IControlledStateKeyedTreeNode"/>.
        /// </summary>
        TValue Value { get; }
    }
}
