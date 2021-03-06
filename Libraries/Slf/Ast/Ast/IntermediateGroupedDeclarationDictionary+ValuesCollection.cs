﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateGroupedDeclarationDictionary<TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration>
        where TDeclarationIdentifier :
            TMDeclarationIdentifier,
            IDeclarationUniqueIdentifier
        where TMDeclarationIdentifier :
            IDeclarationUniqueIdentifier
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
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// to mask the original <typeparamref name="TDeclaration"/> set of values
        /// with the more accurate <typeparamref name="TIntermediateDeclaration"/>.
        /// </summary>
        new protected class ValuesCollection :
            IControlledCollection<TIntermediateDeclaration>,
            IControlledCollection
        {
            private IntermediateGroupedDeclarationDictionary<TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration> owner;
            public ValuesCollection(IntermediateGroupedDeclarationDictionary<TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration> owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<TIntermediateDeclaration> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(TIntermediateDeclaration item)
            {
                if (this.owner.Suspended)
                {
                    var originalContains = ((ControlledDictionary<TDeclarationIdentifier, TDeclaration>)(this.owner)).Values.Contains(item);
                    if (originalContains)
                        return true;
                    if (owner.suspendedMembers.Contains(item))
                        return true;
                    return false;
                }
                else
                    return ((ControlledDictionary<TDeclarationIdentifier, TDeclaration>)(this.owner)).Values.Contains(item);
            }

            public void CopyTo(TIntermediateDeclaration[] array, int arrayIndex = 0)
            {
                if ((arrayIndex + this.Count) > array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                var ownerValuesCollection = ((ControlledDictionary<TDeclarationIdentifier, TDeclaration>)(this.owner)).Values;
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
                get { return ((TIntermediateDeclaration)(((ControlledDictionary<TDeclarationIdentifier, TDeclaration>)(this.owner)).Values[index])); }
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
                    int ownerIndex = ((ControlledDictionary<TDeclarationIdentifier, TDeclaration>)(this.owner)).Values.IndexOf(element);
                    if (ownerIndex == -1)
                    {
                        int suspendedIndex = this.owner.suspendedMembers.IndexOf(element);
                        if (suspendedIndex == -1)
                            return -1;
                        return this.owner.BaseCount + suspendedIndex;
                    }
                    return ownerIndex;
                }
                return ((ControlledDictionary<TDeclarationIdentifier, TDeclaration>)(this.owner)).Values.IndexOf(element);
            }
            #endregion

            #region IEnumerable<TIntermediateDeclaration> Members

            public IEnumerator<TIntermediateDeclaration> GetEnumerator()
            {
                foreach (var item in ((ControlledDictionary<TDeclarationIdentifier, TDeclaration>)(this.owner)).Values)
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

            #region IControlledCollection Members

            bool IControlledCollection.Contains(object item)
            {
                if (item is TIntermediateDeclaration)
                    return this.Contains(((TIntermediateDeclaration)(item)));
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex)
            {
                if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (!(array is TIntermediateDeclaration[]))
                    throw new ArgumentException("array");
                this.CopyTo((TIntermediateDeclaration[])array, arrayIndex);
            }

            object IControlledCollection.this[int index]
            {
                get { return this[index]; }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is TIntermediateDeclaration)
                    return this.IndexOf((TIntermediateDeclaration)(element));
                return -1;
            }
            #endregion

        }
    }
}
