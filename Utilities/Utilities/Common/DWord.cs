using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AllenCopeland.Abstraction.Utilities.Common
{
    [StructLayout(LayoutKind.Explicit, Size=4)]
    public struct DWord
    {
        [FieldOffset(0)]
        internal readonly uint _value;
        [FieldOffset(0)]
        public readonly byte Byte0;
        [FieldOffset(1)]
        public readonly byte Byte1;
        [FieldOffset(2)]
        public readonly byte Byte2;
        [FieldOffset(3)]
        public readonly byte Byte3;

        [FieldOffset(0)]
        public readonly Word LoWord;
        [FieldOffset(0)]
        public readonly ushort loWord;
        [FieldOffset(2)]
        public readonly Word HiWord;
        [FieldOffset(2)]
        public readonly ushort hiWord;

        public unsafe DWord(byte byte0, byte byte1, byte byte2, byte byte3)
            : this((uint)byte0 | ((uint)byte1 << 8) | ((uint)byte2 << 16) | ((uint)byte3 << 24))
        {
        }

        public unsafe DWord(ushort loWord, ushort hiWord)
            : this((uint)loWord | ((uint)(hiWord)) << 16)
        {
        }

        public unsafe DWord(Word loWord, Word hiWord)
            : this()
        {
            this.LoWord = loWord;
            this.HiWord = hiWord;
        }

        public unsafe DWord(uint value)
        {
            this = *(DWord*)&value;
        }

        public static unsafe implicit operator DWord(uint value)
        {
            return *(DWord*)&value;
        }

        public static unsafe implicit operator uint(DWord value)
        {
            return value._value;
        }

    }
}
