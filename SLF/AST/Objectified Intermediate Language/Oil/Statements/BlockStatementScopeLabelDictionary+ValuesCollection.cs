using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    partial class BlockStatementScopeLabelDictionary
    {
        private class ValuesCollection :
            IControlledStateCollection<ILabelStatement>
        {
            private BlockStatementScopeLabelDictionary owner;
            public ValuesCollection(BlockStatementScopeLabelDictionary owner)
            {
                this.owner = owner;
            }

            #region IControlledStateCollection<ILabelStatement> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(ILabelStatement item)
            {
                foreach (var element in this.owner.GetLabels())
                    if (element == item)
                        return true;
                return false;
            }

            public void CopyTo(ILabelStatement[] array, int arrayIndex = 0)
            {
                this.owner.GetLabels().ToArray().CopyTo(array, arrayIndex);
            }

            public ILabelStatement this[int index]
            {
                get
                {
                    return this.owner.GetLabels().ElementAt(index);
                }
            }

            public ILabelStatement[] ToArray()
            {
                return this.owner.GetLabels().ToArray();
            }

            public int IndexOf(ILabelStatement element)
            {
                return this.owner.GetLabels().GetIndexOf(element);
            }

            #endregion

            #region IEnumerable<ILabelStatement> Members

            public IEnumerator<ILabelStatement> GetEnumerator()
            {
                return this.owner.GetLabels().GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }
    }
}
