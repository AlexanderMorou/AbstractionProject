using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GroupedMasterBase<TDeclaration>
    {
        private _VC valuesCollection;

        protected override ICollection<MasterDictionaryEntry<TDeclaration>> GetValues()
        {
            if (this.valuesCollection == null)
                this.valuesCollection = new _VC(this);
            return this.valuesCollection;
        }

        private class _VC :
            ReadOnlyCollection<MasterDictionaryEntry<TDeclaration>>,
            ICollection<MasterDictionaryEntry<TDeclaration>>
        {
            private _GroupedMasterBase<TDeclaration> master;

            public _VC(_GroupedMasterBase<TDeclaration> master)
            {
                this.master = master;
            }

            #region ICollection<MasterDictionaryEntry<TDeclaration>> Members

            public void Add(MasterDictionaryEntry<TDeclaration> item)
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

            public bool Remove(MasterDictionaryEntry<TDeclaration> item)
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

            public override MasterDictionaryEntry<TDeclaration> this[int index]
            {
                get
                {
                    int currentIndexBase = 0;
                    foreach (var subordinate in this.master.Subordinates)
                    {
                        if (index >= currentIndexBase &&
                            index < currentIndexBase + subordinate.Count)
                        {
                            return new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinate.Values[index - currentIndexBase]);
                        }
                        currentIndexBase += subordinate.Count;
                    }
                    throw new ArgumentOutOfRangeException("index");
                }
            }

            public override void CopyTo(MasterDictionaryEntry<TDeclaration>[] array, int arrayIndex)
            {
                if (array.Length - arrayIndex < this.Count)
                    throw new ArgumentException("array");
                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                CopyToVerified(array, arrayIndex);
            }

            private void CopyToVerified(MasterDictionaryEntry<TDeclaration>[] array, int arrayIndex = 0)
            {
                var subordinates = this.master.Subordinates.ToArray();
                for (int i = 0, offset = arrayIndex; i < subordinates.Length; offset += subordinates[i++].Count)
                    for (int j = 0; j < subordinates[i].Count; j++)
                        array[offset + j] = new MasterDictionaryEntry<TDeclaration>(subordinates[i], (TDeclaration)subordinates[i].Values[j]);
            }

            public override IEnumerator<MasterDictionaryEntry<TDeclaration>> GetEnumerator()
            {
                foreach (var subordinate in this.master.Subordinates)
                    foreach (var subordinateValue in subordinate.Values)
                        yield return new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinateValue);
            }

            public override MasterDictionaryEntry<TDeclaration>[] ToArray()
            {
                MasterDictionaryEntry<TDeclaration>[] result = new MasterDictionaryEntry<TDeclaration>[this.Count];
                CopyToVerified(result);
                return result;
            }

            public override bool Contains(MasterDictionaryEntry<TDeclaration> item)
            {
                if (this.master.Subordinates.Contains(item.Subordinate))
                    return item.Subordinate.Values.Contains(item.Entry);
                return false;
            }
        }
    }
}
