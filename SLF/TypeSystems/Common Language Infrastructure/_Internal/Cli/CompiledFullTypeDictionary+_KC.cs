using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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


            public override bool Contains(string item)
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
            public override void CopyTo(string[] array, int arrayIndex = 0)
            {
                if (this.Count == 0)
                    return;
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (this.Count + arrayIndex > array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    array[i + arrayIndex] = this.dataCopy[i];
                }
            }

            protected override void ICollection_CopyTo(Array array, int arrayIndex)
            {
                if (this.Count == 0)
                    return;
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (this.Count + arrayIndex > array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                {
                    CheckItemAt(i);
                    array.SetValue(this.dataCopy[i], i + arrayIndex);
                }
            }

            public override int Count
            {
                get { return this.dataCopy.Length; }
            }

            public override string[] ToArray()
            {
                var result = new string[this.Count];
                this.CopyTo(result);
                return result;
            }

            public override int IndexOf(string key)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.dataCopy[i] == key)
                        return i;
                return -1;
            }

            protected override string OnGetKey(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.dataCopy[index];
            }

            #region IEnumerable<string> Members

            public override IEnumerator<string> GetEnumerator()
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
