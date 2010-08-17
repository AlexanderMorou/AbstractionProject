using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
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
            private class KeysCollection :
                IControlledStateCollection<string>,
                IControlledStateCollection
            {
                private GenericParameterDictionary container;
                internal KeysCollection(GenericParameterDictionary container)
                {
                    this.container = container;
                }

                #region IControlledStateCollection<string> Members

                public int Count
                {
                    get { return this.container.Count; }
                }

                public bool Contains(string item)
                {
                    return this.container.ContainsKey(item);
                }

                public void CopyTo(string[] array, int arrayIndex)
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
                        array[i+arrayIndex] = this.container.tParamNames[i];
                }

                public string this[int index]
                {
                    get {
                        if (index < 0 || index >= this.Count)
                            throw new ArgumentOutOfRangeException("index");
                        return this.container.tParamNames[index];
                    }
                }

                public string[] ToArray()
                {
                    string[] result = new string[this.Count];
                    this.CopyTo(result, 0);
                    return result;
                }

                #endregion

                #region IEnumerable<string> Members

                public IEnumerator<string> GetEnumerator()
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

                #region IControlledStateCollection Members

                bool IControlledStateCollection.Contains(object item)
                {
                    if (!(item is string))
                        return false;
                    return this.Contains((string)item);
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
                        array.SetValue(this.container.tParamNames[i], i + arrayIndex);
                }

                void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
                {
                    SimpleCopyTo(array, arrayIndex);
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
            }
        }
    }
}
