using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AllenCopeland.Abstraction.Numerics
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct QWord
    {
        [FieldOffset(0)]
        internal readonly ulong _value;

        [FieldOffset(0)]
        public readonly byte Byte0;
        [FieldOffset(1)]
        public readonly byte Byte1;
        [FieldOffset(2)]
        public readonly byte Byte2;
        [FieldOffset(3)]
        public readonly byte Byte3;
        [FieldOffset(4)]
        public readonly byte Byte4;
        [FieldOffset(5)]
        public readonly byte Byte5;
        [FieldOffset(6)]
        public readonly byte Byte6;
        [FieldOffset(7)]
        public readonly byte Byte7;

        [FieldOffset(0)]
        public readonly Word Word0;
        [FieldOffset(0)]
        private readonly ushort word0;
        [FieldOffset(2)]
        public readonly Word Word1;
        [FieldOffset(2)]
        private readonly ushort word1;
        [FieldOffset(4)]
        public readonly Word Word2;
        [FieldOffset(4)]
        private readonly ushort word2;
        [FieldOffset(6)]
        public readonly Word Word3;
        [FieldOffset(6)]
        private readonly ushort word3;

        [FieldOffset(0)]
        public readonly DWord LoDWord;
        [FieldOffset(0)]
        private readonly uint loDWord;
        [FieldOffset(4)]
        public readonly DWord HiDWord;
        [FieldOffset(4)]
        private readonly uint hiDWord;

        public QWord(byte byte0, byte byte1, byte byte2, byte byte3, byte byte4, byte byte5, byte byte6, byte byte7)
            : this((ulong)((ulong)byte0 | (ulong)byte1 << 8 | (ulong)byte2 << 16 | (ulong)byte3 << 24 | (ulong)byte4 << 32 | (ulong)byte5 << 40 | (ulong)byte6 << 48 | (ulong)byte7 << 56))
        {
        }

        public unsafe QWord(Word loDWordLoWord, Word loDWordHiWord, Word hiDWordLoWord, Word hiDWordHiWord)
            : this((ulong)(loDWordLoWord._value) | ((ulong)loDWordHiWord._value << 16) | ((ulong)hiDWordLoWord._value << 32) | ((ulong)hiDWordHiWord._value << 48))
        {
        }

        public QWord(ushort loDWordLoWord, ushort loDWordHiWord, ushort hiDWordLoWord, ushort hiDWordHiWord)
            : this(loDWordLoWord | ((ulong)loDWordHiWord << 16) | (((ulong)hiDWordLoWord << 32 | (ulong)hiDWordHiWord << 48)))
        {
        }

        public QWord(DWord loDWord, DWord hiDWord)
            : this((ulong)(loDWord._value) | ((ulong)hiDWord._value << 32))
        {
        }

        public QWord(uint loDWord, uint hiDWord)
            : this((ulong)loDWord | ((ulong)hiDWord) << 32)
        {
        }

        public unsafe QWord(ulong value)
        {
            this = *(QWord*)&value;
        }

        public static implicit operator QWord(ulong value)
        {
            return new QWord(value);
        }

        public static implicit operator ulong(QWord value)
        {
            return value._value;
        }
    }
}
