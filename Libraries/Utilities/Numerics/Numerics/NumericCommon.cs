using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Numerics.Properties;

namespace AllenCopeland.Abstraction.Numerics
{
    public static class NumericExtensions
    {
        internal static byte[] bitCounts = LoadBitLookup();

        private static byte[] LoadBitLookup()
        {
            byte[] result = new byte[ushort.MaxValue + 1];
            using (GZipStream gzStream = new GZipStream(new MemoryStream(Resources.BitCounts, false), CompressionMode.Decompress))
                gzStream.Read(result, 0, result.Length);
            return result;
        }

        public static byte CountBits(this ushort target)
        {
            return ((byte)(bitCounts[target]));
        }
        public static byte CountBits(this short target)
        {
            return ((byte)(bitCounts[target]));
        }
        public static byte CountBits(this uint target)
        {
            return ((byte)(
                bitCounts[target & 0xFFFF] +
                bitCounts[(target >> 0x10) & 0xFFFF]));
        }
        public static byte CountBits(this int target)
        {
            return ((byte)(
                bitCounts[target & 0xFFFF] +
                bitCounts[(target >> 0x10) & 0xFFFF]));
        }
        public static byte CountBits(this ulong target)
        {
            return ((byte)(
                bitCounts[((int)(target & 0xFFFF))] +
                bitCounts[((int)((target >> 0x10) & 0xFFFF))] +
                bitCounts[((int)((target >> 0x20) & 0xFFFF))] +
                bitCounts[((int)((target >> 0x30) & 0xFFFF))]));
        }
        public static byte CountBits(this long target)
        {
            return ((byte)(
                bitCounts[((int)(target & 0xFFFF))] +
                bitCounts[((int)((target >> 0x10) & 0xFFFF))] +
                bitCounts[((int)((target >> 0x20) & 0xFFFF))] +
                bitCounts[((int)((target >> 0x30) & 0xFFFF))]));
        }

        public static byte CountBits(this byte value)
        {
            return bitCounts[value];
        }

        public static byte CountBits(this sbyte value)
        {
            return bitCounts[unchecked((byte)value)];
        }


        public static ulong LeftShift(ulong value, int shift)
        {
            return value << shift;
        }

        public static long LeftShift(long value, int shift)
        {
            return value << shift;
        }

        public static uint LeftShift(uint value, int shift)
        {
            return value << shift;
        }

        public static int LeftShift(int value, int shift)
        {
            return value << shift;
        }

        public static ulong RightShift(ulong value, int shift)
        {
            return value >> shift;
        }

        public static long RightShift(long value, int shift)
        {
            return value >> shift;
        }

        public static uint RightShift(uint value, int shift)
        {
            return value >> shift;
        }

        public static int RightShift(int value, int shift)
        {
            return value >> shift;
        }

        public static long RotL(this long value, byte numberOfBits)
        {
            return (value << numberOfBits) | value >> (64 - numberOfBits);
        }

        public static long RotR(this long value, byte numberOfBits)
        {
            return (value >> numberOfBits) | value << (64 - numberOfBits);
        }

        public static ulong RotL(this ulong value, byte numberOfBits)
        {
            return (value << numberOfBits) | value >> (64 - numberOfBits);
        }

        public static ulong RotR(this ulong value, byte numberOfBits)
        {
            return (value >> numberOfBits) | value << (64 - numberOfBits);
        }

        public static int RotL(this int value, byte numberOfBits)
        {
            return (value << numberOfBits) | value >> (32 - numberOfBits);
        }

        public static int RotR(this int value, byte numberOfBits)
        {
            return (value >> numberOfBits) | value << (32 - numberOfBits);
        }

        public static uint RotL(this uint value, byte numberOfBits)
        {
            return (value << numberOfBits) | value >> (32 - numberOfBits);
        }

        public static uint RotR(this uint value, byte numberOfBits)
        {
            return (value >> numberOfBits) | value << (32 - numberOfBits);
        }

        public static short RotL(this short value, byte numberOfBits)
        {
            return (short)((value << numberOfBits) | (value >> (16 - numberOfBits)));
        }

        public static short RotR(this short value, byte numberOfBits)
        {
            return (short)((value >> numberOfBits) | value << (16 - numberOfBits));
        }

        public static ushort RotL(this ushort value, byte numberOfBits)
        {
            return (ushort)((value << numberOfBits) | value >> (16 - numberOfBits));
        }

        public static ushort RotR(this ushort value, byte numberOfBits)
        {
            return (ushort)((value >> numberOfBits) | value << (16 - numberOfBits));
        }

        public static byte RotL(this byte value, byte numberOfBits)
        {
            return (byte)((value << numberOfBits) | value >> (8 - numberOfBits));
        }

        public static byte RotR(this byte value, byte numberOfBits)
        {
            return (byte)((value >> numberOfBits) | value << (8 - numberOfBits));
        }

    }
}
