using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract partial class CliMetadataDrivenDictionary<TDeclarationIdentifier, TMetadata, TDeclaration> :
        IDeclarationDictionary<TDeclarationIdentifier, TDeclaration>,
        IDeclarationDictionary,
        IDisposable
        where TDeclarationIdentifier :
            class,
            IDeclarationUniqueIdentifier
        where TDeclaration :
            class,
            IDeclaration
    {
        const int METADATA_SOURCE_ARRAY = 0;
        const int METADATA_SOURCE_COLLECTION = 1;
        private int state = METADATA_SOURCE_ARRAY;
        private bool[] checkedMetadata;
        private IControlledCollection<TMetadata> metadataCollection;
        private TMetadata[] metadataSource;
        private TDeclaration[] declarationData;
        private KeysCollection keys;
        private ValuesCollection values;
        private object syncObject = new object();
        protected CliMetadataDrivenDictionary(int count, int state = METADATA_SOURCE_ARRAY)
        {

            if (state == METADATA_SOURCE_ARRAY)
            {
                this.checkedMetadata = new bool[count];
                this.metadataSource = new TMetadata[count];
            }
            this.declarationData = new TDeclaration[count];
        }

        protected CliMetadataDrivenDictionary(TMetadata[] metadata)
        {
            this.Initialize(metadata);
        }

        protected CliMetadataDrivenDictionary(IControlledCollection<TMetadata> metadata)
        {
            this.Initialize(metadata);
        }

        internal CliMetadataDrivenDictionary() { }

        /// <summary>
        /// Initializes the metadata driven dictionary with the metadata given
        /// up front.
        /// </summary>
        /// <param name="metadata">The <typeparamref name="TMetadata"/> elements
        /// from which the elements are derived.</param>
        internal void Initialize(TMetadata[] metadata)
        {
            this.metadataSource = metadata;
            int count = this.metadataSource.Length;
            this.declarationData = new TDeclaration[count];
            this.checkedMetadata = new bool[count];
            for (int i = 0; i < count; i++)
                this.checkedMetadata[i] = true;
        }

        internal void Initialize(IControlledCollection<TMetadata> collection)
        {
            this.metadataCollection = collection;
            this.state = METADATA_SOURCE_COLLECTION;
            this.declarationData = new TDeclaration[collection.Count];
        }

        protected abstract TMetadata GetMetadataAt(int index);

        protected abstract TDeclaration CreateElementFrom(TMetadata metadata, int index);

        private void CheckItemAt(int index)
        {
            this.CheckMetadataAt(index);
            lock (this.syncObject)
            {
                if (this.declarationData[index] == null)
                {
                    if (this.state == METADATA_SOURCE_ARRAY)
                        this.declarationData[index] = this.CreateElementFrom(this.metadataSource[index], index);
                    else if (this.state == METADATA_SOURCE_COLLECTION)
                        this.declarationData[index] = this.CreateElementFrom(this.metadataCollection[index], index);
                    else
                        throw new InvalidOperationException();
                }
            }
        }

        private TDeclarationIdentifier CheckIdentifierAt(int index)
        {
            lock (this.syncObject)
            {
                CheckMetadataAt(index);
                if (this.state == METADATA_SOURCE_ARRAY)
                    return this.GetIdentifierAt(index, this.metadataSource[index]);
                else
                    return this.GetIdentifierAt(index, this.metadataCollection[index]);
            }
        }

        protected abstract TDeclarationIdentifier GetIdentifierAt(int index, TMetadata metadata);

        private void CheckMetadataAt(int index)
        {
            lock (this.syncObject)
            {
                if (this.state == METADATA_SOURCE_ARRAY)
                {
                    if (!this.checkedMetadata[index])
                    {
                        this.metadataSource[index] = this.GetMetadataAt(index);
                        this.checkedMetadata[index] = true;
                    }
                }
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

        public virtual TDeclaration this[TDeclarationIdentifier key]
        {
            get
            {
                int index = this.Keys.IndexOf(key);
                if (index == -1)
                    throw new KeyNotFoundException();
                return this.Values[index];
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
            get {
                if (this.state == METADATA_SOURCE_ARRAY)
                    return this.metadataSource.Length;
                else if (this.state == METADATA_SOURCE_COLLECTION)
                    return this.metadataCollection.Count;
                else
                    throw new InvalidOperationException();
            }
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
                array[i + arrayIndex] = new KeyValuePair<TDeclarationIdentifier, TDeclaration>(this.CheckIdentifierAt(i), this.declarationData[i]);
            }
        }

        public KeyValuePair<TDeclarationIdentifier, TDeclaration> this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException();
                this.CheckItemAt(index);
                return new KeyValuePair<TDeclarationIdentifier, TDeclaration>(this.CheckIdentifierAt(index), this.declarationData[index]);
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
                yield return new KeyValuePair<TDeclarationIdentifier, TDeclaration>(this.CheckIdentifierAt(i), this.declarationData[i]);
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
                array.SetValue(new KeyValuePair<TDeclarationIdentifier, TDeclaration>(this.CheckIdentifierAt(i), this.declarationData[i]), i + arrayIndex);
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
            this.metadataCollection = null;
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
            if (this.state == METADATA_SOURCE_ARRAY)
            {
                for (int i = 0; i < this.Count; i++)
                    this.CheckMetadataAt(i);
                return this.metadataSource;
            }
            else if (this.state == METADATA_SOURCE_COLLECTION)
                return this.metadataCollection.ToArray();
            throw new InvalidOperationException();
        }
    }
}
