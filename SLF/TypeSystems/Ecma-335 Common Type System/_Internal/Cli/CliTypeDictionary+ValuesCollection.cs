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
        private class ValuesCollection :
            MasterDictionaryBase<IGeneralTypeUniqueIdentifier, IType>.ValuesCollection
        {
            private CliTypeDictionary owningDictionary;
            public ValuesCollection(CliTypeDictionary owningDictionary)
                : base(owningDictionary)
            {
                this.owningDictionary = owningDictionary;
            }
        }
    }
}
