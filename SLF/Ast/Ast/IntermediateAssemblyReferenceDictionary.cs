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
            get { throw new NotImplementedException(); }
        }

        public IControlledCollection<IAssembly> Values
        {
            get { throw new NotImplementedException(); }
        }

        public IAssembly this[IAssemblyUniqueIdentifier key]
        {
            get { throw new NotImplementedException(); }
        }

        public bool ContainsKey(IAssemblyUniqueIdentifier key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(IAssemblyUniqueIdentifier key, out IAssembly value)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool Contains(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>[] array, int arrayIndex = 0)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>[] ToArray()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(KeyValuePair<IAssemblyUniqueIdentifier, IAssembly> element)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<IAssemblyUniqueIdentifier, IAssembly>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
