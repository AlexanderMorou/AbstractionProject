using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliAbstractAssemblyReferences
    {
        private class ValuesCollection :
            IControlledCollection<IAssembly>,
            IControlledCollection
        {
            private CliAbstractAssemblyReferences owner;

            public ValuesCollection(CliAbstractAssemblyReferences owner)
            {
                this.owner = owner;
            }

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(IAssembly item)
            {
                for (int referenceIndex = 0; referenceIndex < this.Count; referenceIndex++)
                {
                    this.owner.CheckItemAt(index: referenceIndex);
                    if (this.owner.references[referenceIndex] == item)
                        return true;
                }
                return false;
            }

            public void CopyTo(IAssembly[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                this.owner.CheckItems();
                this.owner.references.CopyTo(array, arrayIndex);
            }

            public IAssembly this[int index]
            {
                get
                {
                    this.owner.CheckItemAt(index);
                    IAssembly result;
                    lock (this.owner.syncObject)
                        result = this.owner.references[index];
                    return result;
                }
            }

            public IAssembly[] ToArray()
            {
                this.owner.CheckItems();
                IAssembly[] result = new IAssembly[this.Count];
                lock (this.owner.syncObject)
                    this.owner.references.CopyTo(result, 0);
                return result;
            }

            public int IndexOf(IAssembly element)
            {
                for (int referenceIndex = 0; referenceIndex < this.Count; referenceIndex++)
                {
                    this.owner.CheckItemAt(index: referenceIndex);
                    if (this.owner.references[referenceIndex] == element)
                        return referenceIndex;
                }
                return -1;
            }

            public IEnumerator<IAssembly> GetEnumerator()
            {
                for (int referenceIndex = 0; referenceIndex < this.Count; referenceIndex++)
                {
                    this.owner.CheckItemAt(referenceIndex);
                    IAssembly item = this.owner.references[referenceIndex];
                    lock (this.owner.syncObject)
                        yield return item;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            bool IControlledCollection.Contains(object item)
            {
                if (item is IAssembly)
                    return this.Contains((IAssembly) item);
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                this.owner.CheckItems();
                lock (this.owner.syncObject)
                    this.owner.references.CopyTo(array, arrayIndex);
            }

            object IControlledCollection.this[int index]
            {
                get { return this[index]; }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is IAssembly)
                    return this.IndexOf((IAssembly) element);
                return -1;
            }
        }
    }
}
