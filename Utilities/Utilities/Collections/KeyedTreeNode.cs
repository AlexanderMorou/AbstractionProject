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
    public class KeyedTreeNode<TKey, TValue> :
        KeyedTree<TKey, TValue>,
        IKeyedTreeNode<TKey, TValue, KeyedTreeNode<TKey, TValue>>
    {
        public KeyedTreeNode()
        {
        }
        public KeyedTreeNode(IEnumerable<KeyValuePair<TKey, KeyedTreeNode<TKey, TValue>>> entries)
            : base(entries)
        {
        }

        #region IKeyedTreeNode<TKey,TValue,KeyedTreeNode<TKey,TValue>> Members

        public TValue Value { get; set; }

        #endregion

    }
}
