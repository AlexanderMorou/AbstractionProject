﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    partial class IntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            IIntermediateDeclaration<TIdentifier>,
            TDeclaration
    {
        /// <summary>
        /// The values collection used by the 
        /// <see cref="IntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>
        /// to mask the original <typeparamref name="TDeclaration"/> set of values
        /// with the more accurate <typeparamref name="TIntermediateDeclaration"/>.
        /// </summary>
        new protected class ValuesCollection :
            IControlledCollection<TIntermediateDeclaration>,
            IControlledCollection
        {
            private IntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> owner;
            public ValuesCollection(IntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> owner)
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
                return ((ControlledDictionary<TIdentifier, TDeclaration>)(this.owner)).Values.Contains(item);
            }

            public void CopyTo(TIntermediateDeclaration[] array, int arrayIndex = 0)
            {
                if ((arrayIndex + this.Count) >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                var valueEnum = this.owner.Values.GetEnumerator();
                int index = 0;
                while (valueEnum.MoveNext())
                    array[index++ + arrayIndex] = (TIntermediateDeclaration)valueEnum.Current;
                //for (int i = 0; i < this.Count; i++)
                //    array[i + arrayIndex] = this[i];
            }

            public TIntermediateDeclaration this[int index]
            {
                get { return ((TIntermediateDeclaration)(((ControlledDictionary<TIdentifier, TDeclaration>)(this.owner)).Values[index])); }
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
                return ((ControlledDictionary<TIdentifier, TDeclaration>)(this.owner)).Values.IndexOf(element);
            }

            #endregion

            #region IEnumerable<TIntermediateDeclaration> Members

            public IEnumerator<TIntermediateDeclaration> GetEnumerator()
            {
                foreach (var item in ((ControlledDictionary<TIdentifier, TDeclaration>)(this.owner)).Values)
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

            #region IControlledCollection Members

            bool IControlledCollection.Contains(object item)
            {
                if (item is TIntermediateDeclaration)
                    return this.Contains(((TIntermediateDeclaration)(item)));
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex)
            {
                if ((arrayIndex + this.Count) >= array.Length)
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
                if (element is TIntermediateDeclaration)
                    return this.IndexOf((TIntermediateDeclaration)element);
                return -1;
            }
            #endregion

        }
    }
}
