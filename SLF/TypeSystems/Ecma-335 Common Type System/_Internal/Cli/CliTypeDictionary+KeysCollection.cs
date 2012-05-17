using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliTypeDictionary
    {
        private class KeysCollection :
            MasterDictionaryBase<IGeneralTypeUniqueIdentifier, IType>.KeysCollection
        {
            private CliTypeDictionary owningDictionary;
            public KeysCollection(CliTypeDictionary owningDictionary)
                : base(owningDictionary)
            {
                this.owningDictionary = owningDictionary;
            }
            public override int Count
            {
                get
                {
                    return this.owningDictionary.Count;
                }
            }

            public override bool Contains(IGeneralTypeUniqueIdentifier item)
            {
                return this.owningDictionary.ContainsKey(item);
            }

            public override IEnumerator<IGeneralTypeUniqueIdentifier> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.owningDictionary.CheckKeyAt(i);
                    yield return this.owningDictionary.uniqueIdentifiers[i];
                }
            }

            public override int IndexOf(IGeneralTypeUniqueIdentifier key)
            {
                return base.IndexOf(key);
            }
        }
    }
}
