using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliAssemblyReferences
    {
        public class ValuesCollection :
            IControlledCollection<ICliAssembly>,
            IControlledCollection
        {
            private CliAssemblyReferences owner;

            public ValuesCollection(CliAssemblyReferences owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<ICliAssembly> Members

            public int Count
            {
                get
                {
                    return this.owner.Count;
                }
            }

            public bool Contains(ICliAssembly item)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    if (this.owner.vData[i] == item)
                        return true;
                }
                return false;
            }

            public void CopyTo(ICliAssembly[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                this.owner.PrepareValuesCopy();
                this.owner.vData.CopyTo(array, arrayIndex);
            }

            public ICliAssembly this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(ArgumentWithException.index));
                    this.owner.CheckItemAt(index);
                    return this.owner.vData[index];
                }
            }

            public ICliAssembly[] ToArray()
            {
                this.owner.PrepareValuesCopy();
                return (ICliAssembly[]) this.owner.vData.Clone();
            }

            public int IndexOf(ICliAssembly element)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    if (this.owner.vData[i] == element)
                        return i;
                }
                return -1;
            }

            #endregion

            #region IEnumerable<ICliAssembly> Members

            public IEnumerator<ICliAssembly> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    yield return this.owner.vData[i];
                }
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IControlledCollection Members

            bool IControlledCollection.Contains(object item)
            {
                if (item is ICliAssembly)
                    return this.Contains((ICliAssembly) item);
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                this.owner.PrepareValuesCopy();
                this.owner.vData.CopyTo(array, arrayIndex);
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
                if (element is ICliAssembly)
                    return this.IndexOf((ICliAssembly) element);
                return -1;
            }

            #endregion
        }
    }
}
