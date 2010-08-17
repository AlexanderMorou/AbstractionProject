using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
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
        internal partial class GenericParameterDictionary :
            IGenericParameterDictionary<IGenericTypeParameter<ISymbolType>, ISymbolType>,
            IGenericParameterDictionary
        {
            private string[] tParamNames;
            private KeysCollection keys;
            private ValuesCollection values;
            private GenericParameterMember[] elements;
            public GenericParameterDictionary(SymbolType parent)
                : base()
            {
                this.Parent = parent;
                this.tParamNames = new string[0];
                this.elements = new GenericParameterMember[0];
            }

            public GenericParameterDictionary(SymbolType parent, string[] tParamNames)
                : this(parent)
            {
                if (tParamNames != null)
                {
                    this.tParamNames = new string[tParamNames.Length];
                    tParamNames.CopyTo(this.tParamNames, 0);
                    this.elements = new GenericParameterMember[tParamNames.Length];
                }
            }

            public SymbolType Parent { get; private set; }

            #region IGenericParameterDictionary<IGenericTypeParameter<ISymbolType>,ISymbolType> Members

            ISymbolType IGenericParameterDictionary<IGenericTypeParameter<ISymbolType>, ISymbolType>.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            #region IDeclarationDictionary<IGenericTypeParameter<ISymbolType>> Members

            public int IndexOf(IGenericTypeParameter<ISymbolType> decl)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.elements[i] != null)
                        if (decl == this.elements[i])
                            return i;
                return -1;
            }

            #endregion

            private void CheckItemAt(int i)
            {
                if (this.elements[i] == null)
                    this.elements[i] = new GenericParameterMember(this.Parent,  i);
            }

            #region IControlledStateDictionary<string,IGenericTypeParameter<ISymbolType>> Members

            public IControlledStateCollection<string> Keys
            {
                get {
                    if (this.keys == null)
                        this.keys = new KeysCollection(this);
                    return this.keys;
                }
            }

            public IControlledStateCollection<IGenericTypeParameter<ISymbolType>> Values
            {
                get
                {
                    if (this.values == null)
                        this.values = new ValuesCollection(this);
                    return this.values;
                }
            }

            public IGenericTypeParameter<ISymbolType> this[string key]
            {
                get {
                    for (int i = 0; i <this.Count; i++)
                        if (this.tParamNames[i] == key)
                        {
                            this.CheckItemAt(i);
                            return this.elements[i];
                        }
                    throw new ArgumentException("key");
                }
            }

            public bool ContainsKey(string key)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.tParamNames[i] == key)
                        return true;
                return false;
            }

            public bool TryGetValue(string key, out IGenericTypeParameter<ISymbolType> value)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.tParamNames[i] == key)
                    {
                        this.CheckItemAt(i);
                        value = this.elements[i];
                        return true;
                    }
                value = null;
                return false;
            }

            #endregion

            #region IControlledStateCollection<KeyValuePair<string,IGenericTypeParameter<ISymbolType>>> Members

            public int Count
            {
                get { return this.tParamNames.Length; }
            }

            public bool Contains(KeyValuePair<string, IGenericTypeParameter<ISymbolType>> item)
            {
                var vkI = item;
                int index = -1;
                for (int i = 0; i < this.Count; i++)
                    if (this.tParamNames[i] == vkI.Key)
                    {
                        index = i;
                        break;
                    }
                if (index == -1)
                    return false;
                if (this.elements[index] == null)
                    return false;
                return vkI.Value == this.elements[index];
            }

            public void CopyTo(KeyValuePair<string, IGenericTypeParameter<ISymbolType>>[] array, int arrayIndex)
            {
                if (arrayIndex < 0)
                    throw new IndexOutOfRangeException("arrayIndex");
                if (array == null)
                    throw new ArgumentNullException("array");
                if (arrayIndex + this.Count > array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    array[i + arrayIndex] = new KeyValuePair<string, IGenericTypeParameter<ISymbolType>>(this.tParamNames[i], this.elements[i]);
                }
            }

            public KeyValuePair<string, IGenericTypeParameter<ISymbolType>> this[int index]
            {
                get {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    this.CheckItemAt(index);
                    return new KeyValuePair<string, IGenericTypeParameter<ISymbolType>>(this.tParamNames[index], this.elements[index]);
                }
            }

            public KeyValuePair<string, IGenericTypeParameter<ISymbolType>>[] ToArray()
            {
                var result = new KeyValuePair<string, IGenericTypeParameter<ISymbolType>>[this.Count];
                this.CopyTo(result, 0);
                return result;
            }

            #endregion

            #region IEnumerable<KeyValuePair<string,IGenericTypeParameter<ISymbolType>>> Members

            public IEnumerator<KeyValuePair<string, IGenericTypeParameter<ISymbolType>>> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    yield return new KeyValuePair<string, IGenericTypeParameter<ISymbolType>>(this.tParamNames[i], this.elements[i]);
                }
                yield break;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this.elements[i] != null)
                        this.elements[i].Dispose();
                }
                this.elements = null;
                this.tParamNames = null;
            }

            #endregion

            #region IDeclarationDictionary Members

            int IDeclarationDictionary.IndexOf(IDeclaration decl)
            {
                if (!(decl is IGenericTypeParameter<ISymbolType>))
                    return -1;
                return this.IndexOf((IGenericTypeParameter<ISymbolType>)decl);
            }

            #endregion

            #region IControlledStateDictionary Members

            IControlledStateCollection IControlledStateDictionary.Keys
            {
                get { return (IControlledStateCollection)this.Keys; }
            }

            IControlledStateCollection IControlledStateDictionary.Values
            {
                get { return ((IControlledStateCollection)(this.Values)); }
            }

            object IControlledStateDictionary.this[object key]
            {
                get
                {
                    if (key == null)
                        throw new ArgumentNullException("key");
                    if (!(key is string))
                        throw new ArgumentException("key");
                    return this[(string)key];
                }
            }

            bool IControlledStateDictionary.ContainsKey(object key)
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                if (!(key is string))
                    throw new ArgumentException("key");
                return this.ContainsKey((string)key);
            }

            IDictionaryEnumerator IControlledStateDictionary.GetEnumerator()
            {
                return new SimpleDictionaryEnumerator<string, IGenericTypeParameter<ISymbolType>>(this.GetEnumerator());
            }

            #endregion

            #region IControlledStateCollection Members

            int IControlledStateCollection.Count
            {
                get { return this.Count; }
            }

            bool IControlledStateCollection.Contains(object item)
            {
                if (!(item is KeyValuePair<string, IGenericTypeParameter<ISymbolType>>))
                    throw new ArgumentException("item");
                return this.Contains((KeyValuePair<string, IGenericTypeParameter<ISymbolType>>)(item));
            }

            private void SimpleCopyTo(Array array, int arrayIndex)
            {
                if (arrayIndex < 0)
                    throw new IndexOutOfRangeException("arrayIndex");
                if (array == null)
                    throw new ArgumentNullException("array");
                if (arrayIndex + this.Count > array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    array.SetValue(new KeyValuePair<string, IGenericTypeParameter<ISymbolType>>(this.tParamNames[i], this.elements[i]), i + arrayIndex);
                }
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
                SimpleCopyTo(array, index);
            }

            int ICollection.Count
            {
                get { return this.Count; }
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
