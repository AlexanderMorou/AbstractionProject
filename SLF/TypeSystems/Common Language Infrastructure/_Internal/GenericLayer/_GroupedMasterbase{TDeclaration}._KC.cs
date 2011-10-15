using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GroupedMasterBase<TDeclarationIdentifier, TDeclaration>
    {
        private _KC keysCollection;

        protected override ControlledStateDictionary<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>.KeysCollection InitializeKeysCollection()
        {
            if (this.keysCollection == null)
                this.keysCollection = new _KC(this);
            return keysCollection;
        }

        private class _KC :
            _GroupedMasterBase<TDeclarationIdentifier, TDeclaration>.KeysCollection
        {
            private _GroupedMasterBase<TDeclarationIdentifier, TDeclaration> master;

            public _KC(_GroupedMasterBase<TDeclarationIdentifier, TDeclaration> master)
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

            protected override TDeclarationIdentifier OnGetKey(int index)
            {
                int currentIndexBase = 0;
                foreach (var subordinate in this.master.Subordinates)
                {
                    if (index >= currentIndexBase &&
                        index < currentIndexBase + subordinate.Count)
                    {
                        return (TDeclarationIdentifier)subordinate.Keys[index - currentIndexBase];
                    }
                    currentIndexBase += subordinate.Count;
                }
                throw new ArgumentOutOfRangeException("index");
            }

            public override void CopyTo(TDeclarationIdentifier[] array, int arrayIndex)
            {
                if (array.Length - arrayIndex < this.Count)
                    throw new ArgumentException("array");
                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                CopyToVerified(array, arrayIndex);
            }

            private void CopyToVerified(TDeclarationIdentifier[] array, int arrayIndex = 0)
            {
                var subordinates = this.master.Subordinates.ToArray();
                for (int i = 0, offset = 0; i < subordinates.Length; offset += subordinates[i++].Count)
                    for (int j = 0; j < subordinates[i].Count; j++)
                        array[offset + j] = (TDeclarationIdentifier)subordinates[i].Keys[j];
            }

            public override IEnumerator<TDeclarationIdentifier> GetEnumerator()
            {
                foreach (var subordinate in this.master.Subordinates)
                    foreach (var subordinateKey in subordinate.Keys)
                        yield return (TDeclarationIdentifier)subordinateKey;
            }

            public override TDeclarationIdentifier[] ToArray()
            {
                var result = new TDeclarationIdentifier[this.Count];
                CopyToVerified(result);
                return result;
            }

            public override bool Contains(TDeclarationIdentifier item)
            {
                foreach (var subordinate in this.master.Subordinates)
                    if (subordinate.ContainsKey(item))
                        return true;
                return false;
            }
        }
    }
}
