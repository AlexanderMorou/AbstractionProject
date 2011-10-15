using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledTypeDictionary<TTypeIdentifier, TType>
        where TTypeIdentifier :
            ITypeUniqueIdentifier<TTypeIdentifier>,
            IGeneralTypeUniqueIdentifier
        where TType :
            class,
            IType<TTypeIdentifier, TType>
    {
        private class KC :
            KeysCollection,
            IDisposable
        {
            private CompiledTypeDictionary<TTypeIdentifier, TType> parent;
            private TTypeIdentifier[] names;
            public KC(CompiledTypeDictionary<TTypeIdentifier, TType> parent)
                : base(parent)
            {
                this.parent = parent;
                this.names = new TTypeIdentifier[this.parent.Count];
            }
            public override int Count
            {
                get
                {
                    return this.names.Length;
                }
            }
            public override bool Contains(TTypeIdentifier item)
            {
                bool containsUnloaded = false;
                for (int i = 0; i < this.names.Length; i++)
                {
                    if (this.names[i] == null)
                    {
                        containsUnloaded = true;
                        continue;
                    }
                    else if (this.names[i].Equals(item))
                        return true;
                }
                if (containsUnloaded)
                {
                    for (int i = 0; i < this.names.Length; i++)
                    {
                        if (this.names[i] == null)
                        {
                            this.CheckItemAt(i);
                            if (this.names[i].Equals(item))
                                return true;
                        }
                    }
                }
                return false;
            }
            public override void CopyTo(TTypeIdentifier[] array, int arrayIndex)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    array[arrayIndex + i] = this.names[i];
                }
            }

            public override TTypeIdentifier[] ToArray()
            {
                var result = new TTypeIdentifier[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    result[i] = this.names[i];
                }
                return result;
            }

            private void CheckItemAt(int index)
            {
                int i = 0;
                if (this.names[index] == null)
                {
                    foreach (var v in parent.filteredSeries)
                    {
                        if (i == index)
                        {
                            this.names[i] = (TTypeIdentifier)v.GetUniqueIdentifier();
                            break;
                        }
                        i++;
                    }
                }
            }

            public override IEnumerator<TTypeIdentifier> GetEnumerator()
            {
                for (int i = 0; i < this.names.Length; i++)
                {
                    CheckItemAt(i);
                    yield return this.names[i];
                }
                yield break;
            }

            protected override TTypeIdentifier OnGetKey(int index)
            {
                if (index < 0 ||
                    index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                CheckItemAt(index);
                return this.names[index];
            }

            public void Dispose()
            {
                if (this.names != null)
                    this.names = null;
                if (this.parent != null)
                    this.parent = null;
            }
        }

    }
}
