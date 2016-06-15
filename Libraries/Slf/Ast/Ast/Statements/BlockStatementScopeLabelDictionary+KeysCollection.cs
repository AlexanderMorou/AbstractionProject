using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    partial class BlockStatementScopeLabelDictionary
    {
        private class KeysCollection :
            IControlledCollection<string>
        {
            private BlockStatementScopeLabelDictionary owner;
            public KeysCollection(BlockStatementScopeLabelDictionary owner)
            {
                this.owner = owner;
            }
            #region IControlledCollection<string> Members

            public int Count
            {
                get { return this.owner.Count; }
            }
            private IEnumerable<string> GetLabelNames()
            {
                return from label in this.owner.GetLabels()
                       select label.Name;
            }
            public bool Contains(string item)
            {
                foreach (var element in this.owner.GetLabels())
                    if (element.Name == item)
                        return true;
                return false;
            }

            public void CopyTo(string[] array, int arrayIndex = 0)
            {
                GetLabelNames().ToArray().CopyTo(array, arrayIndex);
            }

            public string this[int index]
            {
                get { return this.GetLabelNames().ElementAt(index); }
            }

            public string[] ToArray()
            {
                return GetLabelNames().ToArray();
            }

            public int IndexOf(string element)
            {
                return GetLabelNames().GetIndexOf(element);
            }

            #endregion

            #region IEnumerable<string> Members

            public IEnumerator<string> GetEnumerator()
            {
                return this.GetLabelNames().GetEnumerator();
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
