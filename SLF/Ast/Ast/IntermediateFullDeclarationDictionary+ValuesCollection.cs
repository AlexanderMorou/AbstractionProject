using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateFullDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier
        where TDeclaration :
            class,
            IDeclaration
        where TIntermediateDeclaration :
            class,
            IIntermediateDeclaration,
            TDeclaration
    {
        protected new class ValuesCollection : 
            IControlledCollection<MasterDictionaryEntry<TIntermediateDeclaration>>,
            IControlledCollection
        {
            private IntermediateFullDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> owner;

            public ValuesCollection(IntermediateFullDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> owner)
            {
                this.owner = owner;
            }
            #region IControlledCollection<MasterDictionaryEntry<TIntermediateDeclaration>> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(MasterDictionaryEntry<TIntermediateDeclaration> item)
            {
                return ((MasterDictionaryBase<TIdentifier, TDeclaration>)(this.owner)).Values.Contains(new MasterDictionaryEntry<TDeclaration>(item.Subordinate, item.Entry));
            }

            public void CopyTo(MasterDictionaryEntry<TIntermediateDeclaration>[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0 || arrayIndex + this.Count >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.Count; i++)
                    array[i + arrayIndex] = this[i];
            }

            public MasterDictionaryEntry<TIntermediateDeclaration> this[int index]
            {
                get {
                    if (index < 0 || index > this.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    var item = ((MasterDictionaryBase<TIdentifier, TDeclaration>)(this.owner)).Values.ElementAt(index);

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

            public int IndexOf(MasterDictionaryEntry<TIntermediateDeclaration> element)
            {
                return ((MasterDictionaryBase<TIdentifier, TDeclaration>)(this.owner)).Values.IndexOf(new MasterDictionaryEntry<TDeclaration>(element.Subordinate, element.Entry));
            }
            #endregion

            #region IEnumerable<MasterDictionaryEntry<TIntermediateDeclaration>> Members

            public IEnumerator<MasterDictionaryEntry<TIntermediateDeclaration>> GetEnumerator()
            {
                foreach (var item in ((MasterDictionaryBase<TIdentifier, TDeclaration>)(this.owner)).Values)
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

            #region IControlledCollection Members

            bool IControlledCollection.Contains(object item)
            {
                if (!(item is MasterDictionaryEntry<TIntermediateDeclaration>))
                    return false;
                return this.Contains(((MasterDictionaryEntry<TIntermediateDeclaration>)(item)));
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex)
            {
                if (arrayIndex<0 || arrayIndex + this.Count >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.Count; i++)
                    array.SetValue(this[i], i + arrayIndex);
            }

            object IControlledCollection.this[int index]
            {
                get { return this[index]; }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is MasterDictionaryEntry<TIntermediateDeclaration>)
                    return this.IndexOf((MasterDictionaryEntry<TIntermediateDeclaration>)element);
                return -1;
            }

            #endregion


        }
    }
}
