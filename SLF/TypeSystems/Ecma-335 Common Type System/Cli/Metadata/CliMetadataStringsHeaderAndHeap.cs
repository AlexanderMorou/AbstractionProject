using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.IO;
using System.Collections;
using System.IO;
using AllenCopeland.Abstraction.Numerics;
using System.Diagnostics;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public class CliMetadataStringsHeaderAndHeap :
        CliMetadataStreamHeader,
        IEnumerable<Tuple<int, string>>
    {
        private int count;
        private int substringCount;
        private string[] data;
        private int[] hashCodes;
        //private uint tailLength = 0;
        private Dictionary<uint, uint> positionToIndexTable = new Dictionary<uint, uint>();
        private Dictionary<uint, int> substringIndexTable = new Dictionary<uint, int>();
        private EndianAwareBinaryReader reader;
        //public uint Add(string value)
        //{
        //    int fullCount = count + substringCount;
        //    this.blobCacheData = this.blobCacheData.EnsureSpaceExists(fullCount, 1);
        //    this.blobCacheHashCodes = this.blobCacheHashCodes.EnsureSpaceExists(fullCount, 1);
        //    cachedPosToBlobDataIndexTable.Add(tailLength, (uint) fullCount);
        //    this.blobCacheData[fullCount] = value;
        //    if (value == null)
        //    {
        //        this.blobCacheHashCodes[substringCount + count++] = -1;
        //        tailLength++;
        //    }
        //    else
        //    {
        //        tailLength += (uint) value.Length + 1;
        //        this.blobCacheHashCodes[substringCount + count++] = value.GetHashCode();
        //    }
        //    var result = tailLength;
        //    return result;
        //}

        private uint AddSubstring(string substringValue, uint position)
        {
            int fullCount = count + substringCount;
            this.data = this.data.EnsureSpaceExists(fullCount, 1);
            this.hashCodes = this.hashCodes.EnsureSpaceExists(fullCount, 1);
            positionToIndexTable.Add(position,(uint)fullCount);
            this.data[fullCount] = substringValue;
            this.hashCodes[substringCount++ + count] = substringValue.GetHashCode();
            var result = position;
            return result;
        }

        public void Remove(int index)
        {
            if (index + 1 < this.count)
            {
                Array.ConstrainedCopy(this.data, index + 1, this.data, index, (this.count - 1) - index);
                Array.ConstrainedCopy(this.hashCodes, index + 1, this.hashCodes, index, (this.count - 1) - index);
            }
            this.data[--count] = null;
            this.hashCodes[count] = 0;
        }
        public CliMetadataStringsHeaderAndHeap(CliMetadataStreamHeader originalHeader)
            : base(originalHeader)
        {
        }

        public bool IsValidOffset(uint offset)
        {
            return this.positionToIndexTable.ContainsKey(offset) ||
                   this.ReadSubstring(offset);
        }

        public int this[string value]
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

        public string this[uint index]
        {
            get
            {
                if (index >= base.Size)
                    throw new ArgumentOutOfRangeException("index");
                if (this.positionToIndexTable.ContainsKey(index))
                    return this.data[this.positionToIndexTable[index]];
                string subString;
                //if (this.substringIndexTable.TryGetValue(index, out subString))
                //    return subString;
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
                    for (int i = 0; i < resultChars.Length; i++, sourcePtr++, convertCharsPtr++)
                        *convertCharsPtr = *sourcePtr;
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

        #region IEnumerable<string> Members

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

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

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
    }
}
