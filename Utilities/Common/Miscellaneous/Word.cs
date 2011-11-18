using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AllenCopeland.Abstraction.Utilities.Miscellaneous
{
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct Word
    {
        public static readonly Word MaxValue = ushort.MaxValue;
        [FieldOffset(0)]
        internal readonly ushort _value;
        [FieldOffset(0)]
        public readonly byte LoByte;
        [FieldOffset(1)]
        public readonly byte HiByte;

        public Word(byte loByte, byte hiByte)
            : this((ushort)((ushort)loByte | (ushort)(((ushort)hiByte) << 8)))
        {
        }

        public unsafe Word(ushort value)
        {
            this = *(ushort*)&value;
        }

        public static unsafe implicit operator Word(ushort value)
        {
            return *(Word*)&value;
        }

        public static unsafe implicit operator ushort(Word value)
        {
            return value._value;
        }
    }
}
