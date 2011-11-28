using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GroupedMasterBase<TDeclarationIdentifier, TDeclaration>
    {
        private _VC valuesCollection;

        protected override ControlledDictionary<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>.ValuesCollection InitializeValuesCollection()
        {
            if (this.valuesCollection == null)
                this.valuesCollection = new _VC(this);
            return this.valuesCollection;
        }

        private class _VC :
            _GroupedMasterBase<TDeclarationIdentifier, TDeclaration>.ValuesCollection
        {
            private _GroupedMasterBase<TDeclarationIdentifier, TDeclaration> master;

            public _VC(_GroupedMasterBase<TDeclarationIdentifier, TDeclaration> master)
                : base(master)
            {
                this.master = master;
            }

            public override int Count
            {
                get
                {
                    return this.master.Count;
                }
            }
            protected override MasterDictionaryEntry<TDeclaration> OnGetValue(int index)
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

            public override void CopyTo(MasterDictionaryEntry<TDeclaration>[] array, int arrayIndex)
            {
                if (array.Length - arrayIndex < this.Count)
                    throw new ArgumentOutOfRangeException("arrayIndex");
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
