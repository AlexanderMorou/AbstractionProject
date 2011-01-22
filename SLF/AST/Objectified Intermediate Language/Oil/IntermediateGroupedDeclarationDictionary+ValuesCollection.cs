using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
                if (this.owner.Suspended)
                {
                    var originalContains = ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values.Contains(item);
                    if (originalContains)
                        return true;
                    if (owner.suspendedMembers.Contains(item))
                        return true;
                    return false;
                }
                else
                    return ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values.Contains(item);
            }

            public void CopyTo(TIntermediateDeclaration[] array, int arrayIndex)
            {
                if ((arrayIndex + this.Count) > array.Length)
                    throw new ArgumentException("array");
                var ownerValuesCollection = ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values;
                IEnumerator<TDeclaration> valuesEnum = ownerValuesCollection.GetEnumerator();

                int index = arrayIndex;
                while (valuesEnum.MoveNext())
                    array[index++] = (TIntermediateDeclaration)valuesEnum.Current;
                if (this.owner.Suspended)
                {
                    valuesEnum = this.owner.suspendedMembers.GetEnumerator();
                    while (valuesEnum.MoveNext())
                        array[index++] = (TIntermediateDeclaration)valuesEnum.Current;
                }
            }

            public TIntermediateDeclaration this[int index]
            {
                get { return ((TIntermediateDeclaration)(((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values[index])); }
            }

            public TIntermediateDeclaration[] ToArray()
            {
                TIntermediateDeclaration[] result = new TIntermediateDeclaration[this.Count];
                this.CopyTo(result, 0);
                return result;
            }

            public int IndexOf(TIntermediateDeclaration element)
            {
                if (this.owner.Suspended)
                {
                    int ownerIndex = ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values.IndexOf(element);
                    if (ownerIndex == -1)
                    {
                        int suspendedIndex = this.owner.suspendedMembers.IndexOf(element);
                        if (suspendedIndex == -1)
                            return -1;
                        return this.owner.BaseCount + suspendedIndex;
                    }
                    return ownerIndex;
                }
                return ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values.IndexOf(element);
            }
            #endregion

            #region IEnumerable<TIntermediateDeclaration> Members

            public IEnumerator<TIntermediateDeclaration> GetEnumerator()
            {
                foreach (var item in ((ControlledStateDictionary<string, TDeclaration>)(this.owner)).Values)
                    yield return ((TIntermediateDeclaration)(item));
                if (owner.Suspended)
                    foreach (var item in this.owner.suspendedMembers)
                        yield return (TIntermediateDeclaration)item;
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
                if (!(array is TIntermediateDeclaration[]))
                    throw new ArgumentException("array");
                this.CopyTo((TIntermediateDeclaration[])array, arrayIndex);
            }

            object IControlledStateCollection.this[int index]
            {
                get { return this[index]; }
            }

            int IControlledStateCollection.IndexOf(object element)
            {
                if (element is TIntermediateDeclaration)
                    return this.IndexOf((TIntermediateDeclaration)(element));
                return -1;
            }
            #endregion

        }
    }
}
