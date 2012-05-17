using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal class CliMetadataStringsHeaderAndHeap :
        CliMetadataStreamHeader,
        IEnumerable<Tuple<int, string>>,
        ICliMetadataStringsHeaderAndHeap,
        IDisposable
    {
        private int count;
        private int substringCount;
        private string[] data;
        private Dictionary<uint, uint> positionToIndexTable = new Dictionary<uint, uint>();
        private EndianAwareBinaryReader reader;

        private uint AddSubstring(string substringValue, uint position)
        {
            int fullCount = count + substringCount++;
            this.data = this.data.EnsureSpaceExists(fullCount, 1);
            positionToIndexTable.Add(position, (uint) fullCount);
            this.data[fullCount] = substringValue;
            var result = position;
            return result;
        }

        public CliMetadataStringsHeaderAndHeap(CliMetadataStreamHeader originalHeader)
            : base(originalHeader)
        {
        }

        public string this[uint index]
        {
            get
            {
                if (index >= base.Size)
                    throw new ArgumentOutOfRangeException("index");
                if (this.positionToIndexTable.ContainsKey(index))
                    return this.data[this.positionToIndexTable[index]];
                if (ReadSubstring(index))
                    return this.data[this.positionToIndexTable[index]];
                throw new ArgumentOutOfRangeException("index");
            }
        }

        private unsafe bool ReadSubstring(uint index)
        {
            reader.BaseStream.Position = index;
        /* *
         * To save space, smaller strings that exist as the 
         * tail end of another string, are condensed accordingly.
         * *
         * It's quicker to construct the strings from the 
         * original source than it is to iterate through the
         * location table used to quickly look items up.
         * */
        readChar:
            uint loc = index;
            while (loc < base.Size)
            {
                byte current = reader.ReadByte();
                if (current == 0)
                    break;
                loc++;
            }
            uint size = loc - index;
            reader.BaseStream.Position = index;
            byte[] result = new byte[size];

            for (int i = 0; i < size; i++)
                result[i] = reader.ReadByte();
            var convertResult = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, result);
            char[] resultChars = new char[convertResult.Length >> 1];
            fixed (byte* convertedBytes = convertResult)
            {
                fixed (char* convertChars = resultChars)
                {
                    char* sourcePtr = (char*) convertedBytes;
                    char* convertCharsPtr = convertChars;
                    for (int i = 0; i < resultChars.Length; i++)
                        *convertCharsPtr++ = *sourcePtr++;
                }
            }
            this.AddSubstring(new string(resultChars, 0, resultChars.Length), index);
            return loc < base.Size;
        }

        internal void Read(EndianAwareBinaryReader reader)
        {
            var newStream = new Substream(reader.BaseStream, reader.BaseStream.Position, base.Size);
            this.reader = new EndianAwareBinaryReader(newStream, Endianness.LittleEndian, false);
        }

        //#region IEnumerable<string> Members

        private IEnumerable<Tuple<int, string>> _Enumerator
        {
            get
            {
                return (from kvp in this.positionToIndexTable
                        select new Tuple<int, string>((int) kvp.Key, this.data[kvp.Value]));
            }
        }

        public IEnumerator<Tuple<int, string>> GetEnumerator()
        {
            return (from _ in _Enumerator
                    orderby _.Item2,
                            _.Item1
                    select _).GetEnumerator();
        }

        //#endregion

        //#region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //#endregion

        internal uint ReadIndex(EndianAwareBinaryReader reader)
        {
            if (this.Size > ushort.MaxValue)
                return reader.ReadUInt32();
            return reader.ReadUInt16();
        }

        public int IndexSize
        {
            get
            {
                if (this.Size > ushort.MaxValue)
                    return 4;
                return 2;
            }
        }

        public void Dispose()
        {
            this.data = null;
            this.positionToIndexTable = null;
            this.reader = null;
        }

    }
}
