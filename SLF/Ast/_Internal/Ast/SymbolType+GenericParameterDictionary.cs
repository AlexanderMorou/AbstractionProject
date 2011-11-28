using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    partial class SymbolType
    {
        internal partial class GenericParameterDictionary :
            IGenericParameterDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>, ISymbolType>,
            IGenericParameterDictionary
        {
            private IGenericParameterUniqueIdentifier[] tParamNames;
            private KeysCollection keys;
            private ValuesCollection values;
            private GenericParameterMember[] elements;
            public GenericParameterDictionary(SymbolType parent)
                : base()
            {
                this.Parent = parent;
                this.tParamNames = new IGenericParameterUniqueIdentifier[0];
                this.elements = new GenericParameterMember[0];
            }

            public GenericParameterDictionary(SymbolType parent, string[] tParamNames)
                : this(parent)
            {
                if (tParamNames != null)
                {
                    this.tParamNames = new IGenericParameterUniqueIdentifier[tParamNames.Length];
                    for (int i = 0; i < tParamNames.Length; i++)
                        this.tParamNames[i] = AstIdentifier.GenericParameter(i, true);
                    this.elements = new GenericParameterMember[tParamNames.Length];
                }
            }

            public SymbolType Parent { get; private set; }

            #region IGenericParameterDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>,ISymbolType> Members

            ISymbolType IGenericParameterDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>, ISymbolType>.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            #region IDeclarationDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

            public int IndexOf(IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType> decl)
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

            #region IControlledDictionary<IGenericParameterUniqueIdentifier,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

            public IControlledCollection<IGenericParameterUniqueIdentifier> Keys
            {
                get {
                    if (this.keys == null)
                        this.keys = new KeysCollection(this);
                    return this.keys;
                }
            }

            public IControlledCollection<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Values
            {
                get
                {
                    if (this.values == null)
                        this.values = new ValuesCollection(this);
                    return this.values;
                }
            }

            public IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType> this[IGenericParameterUniqueIdentifier key]
            {
                get {
                    for (int i = 0; i <this.Count; i++)
                        if (this.tParamNames[i].Equals(key))
                        {
                            this.CheckItemAt(i);
                            return this.elements[i];
                        }
                    throw new KeyNotFoundException();
                }
            }

            public bool ContainsKey(IGenericParameterUniqueIdentifier key)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.tParamNames[i].Equals(key))
                        return true;
                return false;
            }

            public bool TryGetValue(IGenericParameterUniqueIdentifier key, out IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType> value)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.tParamNames[i].Equals(key))
                    {
                        this.CheckItemAt(i);
                        value = this.elements[i];
                        return true;
                    }
                value = null;
                return false;
            }

            #endregion

            #region IControlledCollection<KeyValuePair<string,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>> Members

            public int Count
            {
                get { return this.tParamNames.Length; }
            }

            public bool Contains(KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> item)
            {
                var vkI = item;
                int index = -1;
                for (int i = 0; i < this.Count; i++)
                    if (this.tParamNames[i].Equals(vkI.Key))
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

            public void CopyTo(KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0)
                    throw new IndexOutOfRangeException("arrayIndex");
                if (array == null)
                    throw new ArgumentNullException("array");
                if (arrayIndex + this.Count > array.Length)
                    throw new IndexOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    array[i + arrayIndex] = new KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>(this.tParamNames[i], this.elements[i]);
                }
            }

            public KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> this[int index]
            {
                get {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    this.CheckItemAt(index);
                    return new KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>(this.tParamNames[index], this.elements[index]);
                }
            }

            public KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>[] ToArray()
            {
                var result = new KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>[this.Count];
                this.CopyTo(result, 0);
                return result;
            }

            public int IndexOf(KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> element)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.elements[i] == null)
                        continue;
                    else if (this.tParamNames[i] == element.Key &&
                             this.elements[i] == element.Value)
                        return i;
                return -1;
            }
            #endregion

            #region IEnumerable<KeyValuePair<IGenericParameterUniqueIdentifier,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>> Members

            public IEnumerator<KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    yield return new KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>(this.tParamNames[i], this.elements[i]);
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
                if (!(decl is IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>))
                    return -1;
                return this.IndexOf((IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>)decl);
            }

            #endregion

            #region IControlledDictionary Members

            IControlledCollection IControlledDictionary.Keys
            {
                get { return (IControlledCollection)this.Keys; }
            }

            IControlledCollection IControlledDictionary.Values
            {
                get { return ((IControlledCollection)(this.Values)); }
            }

            object IControlledDictionary.this[object key]
            {
                get
                {
                    if (key == null)
                        throw new ArgumentNullException("key");
                    if (!(key is IGenericParameterUniqueIdentifier))
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.key), key.GetType().ToString(), typeof(IGenericParameterUniqueIdentifier).ToString());
                    return this[(IGenericParameterUniqueIdentifier)key];
                }
            }

            bool IControlledDictionary.ContainsKey(object key)
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                if (!(key is IGenericParameterUniqueIdentifier))
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.key), key.GetType().ToString(), typeof(IGenericParameterUniqueIdentifier).ToString());
                return this.ContainsKey((IGenericParameterUniqueIdentifier)key);
            }

            IDictionaryEnumerator IControlledDictionary.GetEnumerator()
            {
                return new SimpleDictionaryEnumerator<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>(this.GetEnumerator());
            }

            #endregion

            #region IControlledCollection Members

            int IControlledCollection.Count
            {
                get { return this.Count; }
            }

            bool IControlledCollection.Contains(object item)
            {
                if (!(item is KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>))
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.item, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.item), item.GetType().ToString(), typeof(KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>).ToString());
                return this.Contains((KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>)(item));
            }

            private void SimpleCopyTo(Array array, int arrayIndex)
            {
                if (array == null)
                    throw new ArgumentNullException("array");
                if (arrayIndex < 0 || arrayIndex + this.Count > array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckItemAt(i);
                    array.SetValue(new KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>(this.tParamNames[i], this.elements[i]), i + arrayIndex);
                }
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
                if (element is KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>)
                    return this.IndexOf((KeyValuePair<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>)element);
                else if (element is IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>)
                    return this.IndexOf((IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>)element);
                return -1;
            }

            #endregion
        }
    }
}
