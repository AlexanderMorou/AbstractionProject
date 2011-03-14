using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal partial class _GroupedMasterBase<TDeclaration> :
        MasterDictionaryBase<string, TDeclaration>
        where TDeclaration :
            class,
            IDeclaration
    {
        internal _GroupedMasterBase()
            : base()
        {
        }
        public override int Count
        {
            get
            {
                int count = 0;
                foreach (var subordinate in this.Subordinates)
                {
                    if (subordinate == null)
                        continue;
                    count += subordinate.Count;
                }
                return count;
            }
        }

        public override IEnumerator<KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>> GetEnumerator()
        {
            foreach (var subordinate in this.Subordinates)
                foreach (var element in subordinate.Count.Range())
                    yield return new KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>((string)subordinate.Keys[element], new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinate.Values[element]));
        }

        protected override KeyValuePair<string, MasterDictionaryEntry<TDeclaration>> OnGetThis(int index)
        {
            var key = this.Keys[index];
            foreach (var subordinate in this.Subordinates)
                if (subordinate.ContainsKey(key))
                    return new KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>(key, new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinate[key]));
            throw new KeyNotFoundException();
        }

        public override bool ContainsKey(string key)
        {
            foreach (var subordinate in this.Subordinates)
                if (subordinate.ContainsKey(key))
                    return true;
            return false;
        }

        protected override void ICollection_CopyTo(Array array, int arrayIndex)
        {
            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("array");
            var subordinates = this.Subordinates.ToArray();
            for (int i = 0, offset = arrayIndex; i < subordinates.Length; offset += subordinates[i++].Count)
            {
                var currentSubordinate = subordinates[i];
                for (int j = 0; j < currentSubordinate.Count; j++)
                    array.SetValue(new KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>((string)currentSubordinate.Keys[j], new MasterDictionaryEntry<TDeclaration>(currentSubordinate, (TDeclaration)currentSubordinate.Values[j])), offset + j);
            }
        }

    }
}
