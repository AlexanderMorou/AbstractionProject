﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Utilities.Arrays;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public class CliMetadataGuidHeaderAndHeap :
        CliMetadataStreamHeader,
        IEnumerable<Guid>
    {
        private int count;
        private Guid[] data;
        private int[] hashCodes;

        public CliMetadataGuidHeaderAndHeap(CliMetadataStreamHeader originalHeader)
            : base(originalHeader)
        {
        }
        public uint Add(Guid value)
        {
            this.data = this.data.EnsureSpaceExists(count, 1);
            this.hashCodes = this.hashCodes.EnsureSpaceExists(count, 1);
            this.data[count] = value;
            this.hashCodes[count] = value.GetHashCode();
            return (uint) count++;
        }

        public void Remove(int index)
        {
            if (index + 1 < this.count)
            {
                Array.ConstrainedCopy(this.data, index + 1, this.data, index, (this.count - 1) - index);
                Array.ConstrainedCopy(this.hashCodes, index + 1, this.hashCodes, index, (this.count - 1) - index);
            }
            this.data[--count] = default(Guid);
            this.hashCodes[count] = 0;
        }


        public int this[Guid value]
        {
            get
            {
                int hashCode = value.GetHashCode();
                for (int i = 0; i < this.data.Length; i++)
                {
                    if (this.hashCodes[i] == hashCode &&
                        this.data[i] == value)
                        return i;
                }
                return -1;
            }
        }

        public Guid this[uint index]
        {
            get
            {
                if (index <= this.count && index > 0)
                    return this.data[index - 1];
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public override uint Size
        {
            get
            {
                return (uint)(this.count << 4);
            }
        }

        private uint _Size
        {
            get
            {
                return base.Size;
            }
        }

        internal void Read(EndianAwareBinaryReader reader)
        {
            if ((base.Size & 0xF) != 0)
                throw new BadImageFormatException("GUIDs are 16 bytes long, heap size not properly aligned.");
            int sizeShift = (int)base.Size >> 4;
            for (int i = 0; i < sizeShift; i++)
            {
                byte[] current = new byte[16];
                reader.Read(current, 0, current.Length);
                this.Add(new Guid(current));
            }
        }

        #region IEnumerable<Guid> Members

        public IEnumerator<Guid> GetEnumerator()
        {
            for (int i = 0; i < this.count; i++)
                yield return this.data[i];
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        internal uint ReadIndex(EndianAwareBinaryReader reader)
        {
            if (this._Size > ushort.MaxValue)
                return reader.ReadUInt32();
            return reader.ReadUInt16();
        }

        public int IndexSize
        {
            get
            {
                if (this._Size > ushort.MaxValue)
                    return 4;
                return 2;
            }
        }

    }
}