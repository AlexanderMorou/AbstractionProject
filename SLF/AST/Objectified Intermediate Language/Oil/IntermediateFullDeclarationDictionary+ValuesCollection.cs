﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Linq;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateFullDeclarationDictionary<TDeclaration, TIntermediateDeclaration>
        where TDeclaration :
            class,
            IDeclaration
        where TIntermediateDeclaration :
            class,
            IIntermediateDeclaration,
            TDeclaration
    {
        protected class ValuesCollection : 
            IControlledStateCollection<MasterDictionaryEntry<TIntermediateDeclaration>>,
            IControlledStateCollection
        {
            private IntermediateFullDeclarationDictionary<TDeclaration, TIntermediateDeclaration> owner;

            public ValuesCollection(IntermediateFullDeclarationDictionary<TDeclaration, TIntermediateDeclaration> owner)
            {
                this.owner = owner;
            }
            #region IControlledStateCollection<MasterDictionaryEntry<TIntermediateDeclaration>> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(MasterDictionaryEntry<TIntermediateDeclaration> item)
            {
                return ((MasterDictionaryBase<string, TDeclaration>)(this.owner)).Values.Contains(new MasterDictionaryEntry<TDeclaration>(item.Subordinate, item.Entry));
            }

            public void CopyTo(MasterDictionaryEntry<TIntermediateDeclaration>[] array, int arrayIndex)
            {
                if (arrayIndex + this.Count >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array[i + arrayIndex] = this[i];
            }

            public MasterDictionaryEntry<TIntermediateDeclaration> this[int index]
            {
                get {
                    if (index < 0 || index > this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    var item = ((MasterDictionaryBase<string, TDeclaration>)(this.owner)).Values.ElementAt(index);

                    return new MasterDictionaryEntry<TIntermediateDeclaration>(item.Subordinate, ((TIntermediateDeclaration)(item.Entry)));
                }
            }

            public MasterDictionaryEntry<TIntermediateDeclaration>[] ToArray()
            {
                MasterDictionaryEntry<TIntermediateDeclaration>[] result = new MasterDictionaryEntry<TIntermediateDeclaration>[this.Count];
                for (int i = 0; i < this.Count; i++)
                    result[i] = this[i];
                return result;
            }

            #endregion

            #region IEnumerable<MasterDictionaryEntry<TIntermediateDeclaration>> Members

            public IEnumerator<MasterDictionaryEntry<TIntermediateDeclaration>> GetEnumerator()
            {
                foreach (var item in ((MasterDictionaryBase<string, TDeclaration>)(this.owner)).Values)
                    yield return new MasterDictionaryEntry<TIntermediateDeclaration>(item.Subordinate, ((TIntermediateDeclaration)(item.Entry)));
                yield break;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IControlledStateCollection Members

            bool IControlledStateCollection.Contains(object item)
            {
                if (!(item is MasterDictionaryEntry<TIntermediateDeclaration>))
                    return false;
                return this.Contains(((MasterDictionaryEntry<TIntermediateDeclaration>)(item)));
            }

            void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
            {
                if (arrayIndex + this.Count >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array.SetValue(this[i], i + arrayIndex);
            }

            object IControlledStateCollection.this[int index]
            {
                get { return this[index]; }
            }

            #endregion

            #region ICollection Members


            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return null; }
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (index + this.Count >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array.SetValue(this[i], i + index);
            }

            #endregion
        }
    }
}