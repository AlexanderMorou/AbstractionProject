using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliAssemblyReferences
    {
        private class KeysCollection :
            IControlledCollection<ICliMetadataAssemblyRefTableRow>,
            IControlledCollection
        {
            private CliAssemblyReferences owner;

            public KeysCollection(CliAssemblyReferences owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<ICliMetadataAssemblyRefTableRow> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(ICliMetadataAssemblyRefTableRow item)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.owner.CheckKeyAt(i);
                    if (this.owner.kData[i] == item)
                        return true;
                }
                return false;
            }

            public void CopyTo(ICliMetadataAssemblyRefTableRow[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                this.owner.PrepareKeysCopy();
                this.owner.kData.CopyTo(array, arrayIndex);
            }

            public ICliMetadataAssemblyRefTableRow this[int index]
            {
                get {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(ArgumentWithException.index));
                    this.owner.CheckKeyAt(index);
                    return this.owner.kData[index];
                }
            }

            public ICliMetadataAssemblyRefTableRow[] ToArray()
            {
                this.owner.PrepareKeysCopy();
                return (ICliMetadataAssemblyRefTableRow[])this.owner.kData.Clone();
            }

            public int IndexOf(ICliMetadataAssemblyRefTableRow element)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.owner.CheckKeyAt(i);
                    if (this.owner.kData[i] == element)
                        return i;
                }
                return -1;
            }

            #endregion

            #region IEnumerable<ICliMetadataAssemblyRefTableRow> Members

            public IEnumerator<ICliMetadataAssemblyRefTableRow> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.owner.CheckKeyAt(i);
                    yield return this.owner.kData[i];
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
                if (item is ICliMetadataAssemblyRefTableRow)
                    return this.Contains((ICliMetadataAssemblyRefTableRow) item);
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                this.owner.PrepareKeysCopy();
                this.owner.kData.CopyTo(array, arrayIndex);
            }

            object IControlledCollection.this[int index]
            {
                get {
                    return this[index];
                }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is ICliMetadataAssemblyRefTableRow)
                    return this.IndexOf((ICliMetadataAssemblyRefTableRow) element);
                return -1;
            }

            #endregion
        }
    }
}
