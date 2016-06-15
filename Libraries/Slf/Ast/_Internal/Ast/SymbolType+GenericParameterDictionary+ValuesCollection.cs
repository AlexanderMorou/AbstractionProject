using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    using TValueMember = IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>;
    partial class SymbolType
    {
        partial class GenericParameterDictionary
        {
            private class ValuesCollection :
                IControlledCollection<TValueMember>,
                IControlledCollection
            {
                private GenericParameterDictionary container;
                internal ValuesCollection(GenericParameterDictionary container)
                {
                    this.container = container;
                }

                #region IControlledCollection Members

                bool IControlledCollection.Contains(object item)
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
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.array, ExceptionMessageId.InsufficientSpaceForCopy, ThrowHelper.GetArgumentName(ArgumentWithException.array));
                    if (array.Length < this.Count + arrayIndex)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.container.CheckItemAt(i);
                        array.SetValue(this.container.elements[i], i + arrayIndex);
                    }
                }

                void IControlledCollection.CopyTo(Array array, int arrayIndex)
                {
                    this.SimpleCopyTo(array, arrayIndex);
                }

                object IControlledCollection.this[int index]
                {
                    get { return this[index]; }
                }

                int IControlledCollection.IndexOf(object element)
                {
                    if (element is TValueMember)
                        return this.IndexOf((TValueMember)element);
                    return -1;
                }

                #endregion

                #region IEnumerable Members

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.GetEnumerator();
                }

                #endregion

                #region IControlledCollection<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                public int Count
                {
                    get { return this.container.Count; }
                }

                public bool Contains(TValueMember item)
                {
                    for (int i = 0; i < this.Count; i++)
                        if (this.container.elements[i] == null)
                            continue;
                        else if (this.container.elements[i] == item)
                            return true;
                    return false;
                }

                public void CopyTo(TValueMember[] array, int arrayIndex = 0)
                {
                    if (arrayIndex < 0)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (array == null)
                        throw new ArgumentNullException("array");
                    if (array.Length < this.Count)
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.array, ExceptionMessageId.InsufficientSpaceForCopy, ThrowHelper.GetArgumentName(ArgumentWithException.array));
                    if (array.Length < this.Count + arrayIndex)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.container.CheckItemAt(i);
                        array[i + arrayIndex] = this.container.elements[i];
                    }
                }

                public IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType> this[int index]
                {
                    get {
                        if (index < 0 || index >= this.Count)
                            throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
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

                public int IndexOf(TValueMember element)
                {
                    for (int i = 0; i < this.Count; i++)
                        if (this.container.elements[i] == null)
                            continue;
                        else if (this.container.elements[i] == element)
                            return i;
                    return -1;
                }

                #endregion

                #region IEnumerable<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

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
