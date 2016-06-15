using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
            private class KeysCollection :
                IControlledCollection<IGenericParameterUniqueIdentifier>,
                IControlledCollection
            {
                private GenericParameterDictionary container;
                internal KeysCollection(GenericParameterDictionary container)
                {
                    this.container = container;
                }

                #region IControlledCollection<IGenericParameterUniqueIdentifier> Members

                public int Count
                {
                    get { return this.container.Count; }
                }

                public bool Contains(IGenericParameterUniqueIdentifier item)
                {
                    return this.container.ContainsKey(item);
                }

                public void CopyTo(IGenericParameterUniqueIdentifier[] array, int arrayIndex = 0)
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
                        array[i+arrayIndex] = this.container.tParamNames[i];
                }

                public IGenericParameterUniqueIdentifier this[int index]
                {
                    get {
                        if (index < 0 || index >= this.Count)
                            throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                        return this.container.tParamNames[index];
                    }
                }

                public IGenericParameterUniqueIdentifier[] ToArray()
                {
                    IGenericParameterUniqueIdentifier[] result = new IGenericParameterUniqueIdentifier[this.Count];
                    this.CopyTo(result, 0);
                    return result;
                }

                public int IndexOf(IGenericParameterUniqueIdentifier key)
                {
                    for (int i = 0; i < this.Count; i++)
                        if (this.container.tParamNames[i] == key)
                            return i;
                    return -1;
                }

                #endregion

                #region IEnumerable<IGenericParameterUniqueIdentifier> Members

                public IEnumerator<IGenericParameterUniqueIdentifier> GetEnumerator()
                {
                    for (int i = 0; i < this.Count; i++)
                        yield return this.container.tParamNames[i];
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
                    if (!(item is IGenericParameterUniqueIdentifier))
                        return false;
                    return this.Contains((IGenericParameterUniqueIdentifier)item);
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
                        array.SetValue(this.container.tParamNames[i], i + arrayIndex);
                }

                void IControlledCollection.CopyTo(Array array, int arrayIndex)
                {
                    SimpleCopyTo(array, arrayIndex);
                }

                object IControlledCollection.this[int index]
                {
                    get { return this[index]; }
                }

                int IControlledCollection.IndexOf(object element)
                {
                    if (element is IGenericParameterUniqueIdentifier)
                        return this.IndexOf((IGenericParameterUniqueIdentifier)element);
                    return -1;
                }
                #endregion

            }
        }
    }
}
