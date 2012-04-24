using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract partial class CliMetadataDrivenDictionary<TDeclarationIdentifier, TMetadata, TDeclaration> :
        IDeclarationDictionary<TDeclarationIdentifier, TDeclaration>,
        IDeclarationDictionary,
        IDisposable
        where TDeclarationIdentifier :
            IDeclarationUniqueIdentifier
        where TDeclaration :
            class,
            IDeclaration
    {
        private bool[] checkedMetadata;
        private TMetadata[] metadataSource;
        private TDeclaration[] declarationData;
        private KeysCollection keys;
        private ValuesCollection values;

        protected CliMetadataDrivenDictionary(int count)
        {
            this.metadataSource = new TMetadata[count];
            this.declarationData = new TDeclaration[count];
            this.checkedMetadata = new bool[count];
        }

        protected abstract TMetadata GetMetadataAt(int index);

        protected abstract TDeclaration CreateElementFrom(TMetadata metadata);

        private void CheckItemAt(int index)
        {
            CheckMetadataAt(index);
            lock (this.declarationData)
                if (this.declarationData[index] == null)
                    this.declarationData[index] = this.CreateElementFrom(this.metadataSource[index]);
        }

        private void CheckMetadataAt(int index)
        {
            lock (this.metadataSource)
                if (!this.checkedMetadata[index])
                {
                    this.metadataSource[index] = this.GetMetadataAt(index);
                    this.checkedMetadata[index] = true;
                }
        }

        //#region IControlledDictionary<TDeclarationIdentifier,TDeclaration> Members

        public IControlledCollection<TDeclarationIdentifier> Keys
        {
            get
            {
                if (this.keys == null)
                    this.keys = new KeysCollection(this);
                return this.keys;
            }
        }

        public IControlledCollection<TDeclaration> Values
        {
            get
            {
                if (this.values == null)
                    this.values = new ValuesCollection(this);
                return this.values;
            }
        }

        public TDeclaration this[TDeclarationIdentifier key]
        {
            get
            {
                int index = this.Keys.IndexOf(key);
                if (index == -1)
                    throw new KeyNotFoundException();
                return this.values[index];
            }
        }

        public bool ContainsKey(TDeclarationIdentifier key)
        {
            return this.Keys.Contains(key);
        }

        public bool TryGetValue(TDeclarationIdentifier key, out TDeclaration value)
        {
            int index = this.Keys.IndexOf(key);
            if (index == -1)
            {
                value = null;
                return false;
            }
            value = this.Values[index];
            return true;
        }

        //#endregion

        //#region IControlledCollection<KeyValuePair<TDeclarationIdentifier,TDeclaration>> Members

        public int Count
        {
            get { return this.metadataSource.Length; }
        }

        public bool Contains(KeyValuePair<TDeclarationIdentifier, TDeclaration> item)
        {
            int index = this.Keys.IndexOf(item.Key);
            if (index == -1)
                return false;
            return this.Values[index] == item.Value;
        }

        public void CopyTo(KeyValuePair<TDeclarationIdentifier, TDeclaration>[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                array[i + arrayIndex] = new KeyValuePair<TDeclarationIdentifier, TDeclaration>((TDeclarationIdentifier) this.declarationData[i].UniqueIdentifier, this.declarationData[i]);
            }
        }

        public KeyValuePair<TDeclarationIdentifier, TDeclaration> this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException();
                this.CheckItemAt(index);
                return new KeyValuePair<TDeclarationIdentifier, TDeclaration>((TDeclarationIdentifier) this.declarationData[index].UniqueIdentifier, this.declarationData[index]);
            }
        }

        public KeyValuePair<TDeclarationIdentifier, TDeclaration>[] ToArray()
        {
            var result = new KeyValuePair<TDeclarationIdentifier, TDeclaration>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(KeyValuePair<TDeclarationIdentifier, TDeclaration> element)
        {
            int index = this.Keys.IndexOf(element.Key);
            if (index == -1)
                return index;
            if (this.Values[index] == element.Value)
                return index;
            return -1;
        }

        //#endregion

        //#region IEnumerable<KeyValuePair<TDeclarationIdentifier,TDeclaration>> Members

        public IEnumerator<KeyValuePair<TDeclarationIdentifier, TDeclaration>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                yield return new KeyValuePair<TDeclarationIdentifier, TDeclaration>((TDeclarationIdentifier) this.declarationData[i].UniqueIdentifier, this.declarationData[i]);
            }
        }

        //#endregion

        //#region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //#endregion

        //#region IControlledDictionary Members

        IControlledCollection IControlledDictionary.Keys
        {
            get { return (IControlledCollection) this.Keys; }
        }

        IControlledCollection IControlledDictionary.Values
        {
            get { return (IControlledCollection) this.Values; }
        }

        object IControlledDictionary.this[object key]
        {
            get
            {
                if (key is TDeclarationIdentifier)
                    return this[(TDeclarationIdentifier) key];
                throw new KeyNotFoundException();
            }
        }

        bool IControlledDictionary.ContainsKey(object key)
        {
            if (key is TDeclarationIdentifier)
                return this.ContainsKey((TDeclarationIdentifier) key);
            return false;
        }

        IDictionaryEnumerator IControlledDictionary.GetEnumerator()
        {
            return new SimpleDictionaryEnumerator<TDeclarationIdentifier, TDeclaration>(this.GetEnumerator());
        }

        //#endregion

        //#region IControlledCollection Members

        bool IControlledCollection.Contains(object item)
        {
            if (item is KeyValuePair<TDeclarationIdentifier, TDeclaration>)
                return this.Contains((KeyValuePair<TDeclarationIdentifier, TDeclaration>) item);
            return false;
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                array.SetValue(new KeyValuePair<TDeclarationIdentifier, TDeclaration>((TDeclarationIdentifier) this.declarationData[i].UniqueIdentifier, this.declarationData[i]), i + arrayIndex);
            }
        }

        object IControlledCollection.this[int index]
        {
            get { return this[index]; }
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is KeyValuePair<TDeclarationIdentifier, TDeclaration>)
                return this.IndexOf((KeyValuePair<TDeclarationIdentifier, TDeclaration>) element);
            return -1;
        }

        //#endregion

        //#region IDeclarationDictionary<TDeclarationIdentifier,TDeclaration> Members

        public int IndexOf(TDeclaration decl)
        {
            return this.Values.IndexOf(decl);
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            for (int i = 0; i < this.Count; i++)
                this.declarationData[i].Dispose();
            this.declarationData = null;
            this.metadataSource = null;
        }

        //#endregion

        //#region IDeclarationDictionary Members

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (decl is TDeclaration)
                return this.IndexOf((TDeclaration) decl);
            return -1;
        }

        //#endregion

        internal TMetadata[] GetMetadata()
        {
            for (int i = 0; i < this.Count; i++)
                this.CheckMetadataAt(i);
            return this.metadataSource;
        }
    }
}
