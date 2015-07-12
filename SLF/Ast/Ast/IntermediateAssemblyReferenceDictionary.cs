using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public partial class IntermediateAssemblyReferenceDictionary :
        IControlledDictionary<IAssemblyUniqueIdentifier, IAssembly>
    {
        private IAssemblyReferenceCollection references;
        private KeysCollection keys;
        private ValuesCollection values;

        internal IntermediateAssemblyReferenceDictionary(IAssemblyReferenceCollection references)
        {
            this.references = references;
        }

        public IControlledCollection<IAssemblyUniqueIdentifier> Keys
        {
            get
            {
                return this.keys ?? (this.keys = new KeysCollection(this));
            }
        }

        public IControlledCollection<IAssembly> Values
        {
            get
            {
                return this.values ?? (this.values = new ValuesCollection(this));
            }
        }

        public IAssembly this[IAssemblyUniqueIdentifier key]
        {
            get {
                int index = this.Keys.IndexOf(key);
                if (index == -1)
                    throw new KeyNotFoundException();
                return this.values[index];
            }
        }

        public bool ContainsKey(IAssemblyUniqueIdentifier key)
        {
            return this.Keys.Contains(key);
        }

        public bool TryGetValue(IAssemblyUniqueIdentifier key, out IAssembly value)
        {
            int index = this.Keys.IndexOf(key);
            if (index == -1)
            {
                value = null;
                return false;
            }
            value = this.values[index];
            return true;
        }

        public int Count
        {
            get {
                return this.references.Count;
            }
        }

        public bool Contains(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> item)
        {
            IAssembly check;
            if (this.TryGetValue(item.Key, out check) && check == item.Value)
                return true;
            return false;
        }

        public void CopyTo(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int index = 0; index < this.Count; index++)
                array[arrayIndex + index] = this[index];
        }

        public KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> this[int index]
        {
            get {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
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
            int index = this.Keys.IndexOf(element.Key);
            return index == -1 || this.Values[index] == element.Value ? index : -1;
        }

        public IEnumerator<KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>> GetEnumerator()
        {
            for (int index = 0; index < this.Count; index++)
                yield return this[index];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
