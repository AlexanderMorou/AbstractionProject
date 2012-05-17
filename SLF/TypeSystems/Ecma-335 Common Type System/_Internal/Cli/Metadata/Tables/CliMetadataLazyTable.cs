using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    internal abstract class CliMetadataLazyTable<T> :
        IControlledCollection<T>,
        IControlledCollection,
        ICliMetadataTable
        where T :
            class,
            ICliMetadataTableRow
    {
        private T[] items;
        private uint rowCount;
        private ICliMetadataRoot metadataRoot;
        private bool fullyRead;
        protected CliMetadataLazyTable(ICliMetadataRoot metadataRoot, uint rowCount)
        {
            this.metadataRoot = metadataRoot;
            this.rowCount = rowCount;
            this.items = new T[rowCount];
        }

        //#region IControlledCollection<T> Members

        public bool Contains(T item)
        {
            for (int i = 0; i < items.Length; i++)
                if (object.ReferenceEquals(this.items[i], item))
                    return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            this.Read();
            for (uint i = 0; i < this.Count; i++)
                array[i + arrayIndex] = this.items[i];
        }

        public int Count
        {
            get { return (int) this.rowCount; }
        }

        public int IndexOf(T element)
        {
            for (int i = 0; i < items.Length; i++)
                if (object.ReferenceEquals(this.items[i], element))
                    return i + 1;
            return -1;
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];
            this.CopyTo(result, 0);
            return result;
        }

        public T this[int index]
        {
            get
            {
                if (index <= 0 || index > this.Count + 1)
                    throw new ArgumentOutOfRangeException("index");
                if (index == this.Count + 1)
                    return default(T);
                this.CheckItemAt((uint) index);
                return this.items[index - 1];
            }
        }

        internal void InjectLoadedItem(T item, uint index)
        {
            this.items[index - 1] = item;
        }

        internal bool ItemLoaded(uint index)
        {
            return this.items[index - 1] != null;
        }

        private void CheckItemAt(uint index)
        {
            if (this.items[index - 1] == null)
            {
                if (this.items.Length < index)
                    this.items = this.items.EnsureMinimalSpaceExists((uint) this.items.Length, (uint) ((index + 1) - this.items.Length), this.rowCount);
                this.items[index - 1] = ReadElementAt(index);
            }
        }

        protected abstract T ReadElementAt(uint index);

        //#endregion

        //#region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt((uint) i + 1);
                yield return this.items[i];
            }
        }

        //#endregion

        //#region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //#endregion

        //#region IControlledCollection Members

        bool IControlledCollection.Contains(object item)
        {
            if (item is T)
                return this.Contains((T) item);
            return false;
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            this.Read();
            for (int i = 0; i < this.Count; i++)
                array.SetValue(this.items[i], i + arrayIndex);
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is T)
                return this.IndexOf((T) element);
            return -1;
        }

        object IControlledCollection.this[int index]
        {
            get { return this[index]; }
        }

        //#endregion

        //#region ICliMetadataTable Members

        public abstract CliMetadataTableKinds Kind { get; }

        public virtual void Read()
        {
            if (fullyRead)
                return;
            if (this.items.Length != this.rowCount)
                this.items = this.items.EnsureMinimalSpaceExists((uint) this.items.Length, (uint) (rowCount - items.Length), (uint)this.Count);
            for (int i = 0; i < this.Count; i++)
                this.CheckItemAt((uint) i + 1);
            fullyRead = true;
        }

        public uint RowCount
        {
            get { return this.rowCount; }
        }


        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            this.items = null;
        }

        //#endregion

        public override string ToString()
        {
            return string.Format("{0} {1} entries.", this.Count, this.Kind);
        }
    }
}
