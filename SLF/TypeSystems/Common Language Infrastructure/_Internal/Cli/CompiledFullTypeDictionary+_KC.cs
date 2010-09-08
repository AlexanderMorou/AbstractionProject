using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledFullTypeDictionary
    {
        private class _KC :
            CompiledFullTypeDictionary.KeysCollection,
            IDisposable
        {
            private CompiledFullTypeDictionary parent;
            private string[] dataCopy;
            internal _KC(CompiledFullTypeDictionary parent)
                : base(parent)
            {
                this.parent = parent;
                this.dataCopy = new string[this.parent.parent.UnderlyingSystemTypes.Length];
            }

            #region ICollection<string> Members

            public void Add(string item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(string item)
            {
                bool containsUnloaded = false;
                for (int i = 0; i < this.dataCopy.Length; i++)
                    if (this.dataCopy[i] == null)
                    {
                        containsUnloaded = true;
                        continue;
                    }
                    else if (this.dataCopy[i] == item)
                        return true;
                if (containsUnloaded)
                {
                    for (int i = 0; i < this.dataCopy.Length; i++)
                    {
                        CheckItemAt(i);
                        if (this.dataCopy[i] == item)
                            return true;
                    }
                }
                return false;
            }

            private void CheckItemAt(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                this.dataCopy[index] = this.parent.parent.UnderlyingSystemTypes[index].Name;
            }

            public void CopyTo(string[] array, int arrayIndex)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    array[i + arrayIndex] = this.dataCopy[i];
                }
            }

            public int Count
            {
                get { return this.dataCopy.Length; }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(string item)
            {
                throw new NotSupportedException();
            }

            #endregion

            #region IEnumerable<string> Members

            public IEnumerator<string> GetEnumerator()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                {
                    if (dataCopy[i] == null)
                        dataCopy[i] = this.parent.parent.UnderlyingSystemTypes[i].Name;
                    yield return dataCopy[i];
                }
                yield break;
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                if (this.dataCopy != null)
                    this.dataCopy = null;
                this.parent = null;
            }

            #endregion
        }
    }
}
