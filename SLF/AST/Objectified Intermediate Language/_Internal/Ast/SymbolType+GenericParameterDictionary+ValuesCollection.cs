﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using TValueMember = AllenCopeland.Abstraction.Slf.Abstract.IGenericTypeParameter<AllenCopeland.Abstraction.Slf.Oil.ISymbolType> ;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    partial class SymbolType
    {
        partial class GenericParameterDictionary
        {
            private class ValuesCollection :
                IControlledStateCollection<TValueMember>,
                IControlledStateCollection
            {
                private GenericParameterDictionary container;
                internal ValuesCollection(GenericParameterDictionary container)
                {
                    this.container = container;
                }

                #region IControlledStateCollection Members

                bool IControlledStateCollection.Contains(object item)
                {
                    if (!(item is TValueMember))
                        return false;
                    return this.Contains(((TValueMember)(item)));
                }

                private void SimpleCopyTo(Array array, int arrayIndex)
                {
                    if (arrayIndex < 0)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (array == null)
                        throw new ArgumentNullException("array");
                    if (array.Length < this.Count)
                        throw new ArgumentException("array");
                    if (array.Length < this.Count + arrayIndex)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.container.CheckItemAt(i);
                        array.SetValue(this.container.elements[i], i + arrayIndex);
                    }
                }

                void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
                {
                    this.SimpleCopyTo(array, arrayIndex);
                }

                object IControlledStateCollection.this[int index]
                {
                    get { return this[index]; }
                }

                #endregion

                #region ICollection Members

                void ICollection.CopyTo(Array array, int index)
                {
                    this.SimpleCopyTo(array, index);
                }


                bool ICollection.IsSynchronized
                {
                    get { return false; }
                }

                object ICollection.SyncRoot
                {
                    get { return null; }
                }

                #endregion

                #region IEnumerable Members

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.GetEnumerator();
                }

                #endregion

                #region IControlledStateCollection<IGenericTypeParameter<ISymbolType>> Members

                public int Count
                {
                    get { return this.container.Count; }
                }

                public bool Contains(TValueMember item)
                {
                    throw new NotImplementedException();
                }

                public void CopyTo(TValueMember[] array, int arrayIndex)
                {
                    if (arrayIndex < 0)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (array == null)
                        throw new ArgumentNullException("array");
                    if (array.Length < this.Count)
                        throw new ArgumentException("array");
                    if (array.Length < this.Count + arrayIndex)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.container.CheckItemAt(i);
                        array[i + arrayIndex] = this.container.elements[i];
                    }
                }

                public IGenericTypeParameter<ISymbolType> this[int index]
                {
                    get {
                        if (index < 0 || index >= this.Count)
                            throw new ArgumentOutOfRangeException("index");
                        this.container.CheckItemAt(index);
                        return this.container.elements[index];
                    }
                }

                public TValueMember[] ToArray()
                {
                    TValueMember[] result = new TValueMember[this.Count];
                    this.CopyTo(result, 0);
                    return result;
                }

                #endregion

                #region IEnumerable<IGenericTypeParameter<ISymbolType>> Members

                public IEnumerator<TValueMember> GetEnumerator()
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.container.CheckItemAt(i);
                        yield return this.container.elements[i];
                    }
                }

                #endregion
            }
        }
    }
}