using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledFullTypeDictionary
    {
        private class _VC :
            CompiledFullTypeDictionary.ValuesCollection,
            IDisposable
        {
            private CompiledFullTypeDictionary parent;
            private MasterDictionaryEntry<IType>?[] dataCopy;
            internal _VC(CompiledFullTypeDictionary parent)
                : base(parent)
            {
                this.parent = parent;
                this.dataCopy = new MasterDictionaryEntry<IType>?[this.parent.parent.UnderlyingSystemTypes.Length];
            }

            #region ICollection<MasterDictionaryEntry<IType>> Members
            public override MasterDictionaryEntry<IType>[] ToArray()
            {
                MasterDictionaryEntry<IType>[] result = new MasterDictionaryEntry<IType>[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    if (this.dataCopy[i].HasValue)
                        result[i] = this.dataCopy[i].Value;
                }
                return result;
            }


            public override bool Contains(MasterDictionaryEntry<IType> item)
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
            public override void CopyTo(MasterDictionaryEntry<IType>[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (this.Count + arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    if (this.dataCopy[i].HasValue)
                        array[i + arrayIndex] = this.dataCopy[i].Value;
                } 
            }

            protected override void GeneralCopyTo(Array array, int arrayIndex)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (this.Count + arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    if (this.dataCopy[i].HasValue)
                        array.SetValue(this.dataCopy[i].Value, i + arrayIndex);
                }
            }

            public override int Count
            {
                get { return this.dataCopy.Length; }
            }
            protected override MasterDictionaryEntry<IType> OnGetValue(int index)
            {
                CheckItemAt(index);
                return this.dataCopy[index].Value;
            }

            #endregion

            #region IEnumerable<MasterDictionaryEntry<IType>> Members

            public override IEnumerator<MasterDictionaryEntry<IType>> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    if (this.dataCopy[i].HasValue)
                        yield return this.dataCopy[i].Value;
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
