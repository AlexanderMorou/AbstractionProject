using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GroupedMasterBase<TDeclaration>
    {
        private _KC keysCollection;
        protected override ICollection<string> GetKeys()
        {
            if (this.keysCollection == null)
                this.keysCollection = new _KC(this);
            return this.keysCollection;
        }
        private class _KC :
            ReadOnlyCollection<string>,
            ICollection<string>
        {
            private _GroupedMasterBase<TDeclaration> master;

            public _KC(_GroupedMasterBase<TDeclaration> master)
            {
                this.master = master;
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

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(string item)
            {
                throw new NotSupportedException();
            }

            #endregion 

            public override int Count
            {
                get
                {
                    return this.master.Count;
                }
            }

            public override string this[int index]
            {
                get
                {
                    int currentIndexBase = 0;
                    foreach (var subordinate in this.master.Subordinates)
                    {
                        if (index >= currentIndexBase &&
                            index < currentIndexBase + subordinate.Count)
                        {
                            return (string)subordinate.Keys[index - currentIndexBase];
                        }
                        currentIndexBase += subordinate.Count;
                    }
                    throw new ArgumentOutOfRangeException("index");
                }
            }

            public override void CopyTo(string[] array, int arrayIndex)
            {
                if (array.Length - arrayIndex < this.Count)
                    throw new ArgumentException("array");
                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                CopyToVerified(array, arrayIndex);
            }

            private void CopyToVerified(string[] array, int arrayIndex = 0)
            {
                var subordinates = this.master.Subordinates.ToArray();
                for (int i = 0, offset = 0; i < subordinates.Length; offset += subordinates[i++].Count)
                    for (int j = 0; j < subordinates[i].Count; j++)
                        array[offset + j] = (string)subordinates[i].Keys[j];
            }

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var subordinate in this.master.Subordinates)
                    foreach (var subordinateKey in subordinate.Keys)
                        yield return (string)subordinateKey;
            }

            public override string[] ToArray()
            {
                string[] result = new string[this.Count];
                CopyToVerified(result);
                return result;
            }

            public override bool Contains(string item)
            {
                foreach (var subordinate in this.master.Subordinates)
                    if (subordinate.ContainsKey(item))
                        return true;
                return false;
            }
        }
    }
}
