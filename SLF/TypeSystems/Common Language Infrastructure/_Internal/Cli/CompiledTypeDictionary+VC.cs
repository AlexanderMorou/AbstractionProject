using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledTypeDictionary<TType>
        where TType :
            class,
            IType<TType>
    {
        private class VC :
            ControlledStateDictionary<string, TType>.ValuesCollection,
            IDisposable
        {
            private CompiledTypeDictionary<TType> parent;
            internal TType[] values;
            public VC(CompiledTypeDictionary<TType> parent)
                : base(parent)
            {
                this.parent = parent;
                this.values = new TType[this.parent.Count];
            }
            public override int Count
            {
                get
                {
                    return this.values.Length;
                }
            }
            public override bool Contains(TType item)
            {
                bool containsUnloaded = false;
                for (int i = 0; i < this.values.Length; i++)
                {
                    if (this.values[i] == null)
                    {
                        containsUnloaded = true;
                        continue;
                    }
                    else if (this.values[i] == item)
                        return true;
                }
                if (containsUnloaded)
                {
                    for (int i = 0; i < this.values.Length; i++)
                    {
                        if (this.values[i] == null)
                        {
                            this.CheckItemAt(i);
                            if (this.values[i] == item)
                                return true;
                        }
                    }
                }
                return false;
            }

            public override void CopyTo(TType[] array, int arrayIndex)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    array[arrayIndex + i] = this.values[i];
                }
            }

            public override TType[] ToArray()
            {
                var result = new TType[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    result[i] = this.values[i];
                }
                return result;
            }

            private void CheckItemAt(int index)
            {
                int i = 0;
                if (this.values[index] == null)
                {
                    foreach (var v in this.parent.filteredSeries)
                    {
                        if (i == index)
                        {
                            this.values[i] = (TType)(v.GetTypeReference());
                            //Update the master dictionary.
                            //((MasterDictionaryBase<string, IType>)(this.parent.Master)).Subordinate_ItemAdded(this.parent, this.parent.Keys[index], (TType)v);
                            break;
                        }
                        i++;
                    }
                }
            }

            public override IEnumerator<TType> GetEnumerator()
            {
                for (int i = 0; i < this.values.Length; i++)
                {
                    CheckItemAt(i);
                    yield return this.values[i];
                }
                yield break;
            }
            protected override TType OnGetThis(int index)
            {
                if (index < 0 ||
                    index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                CheckItemAt(index);
                return this.values[index];
            }

            public void Dispose()
            {
                if (this.values != null)
                {
                    for (int i = 0; i < this.values.Length; i++)
                    {
                        this.values[i].Dispose();
                        this.values[i] = null;
                    }
                    this.values = null;
                    this.parent = null;
                }
            }
        }
    }
}
