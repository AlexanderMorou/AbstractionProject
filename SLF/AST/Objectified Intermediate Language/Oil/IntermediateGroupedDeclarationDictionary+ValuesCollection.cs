using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGroupedDeclarationDictionary<TDeclaration, TMDeclaration, TIntermediateDeclaration>
        where TDeclaration :
            TMDeclaration
        where TMDeclaration :
            class,
            IDeclaration
        where TIntermediateDeclaration :
            IIntermediateDeclaration,
            TDeclaration
    {
        /// <summary>
        /// The values collection used by the 
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>
        /// to mask the original <typeparamref name="TDeclaration"/> set of values
        /// with the more accurate <typeparamref name="TIntermediateDeclaration"/>.
        /// </summary>
        new protected class ValuesCollection :
            IControlledStateCollection<TIntermediateDeclaration>,
            IControlledStateCollection
        {
            private IntermediateGroupedDeclarationDictionary<TDeclaration, TMDeclaration, TIntermediateDeclaration> owner;
            public ValuesCollection(IntermediateGroupedDeclarationDictionary<TDeclaration, TMDeclaration, TIntermediateDeclaration> owner)
            {
                this.owner = owner;
            }

            #region IControlledStateCollection<TIntermediateDeclaration> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(TIntermediateDeclaration item)
            {
                return ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values.Contains(item);
            }

            public void CopyTo(TIntermediateDeclaration[] array, int arrayIndex)
            {
                if ((arrayIndex + this.Count) >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array[i + arrayIndex] = this[i];
            }

            public TIntermediateDeclaration this[int index]
            {
                get { return ((TIntermediateDeclaration)(((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values[index])); }
            }

            public TIntermediateDeclaration[] ToArray()
            {
                TIntermediateDeclaration[] result = new TIntermediateDeclaration[this.Count];
                for (int i = 0; i < this.Count; i++)
                    result[i] = this[i];
                return result;
            }

            #endregion

            #region IEnumerable<TIntermediateDeclaration> Members

            public IEnumerator<TIntermediateDeclaration> GetEnumerator()
            {
                foreach (var item in ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values)
                    yield return ((TIntermediateDeclaration)(item));
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
                if (item is TIntermediateDeclaration)
                    return this.Contains(((TIntermediateDeclaration)(item)));
                return false;
            }

            void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
            {
                if ((arrayIndex + this.Count) >= array.Length)
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

            void ICollection.CopyTo(Array array, int arrayIndex)
            {
                if ((arrayIndex + this.Count) >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array.SetValue(this[i], i + arrayIndex);
            }

            #endregion
        }
    }
}
