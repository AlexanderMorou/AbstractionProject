using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Collections;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledFullTypeDictionary
    {
        private class _VC :
        ICollection<MasterDictionaryEntry<IType>>,
        IDisposable
        {
            private CompiledFullTypeDictionary parent;
            private MasterDictionaryEntry<IType>?[] dataCopy;
            internal _VC(CompiledFullTypeDictionary parent)
            {
                this.parent = parent;
                this.dataCopy = new MasterDictionaryEntry<IType>?[this.parent.parent.UnderlyingSystemTypes.Length];
            }

            #region ICollection<MasterDictionaryEntry<IType>> Members

            public void Add(MasterDictionaryEntry<IType> item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(MasterDictionaryEntry<IType> item)
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
                if (this.dataCopy[index] == null)
                {
                    Type t = this.parent.parent.UnderlyingSystemTypes[index];
                    foreach (ISubordinateDictionary isd in this.parent.Subordinates)
                        if (((ICompiledTypeDictionary)isd).FilteredSeries.Contains(t))
                            this.dataCopy[index] = new MasterDictionaryEntry<IType>(isd, t.GetTypeReference());
                }
            }

            public void CopyTo(MasterDictionaryEntry<IType>[] array, int arrayIndex)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    if (this.dataCopy[i] != null)
                        array[i + arrayIndex] = this.dataCopy[i].Value;
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
            public MasterDictionaryEntry<IType> this[int index]
            {
                get
                {
                    CheckItemAt(index);
                    return this.dataCopy[index].Value;
                }
            }
            public bool Remove(MasterDictionaryEntry<IType> item)
            {
                throw new NotSupportedException();
            }

            #endregion

            #region IEnumerable<MasterDictionaryEntry<IType>> Members

            public IEnumerator<MasterDictionaryEntry<IType>> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    if (this.dataCopy[i] != null)
                        yield return this.dataCopy[i].Value;
                }
                yield break;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
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
