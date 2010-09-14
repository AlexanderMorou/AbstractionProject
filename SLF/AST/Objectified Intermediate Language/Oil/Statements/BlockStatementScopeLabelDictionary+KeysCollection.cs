using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    partial class BlockStatementScopeLabelDictionary
    {
        private class KeysCollection :
            IControlledStateCollection<string>
        {
            private BlockStatementScopeLabelDictionary owner;
            public KeysCollection(BlockStatementScopeLabelDictionary owner)
            {
                this.owner = owner;
            }
            #region IControlledStateCollection<string> Members

            public int Count
            {
                get { return this.owner.Count; }
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
                throw new NotImplementedException();
            }

            public string this[int index]
            {
                get { throw new NotImplementedException(); }
            }

            public string[] ToArray()
            {
                throw new NotImplementedException();
            }

            public int IndexOf(string element)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable<string> Members

            public IEnumerator<string> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}
