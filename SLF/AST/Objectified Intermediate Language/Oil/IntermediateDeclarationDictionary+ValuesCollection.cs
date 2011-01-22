using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using System.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration>
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            IIntermediateDeclaration,
            TDeclaration
    {
        /// <summary>
        /// The values collection used by the 
        /// <see cref="IntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>
        /// to mask the original <typeparamref name="TDeclaration"/> set of values
        /// with the more accurate <typeparamref name="TIntermediateDeclaration"/>.
        /// </summary>
        new protected class ValuesCollection :
            IControlledStateCollection<TIntermediateDeclaration>,
            IControlledStateCollection
        {
            private IntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration> owner;
            public ValuesCollection(IntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration> owner)
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
                var valueEnum = this.owner.Values.GetEnumerator();
                int index = 0;
                while (valueEnum.MoveNext())
                    array[index++ + arrayIndex] = (TIntermediateDeclaration)valueEnum.Current;
                //for (int i = 0; i < this.Count; i++)
                //    array[i + arrayIndex] = this[i];
            }

            public TIntermediateDeclaration this[int index]
            {
                get { return ((TIntermediateDeclaration)(((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values[index])); }
            }

            public TIntermediateDeclaration[] ToArray()
            {
                TIntermediateDeclaration[] result = new TIntermediateDeclaration[this.Count];
                var valueEnum = this.owner.Values.GetEnumerator();
                var index = 0;
                while (valueEnum.MoveNext())
                    result[index++] = (TIntermediateDeclaration)valueEnum.Current;
                return result;
            }

            public int IndexOf(TIntermediateDeclaration element)
            {
                return ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values.IndexOf(element);
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

            int IControlledStateCollection.IndexOf(object element)
            {
                if (element is TIntermediateDeclaration)
                    return this.IndexOf((TIntermediateDeclaration)element);
                return -1;
            }
            #endregion

        }
    }
}
