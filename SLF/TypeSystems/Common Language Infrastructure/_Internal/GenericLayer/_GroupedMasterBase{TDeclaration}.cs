﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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

        public override MasterDictionaryEntry<TDeclaration> this[string key]
        {
            get
            {
                foreach (var subordinate in this.Subordinates)
                    if (subordinate.ContainsKey(key))
                        return new MasterDictionaryEntry<TDeclaration>(subordinate, (TDeclaration)subordinate[key]);
                throw new KeyNotFoundException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }
        public override bool ContainsKey(string key)
        {
            foreach (var subordinate in this.Subordinates)
                if (subordinate.ContainsKey(key))
                    return true;
            return false;
        }

        public override void Clear()
        {
            throw new NotSupportedException();
        }

        public override void Add(string key, MasterDictionaryEntry<TDeclaration> value)
        {
            throw new NotSupportedException();
        }

        public override bool IsReadOnly
        {
            get
            {
                return true;
            }
        }
        protected override bool OnGetIsSynchronized()
        {
            return false;
        }

        public override bool Remove(string key)
        {
            throw new NotSupportedException();
        }

        protected override IDictionaryEnumerator IDictionaryGetEnumerator()
        {
            return new _DictionaryEnumerator(this);
        }

        protected override void ICollection_CopyTo(KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("array");
            var subordinates = this.Subordinates.ToArray();
            for (int i = 0, offset = arrayIndex; i < subordinates.Length; offset += subordinates[i++].Count)
            {
                var currentSubordinate = subordinates[i];
                for (int j = 0; j < currentSubordinate.Count; j++)
                    array[offset + j] = new KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>((string)currentSubordinate.Keys[j], new MasterDictionaryEntry<TDeclaration>(currentSubordinate, (TDeclaration)currentSubordinate.Values[j]));
            }
        }

    }
}