using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliAbstractAssemblyReferences :
        IReadOnlyDictionary<IAssemblyUniqueIdentifier, IAssembly>,
        IReadOnlyDictionary
    {
        private CliAssembly assembly;
        private IReadOnlyCollection<IAssemblyUniqueIdentifier> identifiers;
        private ICliMetadataAssemblyRefTableRow[] referenceSources;
        private IAssembly[] references;
        private object syncObject = new object();
        private ValuesCollection values;
        public CliAbstractAssemblyReferences(CliAssembly assembly)
        {
            var referenceTables = assembly.ObtainAssemblyRefTables().ToArray();
            List<ICliMetadataAssemblyRefTableRow> referenceSources = new List<ICliMetadataAssemblyRefTableRow>();
            HashSet<IAssemblyUniqueIdentifier> distinctIdentifiers = new HashSet<IAssemblyUniqueIdentifier>();
            foreach (var table in referenceTables)
            {
                var copy = table.ToArray();
                for (int copyIndex = 0; copyIndex < copy.Length; copyIndex++)
                {
                    var current = copy[copyIndex];
                    if (distinctIdentifiers.Add(CliCommon.GetAssemblyUniqueIdentifier(current).Item2))
                        referenceSources.Add(current);
                }
            }
            this.referenceSources = referenceSources.ToArray();
            this.identifiers = new ArrayReadOnlyCollection<IAssemblyUniqueIdentifier>(distinctIdentifiers.ToArray());
            this.assembly = assembly;
            this.references = new IAssembly[this.identifiers.Count];
        }


        public IControlledCollection<IAssemblyUniqueIdentifier> Keys
        {
            get { return this.identifiers; }
        }

        public IControlledCollection<IAssembly> Values
        {
            get {
                lock (this.syncObject)
                {
                    if (this.values == null)
                        this.values = new ValuesCollection(this);
                    return this.values;
                }
            }
        }

        public IAssembly this[IAssemblyUniqueIdentifier key]
        {
            get
            {
                int index = this.identifiers.IndexOf(key);
                if (index == -1)
                    throw new KeyNotFoundException();
                return this.Values[index];
            }
        }

        public bool ContainsKey(IAssemblyUniqueIdentifier key)
        {
            return this.identifiers.Contains(key);
        }

        public bool TryGetValue(IAssemblyUniqueIdentifier key, out IAssembly value)
        {
            int index = this.identifiers.IndexOf(key);
            if (index == -1)
            {
                value = null;
                return false;
            }
            value = this.Values[index];
            return true;
        }

        public int Count
        {
            get { return this.identifiers.Count; }
        }

        public bool Contains(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> item)
        {
            IAssembly localVersion;
            if (this.TryGetValue(item.Key, out localVersion))
                return item.Value == localVersion;
            return false;
        }

        public void CopyTo(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            lock (syncObject)
            {
                for (int localOffset = 0; localOffset < this.Count; localOffset++)
                    array[arrayIndex + localOffset] = new KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>(this.Keys[localOffset], this.Values[localOffset]);
            }
        }

        public KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                return new KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>(this.Keys[index], this.Values[index]);
            }
        }

        public KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>[] ToArray()
        {
            var result = new KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> element)
        {
            IAssembly localVersion;
            int index = 0;
            index = this.Keys.IndexOf(element.Key);
            if (index != -1 &&
                element.Value == this.Values[index])
                return index;
            return -1;
        }

        public IEnumerator<KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>> GetEnumerator()
        {
            for (int index = 0; index < this.Count; index++)
                yield return new KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>(this.Keys[index], this.Values[index]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

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
                if (key is IAssemblyUniqueIdentifier)
                    return this[(IAssemblyUniqueIdentifier) key];
                throw new KeyNotFoundException();
            }
        }

        bool IControlledDictionary.ContainsKey(object key)
        {
            if (key is IAssemblyUniqueIdentifier)
                return this.ContainsKey((IAssemblyUniqueIdentifier) key);
            return false;
        }

        IDictionaryEnumerator IControlledDictionary.GetEnumerator()
        {
            return new SimpleDictionaryEnumerator<IAssemblyUniqueIdentifier, IAssembly>(this.GetEnumerator());
        }

        bool IControlledCollection.Contains(object item)
        {
            if (item is KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>)
                return this.Contains((KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>) item);
            return false;
        }

        public void CopyTo(Array array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            lock (syncObject)
            {
                for (int localOffset = 0; localOffset < this.Count; localOffset++)
                    array.SetValue(new KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>(this.Keys[localOffset], this.Values[localOffset]), arrayIndex + localOffset);
            }
        }

        object IControlledCollection.this[int index]
        {
            get { return this[index]; }
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>)
                return this.IndexOf((KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>) element);
            return -1;
        }

        private void CheckItemAt(int index)
        {
            lock (this.syncObject)
                if (this.references[index] == null)
                    this.references[index] = this.assembly.IdentityManager.ObtainAssemblyReference(this.referenceSources[index]);
        }


        internal void CheckItems()
        {
            lock (this.syncObject)
                for (int referenceIndex = 0; referenceIndex < this.Count; referenceIndex++)
                    if (this.references[referenceIndex] == null)
                        this.references[referenceIndex] = this.assembly.IdentityManager.ObtainAssemblyReference(this.referenceSources[referenceIndex]);
        }

    }
}
