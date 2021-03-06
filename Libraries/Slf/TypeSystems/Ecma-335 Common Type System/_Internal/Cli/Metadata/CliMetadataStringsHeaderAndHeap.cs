﻿using System;
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
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal class CliMetadataStringsHeaderAndHeap :
        CliMetadataStreamHeader,
        ICliMetadataStringsHeaderAndHeap,
        IDisposable
    {
        private int substringCount;
        private string[] data;
        private object syncObject;
        private Dictionary<uint, uint> positionToIndexTable = new Dictionary<uint, uint>();
        private EndianAwareBinaryReader reader;

        private uint AddSubstring(string substringValue, uint position)
        {
            int fullCount = substringCount++;
            this.data = this.data.EnsureSpaceExists(fullCount, 1);
            positionToIndexTable.Add(position, (uint) fullCount);
            this.data[fullCount] = substringValue;
            var result = position;
            return result;
        }

        public CliMetadataStringsHeaderAndHeap(CliMetadataStreamHeader originalHeader, object syncObject)
            : base(originalHeader)
        {
            this.syncObject = syncObject;
        }

        public string this[uint index]
        {
            get
            {
                lock (this.syncObject)
                {
                    if (index >= base.Size)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    if (this.positionToIndexTable.ContainsKey(index))
                        return this.data[this.positionToIndexTable[index]];
                    if (ReadSubstring(index))
                        return this.data[this.positionToIndexTable[index]];
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                }
            }
        }

        private unsafe bool ReadSubstring(uint index)
        {
            lock (this.syncObject)
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
                this.AddSubstring(ConvertUTF8ByteArray(result), index);
                return loc < base.Size;
            }
        }

        unsafe public static string ConvertUTF8ByteArray(byte[] result)
        {
            var convertResult = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, result);
            char[] resultChars = new char[convertResult.Length >> 1];
            fixed (byte* convertedBytes = convertResult)
            {
                fixed (char* convertChars = resultChars)
                {
                    char* sourcePtr = (char*)convertedBytes;
                    char* convertCharsPtr = convertChars;
                    for (int i = 0; i < resultChars.Length; i++)
                        *convertCharsPtr++ = *sourcePtr++;
                }
            }
            return new string(resultChars, 0, resultChars.Length);
        }

        internal void Read(EndianAwareBinaryReader reader)
        {
            var newStream = new Substream(reader.BaseStream, reader.BaseStream.Position, base.Size);
            this.reader = new EndianAwareBinaryReader(newStream, Endianness.LittleEndian, false);
        }

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
