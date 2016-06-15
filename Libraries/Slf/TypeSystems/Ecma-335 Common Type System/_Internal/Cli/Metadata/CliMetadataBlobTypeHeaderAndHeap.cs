using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Utilities.Arrays;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal abstract class CliMetadataBlobTypeHeaderAndHeap<T> :
        CliMetadataStreamHeader,
        IDisposable,
        IEnumerable<Tuple<int, T>>
    {
        internal T[] data;
        private int[] hashCodes;
        private int[] encodedPaddings;
        internal int count;
        private uint tailLength = 0;
        private Dictionary<uint, uint> positionToIndexTable = new Dictionary<uint, uint>();
        private EndianAwareBinaryReader reader;
        private object syncObject;

        internal CliMetadataBlobTypeHeaderAndHeap(CliMetadataStreamHeader forwardHeader, object syncObject)
            : base(forwardHeader)
        {
            this.syncObject = syncObject;
        }

        public uint AddData(T value, byte encodedPadding)
        {
            this.encodedPaddings = this.encodedPaddings.EnsureSpaceExists(count, 1);
            this.data = this.data.EnsureSpaceExists(count, 1);
            this.hashCodes = this.hashCodes.EnsureSpaceExists(count, 1);
            this.encodedPaddings[count] = encodedPadding;
            positionToIndexTable.Add(tailLength, (uint) count);
            tailLength += this.GetSizeOf(value) + encodedPadding;

            this.data[count] = value;
            this.hashCodes[count] = GetHashCode(value);
            return (uint) count++;
        }

        protected abstract uint GetSizeOf(T value);

        public void Remove(int index)
        {
            if (index + 1 < this.count)
            {
                Array.ConstrainedCopy(this.data, index + 1, this.data, index, (this.count - 1) - index);
                Array.ConstrainedCopy(this.hashCodes, index + 1, this.hashCodes, index, (this.count - 1) - index);
            }
            this.data[--count] = default(T);
            this.hashCodes[count] = 0;
        }

        public int this[T value]
        {
            get
            {
                int hashCode = GetHashCode(value);
                for (int i = 0; i < this.data.Length; i++)
                {
                    if (this.hashCodes[i] == hashCode &&
                        ItemsEqual(this.data[i], value))
                        return i;
                }
                return -1;
            }
        }


        public T this[uint index]
        {
            get
            {
                if (index >= this.Size)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                if (this.positionToIndexTable.ContainsKey(index))
                    return this.data[this.positionToIndexTable[index]];
                if (index < this.Size)
                {
                    while (!this.positionToIndexTable.ContainsKey(index) &&
                            this.ReadEntry());
                    uint dataIndex;
                    if (this.positionToIndexTable.TryGetValue(index, out dataIndex))
                        return this.data[dataIndex];
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                }
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
            }
        }


        protected abstract bool ItemsEqual(T item1, T item2);

        protected abstract T GetData(byte[] data);

        protected abstract int GetHashCode(T value);

        //#region IEnumerable<T> Members

        public virtual IEnumerator<Tuple<int, T>> GetEnumerator()
        {
            return this._GetEnumerable().GetEnumerator();
        }

        protected IEnumerable<Tuple<int, T>> _GetEnumerable()
        {
            for (int i = 0; i < this.count; i++)
                yield return new Tuple<int, T>(i, this.data[i]);
        }

        //#endregion

        //#region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //#endregion

        internal virtual void Read(EndianAwareBinaryReader reader)
        {
            lock (syncObject)
            {
                var newStream = new Substream(reader.BaseStream, reader.BaseStream.Position, base.Size);
                this.reader = new EndianAwareBinaryReader(newStream, Endianness.LittleEndian, false);

                byte firstNull = this.reader.ReadByte();
                this.AddData(this.GetData(new byte[] { }), 1);
                if (firstNull != 0)
                    throw new BadImageFormatException(string.Format("The first item of a {0} heap must be null.", this.Name));
            }
            //while (this.ReadEntry())
            //    ;
        }

        internal bool ReadEntry()
        {
            var tailLength = this.tailLength;
            if (tailLength + 1 >= this.Size)
                return false;
            byte compressedIntBytes = 0;
            byte[] currentData = null;
            int currentDataCount = 0;

            currentDataCount = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader, out compressedIntBytes);
            if (tailLength + compressedIntBytes >= this.Size)
            {
                reader.BaseStream.Seek(-(tailLength + compressedIntBytes - this.Size), SeekOrigin.Current);
                return false;
            }
            else
                tailLength += compressedIntBytes;
            if (currentDataCount == 0)
                /* *
                 * Reached the end of the heap which is padded
                 * with zeroes until the next 4-byte boundry.
                 * */
                return false;
            if (tailLength + currentDataCount > this.Size)
                throw new BadImageFormatException(string.Format("The data within the {0} heap entry is longer than expected.", this.Name));
            currentData = new byte[currentDataCount];
            for (int i = 0; i < currentDataCount && tailLength < this.Size; i++, tailLength++)
            {
                currentData[i] = reader.ReadByte();
            }
            this.AddData(this.GetData(currentData), compressedIntBytes);
            if (tailLength >= this.Size)
                return false;
            return true;
        }

        private uint _Size { get { return base.Size; } }

        public int IndexSize
        {
            get
            {
                if (this._Size > ushort.MaxValue)
                    return 4;
                return 2;
            }
        }


        //#region IDisposable Members

        public void Dispose()
        {
            this.data = null;
        }

        //#endregion
    }
}
