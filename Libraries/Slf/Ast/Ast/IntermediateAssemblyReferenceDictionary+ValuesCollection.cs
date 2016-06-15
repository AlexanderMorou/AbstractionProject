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
        private class ValuesCollection :
            IControlledCollection<IAssembly>
        {
            private IntermediateAssemblyReferenceDictionary owner;

            public ValuesCollection(IntermediateAssemblyReferenceDictionary owner)
            {
                this.owner = owner;
            }

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(IAssembly item)
            {
                foreach (var @ref in this.owner.references)
                    if (@ref.Reference == item)
                        return true;
                return false;
            }

            public void CopyTo(IAssembly[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                for (int assemblyRefIndex = 0; assemblyRefIndex < this.Count; assemblyRefIndex++)
                    array[assemblyRefIndex + arrayIndex] = this.owner.references[assemblyRefIndex].Reference;
            }

            public IAssembly this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    return this.owner.references[index].Reference;
                }
            }

            public IAssembly[] ToArray()
            {
                IAssembly[] result = new IAssembly[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(IAssembly element)
            {
                for (int assemblyRefIndex = 0; assemblyRefIndex < this.Count; assemblyRefIndex++)
                    if (this.owner.references[assemblyRefIndex].Reference == element)
                        return assemblyRefIndex;
                return -1;
            }

            public IEnumerator<IAssembly> GetEnumerator()
            {
                foreach (var assemblyReference in this.owner.references)
                    yield return assemblyReference.Reference;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
