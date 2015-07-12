using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal partial class _GroupedMasterBase<TDeclarationIdentifier, TDeclaration> :
        MasterDictionaryBase<TDeclarationIdentifier, TDeclaration>
        where TDeclarationIdentifier :
            IDeclarationUniqueIdentifier
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

        protected override IEnumerator<KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>> OnGetEnumerator()
        {
            foreach (var subordinate in this.Subordinates)
                for (int i = 0, c = subordinate.Count; i < c; i++)
                    yield return new KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>((TDeclarationIdentifier)subordinate.Keys[i], new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinate.Values[i]));
        }

        protected override KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>> OnGetThis(int index)
        {
            var key = this.Keys[index];
            foreach (var subordinate in this.Subordinates)
                if (subordinate.ContainsKey(key))
                    return new KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>(key, new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinate[key]));
            throw new KeyNotFoundException();
        }

        protected override MasterDictionaryEntry<TDeclaration> OnGetThis(TDeclarationIdentifier key)
        {
            foreach (var subordinate in this.Subordinates)
                if (subordinate.ContainsKey(key))
                    return new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinate[key]);
            throw new KeyNotFoundException();
        }

        public override bool ContainsKey(TDeclarationIdentifier key)
        {
            foreach (var subordinate in this.Subordinates)
                if (subordinate.ContainsKey(key))
                    return true;
            return false;
        }

        protected override void CopyToArray(Array array, int arrayIndex)
        {
            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentOutOfRangeException("array");
            var subordinates = this.Subordinates.ToArray();
            for (int i = 0, offset = arrayIndex; i < subordinates.Length; offset += subordinates[i++].Count)
            {
                var currentSubordinate = subordinates[i];
                for (int j = 0; j < currentSubordinate.Count; j++)
                    array.SetValue(new KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>((TDeclarationIdentifier)currentSubordinate.Keys[j], new MasterDictionaryEntry<TDeclaration>(currentSubordinate, (TDeclaration)currentSubordinate.Values[j])), offset + j);
            }
        }

        /// <summary>
        /// Converts the <see cref="_GroupedMasterBase{TDeclarationIdentifier, TDeclaration}"/>
        /// to an <see cref="Array"/>.
        /// </summary>
        /// <returns>A <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/>
        /// instances which represent the elements of the 
        /// <see cref="_GroupedMasterBase{TDeclarationIdentifier, TDeclaration}"/>.
        /// </returns>
        public override KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>[] ToArray()
        {
            var result = new KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>[this.Count];
            for (int i = 0; i < result.Length; i++)
                result[i] = new KeyValuePair<TDeclarationIdentifier, MasterDictionaryEntry<TDeclaration>>(this.Keys[i], this.Values[i]);
            return result;
        }

    }
}
