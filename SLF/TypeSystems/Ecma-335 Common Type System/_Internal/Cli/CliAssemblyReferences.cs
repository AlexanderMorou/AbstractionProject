using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliAssemblyReferences :
        IReadOnlyDictionary<ICliMetadataAssemblyRefTableRow, ICliAssembly>,
        IReadOnlyDictionary
    {
        private KeysCollection keys;
        private ValuesCollection values;
        private ICliMetadataAssemblyRefTable[] referenceTables;

        private CliAssembly owner;
        private ICliMetadataAssemblyRefTableRow[] kData;
        private ICliAssembly[] vData;
        private bool[] vCheck;
        private object syncObject = new object();

        public CliAssemblyReferences(CliAssembly owner)
        {
            this.owner = owner;
            this.referenceTables = owner.ObtainAssemblyRefTables().ToArray();
            this.vData = new ICliAssembly[this.Count];
            this.kData = new ICliMetadataAssemblyRefTableRow[this.Count];
            this.vCheck = new bool[this.Count];
        }

        #region IControlledDictionary<ICliMetadataAssemblyRefTableRow,ICliAssembly> Members

        public IControlledCollection<ICliMetadataAssemblyRefTableRow> Keys
        {
            get
            {
                lock (syncObject)
                {
                    if (this.keys == null)
                        this.keys = new KeysCollection(this);
                    return this.keys;
                }
            }
        }

        public IControlledCollection<ICliAssembly> Values
        {
            get
            {
                lock (syncObject)
                {
                    if (this.values == null)
                        this.values = new ValuesCollection(this);
                    return this.values;
                }
            }
        }

        private void CheckKeyAt(int index)
        {
            lock (syncObject)
                if (this.kData[index] == null)
                    for (int tableIndex = 0, adjustedOffset = 0; tableIndex < this.referenceTables.Length; adjustedOffset += this.referenceTables[tableIndex++].Count)
                    {
                        var currentTable = this.referenceTables[tableIndex];
                        if (index >= adjustedOffset && index < adjustedOffset + currentTable.Length)
                            this.kData[index] = currentTable[index - adjustedOffset + 1];
                    }
        }

        private void CheckItemAt(int index)
        {
            lock (syncObject)
            {
                this.CheckKeyAt(index);
                this.CheckValueAt(index);
            }
        }

        private void CheckValueAt(int index)
        {
            lock (syncObject)
                if (!this.vCheck[index])
                {
                    try { this.vData[index] = (ICliAssembly)this.owner.IdentityManager.ObtainAssemblyReference(this.kData[index]); }
                    catch (FileNotFoundException) { }
                    this.vCheck[index] = true;
                }
        }

        public ICliAssembly this[ICliMetadataAssemblyRefTableRow key]
        {
            get
            {
                lock (syncObject)
                {
                    if (this.owner.IdentityManager.GetRelativeAssembly(key.MetadataRoot) != this.owner)
                        throw new KeyNotFoundException();
                    int index = this.Keys.IndexOf(key);
                    if (index == -1)
                        throw new KeyNotFoundException();
                    return this.Values[index];
                }
            }
        }

        public bool ContainsKey(ICliMetadataAssemblyRefTableRow key)
        {
            lock (syncObject)
                return this.Keys.Contains(key);
        }

        public bool TryGetValue(ICliMetadataAssemblyRefTableRow key, out ICliAssembly value)
        {
            lock (syncObject)
            {
                int index = this.Keys.IndexOf(key);
                if (index == -1)
                {
                    value = null;
                    return false;
                }
                else
                {
                    value = this.Values[index];
                    return true;
                }
            }
        }

        #endregion

        #region IControlledCollection<KeyValuePair<ICliMetadataAssemblyRefTableRow,ICliAssembly>> Members

        public int Count
        {
            get
            {
                lock (syncObject)
                {
                    if (this.referenceTables == null)
                        return 0;
                    int result = 0;
                    for (int i = 0; i < this.referenceTables.Length; i++)
                        result += this.referenceTables[i].Count;
                    return result;
                }
            }
        }

        public bool Contains(KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly> item)
        {
            if (item.Key == null || item.Value == null)
                throw new ArgumentException("Required part of element missing.", "element");
            lock (this.syncObject)
            {
                int index = this.Keys.IndexOf(item.Key);
                if (index != -1)
                {
                    if (this.Values[index] == item.Value)
                        return true;
                }
                return false;
            }
        }

        public void CopyTo(KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            PrepareValuesCopy();
            lock (this.syncObject)
                for (int i = 0; i < this.Count; i++)
                    array[i + arrayIndex] = new KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>(kData[i], vData[i]);
        }

        private void PrepareKeysCopy()
        {
            lock (this.syncObject)
            {
                for (int keyIndex = 0; keyIndex < this.kData.Length; keyIndex++)
                    if (this.kData[keyIndex] == null)
                        goto PrepareCopy;
                return;
            PrepareCopy:
                for (int tableIndex = 0, adjustedOffset = 0; tableIndex < this.referenceTables.Length; adjustedOffset += this.referenceTables[tableIndex++].Count)
                    this.referenceTables[tableIndex].CopyTo(kData, adjustedOffset);
            }
        }

        private void PrepareValuesCopy()
        {
            lock (this.syncObject)
            {
                this.PrepareKeysCopy();
                for (int i = 0; i < this.Count; i++)
                    CheckValueAt(i);
            }
        }

        public KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly> this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                return new KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>(this.Keys[index], this.Values[index]);
            }
        }

        public KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>[] ToArray()
        {
            var result = new KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly> element)
        {
            if (element.Key == null || element.Value == null)
                throw new ArgumentException("Required part of element missing.", "element");
            lock (this.syncObject)
            {
                int index = this.Keys.IndexOf(element.Key);
                if (index != -1)
                {
                    if (this.Values[index] == element.Value)
                        return index;
                }
            }
            return -1;
        }

        #endregion

        #region IEnumerable<KeyValuePair<ICliMetadataAssemblyRefTableRow,ICliAssembly>> Members

        public IEnumerator<KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                yield return new KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>(this.Keys[i], this.Values[i]);
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledDictionary Members

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
                if (key is ICliMetadataAssemblyRefTableRow)
                    return this[(ICliMetadataAssemblyRefTableRow) key];
                throw new KeyNotFoundException();
            }
        }

        bool IControlledDictionary.ContainsKey(object key)
        {
            if (key is ICliMetadataAssemblyRefTableRow)
                return this.ContainsKey((ICliMetadataAssemblyRefTableRow) key);
            return false;
        }

        IDictionaryEnumerator IControlledDictionary.GetEnumerator()
        {
            return new SimpleDictionaryEnumerator<ICliMetadataAssemblyRefTableRow, ICliAssembly>(this.GetEnumerator());
        }

        #endregion

        #region IControlledCollection Members


        bool IControlledCollection.Contains(object item)
        {
            if (item is KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>)
                return this.Contains((KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>) item);
            return false;
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            this.PrepareKeysCopy();
            for (int i = 0; i < this.Count; i++)
                array.SetValue(index: i + arrayIndex, value: new KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>(this.Keys[i], this.Values[i]));
        }

        object IControlledCollection.this[int index]
        {
            get
            {
                return this[index];
            }
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>)
                return this.IndexOf((KeyValuePair<ICliMetadataAssemblyRefTableRow, ICliAssembly>) element);
            return -1;
        }

        #endregion

    }
}
