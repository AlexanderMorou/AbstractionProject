using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateAssemblyReferenceDictionary
    {
        private class KeysCollection :
            IControlledCollection<IAssemblyUniqueIdentifier>
        {
            private IntermediateAssemblyReferenceDictionary owner;

            internal KeysCollection(IntermediateAssemblyReferenceDictionary owner)
            {
                this.owner = owner;
            }

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(IAssemblyUniqueIdentifier item)
            {
                foreach (var assemblyReference in this.owner.references)
                    if (assemblyReference.Reference.UniqueIdentifier.Equals(item))
                        return true;
                return false;
            }

            public void CopyTo(IAssemblyUniqueIdentifier[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            }

            public IAssemblyUniqueIdentifier this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    return this.owner.references[index].Reference.UniqueIdentifier;
                }
            }

            public IAssemblyUniqueIdentifier[] ToArray()
            {
                IAssemblyUniqueIdentifier[] result = new IAssemblyUniqueIdentifier[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(IAssemblyUniqueIdentifier element)
            {
                for (int assemblyRefIndex = 0; assemblyRefIndex < this.Count; assemblyRefIndex++)
                    if (this.owner.references[assemblyRefIndex].Reference.UniqueIdentifier.Equals(element))
                        return assemblyRefIndex;
                return -1;
            }

            public IEnumerator<IAssemblyUniqueIdentifier> GetEnumerator()
            {
                foreach (var assemblyReference in this.owner.references)
                    yield return assemblyReference.Reference.UniqueIdentifier;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
