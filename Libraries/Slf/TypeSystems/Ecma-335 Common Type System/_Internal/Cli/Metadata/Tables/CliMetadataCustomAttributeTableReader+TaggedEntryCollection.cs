using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    partial class CliMetadataCustomAttributeTableReader
    {
        private class TaggedEntryCollection :
            IControlledCollection<ICliMetadataCustomAttributeTableRow>
        {
            private CliMetadataCustomAttributeTableReader owner;
            private CliMetadataHasCustomAttributeTag tag;
            private uint[] indices;
            internal TaggedEntryCollection(CliMetadataCustomAttributeTableReader owner, CliMetadataHasCustomAttributeTag tag)
            {
                this.owner = owner;
                long offset = this.owner.streamOffset;
                List<uint> indices = new List<uint>();
                this.tag = tag;
                int size = this.owner.__size;
                for (uint i = 1; i <= this.owner.Count; i++, offset += size)
                {
                    this.owner.reader.BaseStream.Seek(offset, SeekOrigin.Begin);

                    if (this.owner.ItemLoaded((uint)i))
                    {
                        if (this.owner[(int)i].ParentSource == tag)
                            indices.Add(i);
                    }
                    else
                        lock (this.owner.syncObject)
                            if (((CliMetadataHasCustomAttributeTag)(this.owner.reader.ReadByte() & 31)) == tag)
                                indices.Add(i);
                }
                this.indices = indices.ToArray();
            }

            #region IControlledCollection<ICliMetadataCustomAttributeTableRow> Members

            public int Count
            {
                get { return this.indices.Length; }
            }

            public bool Contains(ICliMetadataCustomAttributeTableRow item)
            {
                if (item.MetadataRoot != this.owner.metadataRoot)
                    return false;
                return this.indices.Contains(item.Index);
            }

            public void CopyTo(ICliMetadataCustomAttributeTableRow[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                for (int i = 0; i < this.Count; i++)
                    array[arrayIndex + i] = this.owner[(int)this.indices[i]];
            }

            public ICliMetadataCustomAttributeTableRow this[int index]
            {
                get {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(ArgumentWithException.index));
                    return this.owner[(int)this.indices[index]];
                }
            }

            public ICliMetadataCustomAttributeTableRow[] ToArray()
            {
                ICliMetadataCustomAttributeTableRow[] result = new ICliMetadataCustomAttributeTableRow[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(ICliMetadataCustomAttributeTableRow element)
            {
                if (element == null)
                    throw new ArgumentNullException("element");
                for (int i = 0, idx=(int)element.Index; i < this.Count; i++)
                    if (this.indices[i] == idx)
                        return i;
                return -1;
            }

            #endregion

            #region IEnumerable<ICliMetadataCustomAttributeTableRow> Members

            public IEnumerator<ICliMetadataCustomAttributeTableRow> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                    yield return this.owner[(int)this.indices[i]];
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }
    }
}
