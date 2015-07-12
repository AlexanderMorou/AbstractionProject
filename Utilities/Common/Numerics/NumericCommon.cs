using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Properties;

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

        /// <summary>
        /// The current system's <see cref="Endianness"/> value based off of
        /// <see cref="BitConverter.IsLittleEndian"/>.
        /// </summary>
        public static readonly Endianness SystemEndianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;

        public static unsafe float EndianChange(this float value, Endianness end, bool bitEndianness = false)
        {
            uint ivalue = (*(uint*)&value).EndianChange(end, bitEndianness);
            return *(float*)&ivalue;
        }

        public static unsafe double EndianChange(this double value, Endianness end, bool bitEndianness = false)
        {
            ulong ivalue = (*(ulong*)&value).EndianChange(end, bitEndianness);
            return *(double*)&ivalue;
        }

        public static int EndianChange(this int value, Endianness end, bool bitEndianness = false)
        {
            return value.EndianChange(SystemEndianness, end, bitEndianness);
        }

        public static uint EndianChange(this uint value, Endianness end, bool bitEndianness = false)
        {
            return value.EndianChange(SystemEndianness, end, bitEndianness);
        }

        public static long EndianChange(this long value, Endianness end, bool bitEndianness = false)
        {
            return value.EndianChange(SystemEndianness, end, bitEndianness);
        }
        public static ulong EndianChange(this ulong value, Endianness end, bool bitEndianness = false)
        {
            return value.EndianChange(SystemEndianness, end, bitEndianness);
        }
        public static ushort EndianChange(this ushort value, Endianness end, bool bitEndianness = false)
        {
            return value.EndianChange(SystemEndianness, end, bitEndianness);
        }
        public static short EndianChange(this short value, Endianness end, bool bitEndianness = false)
        {
            return value.EndianChange(SystemEndianness, end, bitEndianness);
        }
        public static long EndianChange(this long value, Endianness start, Endianness end, bool bitEndianess = false)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            ulong rValue = unchecked((ulong) value);
            if (bitEndianess)
            {
                return (long)
                    ((rValue & 0x0000000000000001) << 63 |
                     (rValue & 0x0000000000000002) << 61 |
                     (rValue & 0x0000000000000004) << 59 |
                     (rValue & 0x0000000000000008) << 57 |
                     (rValue & 0x0000000000000010) << 55 |
                     (rValue & 0x0000000000000020) << 53 |
                     (rValue & 0x0000000000000040) << 51 |
                     (rValue & 0x0000000000000080) << 49 |
                     (rValue & 0x0000000000000100) << 47 |
                     (rValue & 0x0000000000000200) << 45 |
                     (rValue & 0x0000000000000400) << 43 |
                     (rValue & 0x0000000000000800) << 41 |
                     (rValue & 0x0000000000001000) << 39 |
                     (rValue & 0x0000000000002000) << 37 |
                     (rValue & 0x0000000000004000) << 35 |
                     (rValue & 0x0000000000008000) << 33 |
                     (rValue & 0x0000000000010000) << 31 |
                     (rValue & 0x0000000000020000) << 29 |
                     (rValue & 0x0000000000040000) << 27 |
                     (rValue & 0x0000000000080000) << 25 |
                     (rValue & 0x0000000000100000) << 23 |
                     (rValue & 0x0000000000200000) << 21 |
                     (rValue & 0x0000000000400000) << 19 |
                     (rValue & 0x0000000000800000) << 17 |
                     (rValue & 0x0000000001000000) << 15 |
                     (rValue & 0x0000000002000000) << 13 |
                     (rValue & 0x0000000004000000) << 11 |
                     (rValue & 0x0000000008000000) << 09 |
                     (rValue & 0x0000000010000000) << 07 |
                     (rValue & 0x0000000020000000) << 05 |
                     (rValue & 0x0000000040000000) << 03 |
                     (rValue & 0x0000000080000000) << 01 |
                     (rValue & 0x0000000100000000) >> 01 |
                     (rValue & 0x0000000200000000) >> 03 |
                     (rValue & 0x0000000400000000) >> 05 |
                     (rValue & 0x0000000800000000) >> 07 |
                     (rValue & 0x0000001000000000) >> 09 |
                     (rValue & 0x0000002000000000) >> 11 |
                     (rValue & 0x0000004000000000) >> 13 |
                     (rValue & 0x0000008000000000) >> 15 |
                     (rValue & 0x0000010000000000) >> 17 |
                     (rValue & 0x0000020000000000) >> 19 |
                     (rValue & 0x0000040000000000) >> 21 |
                     (rValue & 0x0000080000000000) >> 23 |
                     (rValue & 0x0000100000000000) >> 25 |
                     (rValue & 0x0000200000000000) >> 27 |
                     (rValue & 0x0000400000000000) >> 29 |
                     (rValue & 0x0000800000000000) >> 31 |
                     (rValue & 0x0001000000000000) >> 33 |
                     (rValue & 0x0002000000000000) >> 35 |
                     (rValue & 0x0004000000000000) >> 37 |
                     (rValue & 0x0008000000000000) >> 39 |
                     (rValue & 0x0010000000000000) >> 41 |
                     (rValue & 0x0020000000000000) >> 43 |
                     (rValue & 0x0040000000000000) >> 45 |
                     (rValue & 0x0080000000000000) >> 47 |
                     (rValue & 0x0100000000000000) >> 49 |
                     (rValue & 0x0200000000000000) >> 51 |
                     (rValue & 0x0400000000000000) >> 53 |
                     (rValue & 0x0800000000000000) >> 55 |
                     (rValue & 0x1000000000000000) >> 57 |
                     (rValue & 0x2000000000000000) >> 59 |
                     (rValue & 0x4000000000000000) >> 61 |
                     (rValue & 0x8000000000000000) >> 63);
            }
            else
                return
                    (long) ((rValue & 0x00000000000000FF) << 56 |
                           (rValue & 0x000000000000FF00) << 40 |
                           (rValue & 0x0000000000FF0000) << 24 |
                           (rValue & 0x00000000FF000000) << 08 |
                           (rValue & 0x000000FF00000000) >> 08 |
                           (rValue & 0x0000FF0000000000) >> 24 |
                           (rValue & 0x00FF000000000000) >> 40 |
                           (rValue & 0xFF00000000000000) >> 56);
        }
        public static ulong EndianChange(this ulong value, Endianness start, Endianness end, bool bitEndianess = false)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            if (bitEndianess)
            {
                return
                    ((value & 0x0000000000000001) << 63 |
                     (value & 0x0000000000000002) << 61 |
                     (value & 0x0000000000000004) << 59 |
                     (value & 0x0000000000000008) << 57 |
                     (value & 0x0000000000000010) << 55 |
                     (value & 0x0000000000000020) << 53 |
                     (value & 0x0000000000000040) << 51 |
                     (value & 0x0000000000000080) << 49 |
                     (value & 0x0000000000000100) << 47 |
                     (value & 0x0000000000000200) << 45 |
                     (value & 0x0000000000000400) << 43 |
                     (value & 0x0000000000000800) << 41 |
                     (value & 0x0000000000001000) << 39 |
                     (value & 0x0000000000002000) << 37 |
                     (value & 0x0000000000004000) << 35 |
                     (value & 0x0000000000008000) << 33 |
                     (value & 0x0000000000010000) << 31 |
                     (value & 0x0000000000020000) << 29 |
                     (value & 0x0000000000040000) << 27 |
                     (value & 0x0000000000080000) << 25 |
                     (value & 0x0000000000100000) << 23 |
                     (value & 0x0000000000200000) << 21 |
                     (value & 0x0000000000400000) << 19 |
                     (value & 0x0000000000800000) << 17 |
                     (value & 0x0000000001000000) << 15 |
                     (value & 0x0000000002000000) << 13 |
                     (value & 0x0000000004000000) << 11 |
                     (value & 0x0000000008000000) << 09 |
                     (value & 0x0000000010000000) << 07 |
                     (value & 0x0000000020000000) << 05 |
                     (value & 0x0000000040000000) << 03 |
                     (value & 0x0000000080000000) << 01 |
                     (value & 0x0000000100000000) >> 01 |
                     (value & 0x0000000200000000) >> 03 |
                     (value & 0x0000000400000000) >> 05 |
                     (value & 0x0000000800000000) >> 07 |
                     (value & 0x0000001000000000) >> 09 |
                     (value & 0x0000002000000000) >> 11 |
                     (value & 0x0000004000000000) >> 13 |
                     (value & 0x0000008000000000) >> 15 |
                     (value & 0x0000010000000000) >> 17 |
                     (value & 0x0000020000000000) >> 19 |
                     (value & 0x0000040000000000) >> 21 |
                     (value & 0x0000080000000000) >> 23 |
                     (value & 0x0000100000000000) >> 25 |
                     (value & 0x0000200000000000) >> 27 |
                     (value & 0x0000400000000000) >> 29 |
                     (value & 0x0000800000000000) >> 31 |
                     (value & 0x0001000000000000) >> 33 |
                     (value & 0x0002000000000000) >> 35 |
                     (value & 0x0004000000000000) >> 37 |
                     (value & 0x0008000000000000) >> 39 |
                     (value & 0x0010000000000000) >> 41 |
                     (value & 0x0020000000000000) >> 43 |
                     (value & 0x0040000000000000) >> 45 |
                     (value & 0x0080000000000000) >> 47 |
                     (value & 0x0100000000000000) >> 49 |
                     (value & 0x0200000000000000) >> 51 |
                     (value & 0x0400000000000000) >> 53 |
                     (value & 0x0800000000000000) >> 55 |
                     (value & 0x1000000000000000) >> 57 |
                     (value & 0x2000000000000000) >> 59 |
                     (value & 0x4000000000000000) >> 61 |
                     (value & 0x8000000000000000) >> 63);
            }
            else
            {
                return
                    (value & 0x00000000000000FF) << 56 |
                    (value & 0x000000000000FF00) << 40 |
                    (value & 0x0000000000FF0000) << 24 |
                    (value & 0x00000000FF000000) << 08 |
                    (value & 0x000000FF00000000) >> 08 |
                    (value & 0x0000FF0000000000) >> 24 |
                    (value & 0x00FF000000000000) >> 40 |
                    (value & 0xFF00000000000000) >> 56;
            }
        }

        public static int EndianChange(this int value, Endianness start, Endianness end, bool bitEndianness = false)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            if (bitEndianness)
            {
                return (int)
                      ((value & 0x00000001U) << 31 |
                       (value & 0x00000002U) << 29 |
                       (value & 0x00000004U) << 27 |
                       (value & 0x00000008U) << 25 |
                       (value & 0x00000010U) << 23 |
                       (value & 0x00000020U) << 21 |
                       (value & 0x00000040U) << 19 |
                       (value & 0x00000080U) << 17 |
                       (value & 0x00000100U) << 15 |
                       (value & 0x00000200U) << 13 |
                       (value & 0x00000400U) << 11 |
                       (value & 0x00000800U) << 09 |
                       (value & 0x00001000U) << 07 |
                       (value & 0x00002000U) << 05 |
                       (value & 0x00004000U) << 03 |
                       (value & 0x00008000U) << 01 |
                       (value & 0x00010000U) >> 01 |
                       (value & 0x00020000U) >> 03 |
                       (value & 0x00040000U) >> 05 |
                       (value & 0x00080000U) >> 07 |
                       (value & 0x00100000U) >> 09 |
                       (value & 0x00200000U) >> 11 |
                       (value & 0x00400000U) >> 13 |
                       (value & 0x00800000U) >> 15 |
                       (value & 0x01000000U) >> 17 |
                       (value & 0x02000000U) >> 19 |
                       (value & 0x04000000U) >> 21 |
                       (value & 0x08000000U) >> 23 |
                       (value & 0x10000000U) >> 25 |
                       (value & 0x20000000U) >> 27 |
                       (value & 0x40000000U) >> 29 |
                       (value & 0x80000000U) >> 31);
            }
            else
                return
                     (int) ((value & 0x000000FF) << 24) |
                           ((value & 0x0000FF00) << 08) |
                    ((int) ((value & 0x00FF0000) >> 08)) |
                    ((int) ((value & 0xFF000000) >> 24));
        }

        public static uint EndianChange(this uint value, Endianness start, Endianness end, bool bitEndianness = false)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            if (bitEndianness)
            {
                return (uint)
                      ((value & 0x00000001U) << 31 |
                       (value & 0x00000002U) << 29 |
                       (value & 0x00000004U) << 27 |
                       (value & 0x00000008U) << 25 |
                       (value & 0x00000010U) << 23 |
                       (value & 0x00000020U) << 21 |
                       (value & 0x00000040U) << 19 |
                       (value & 0x00000080U) << 17 |
                       (value & 0x00000100U) << 15 |
                       (value & 0x00000200U) << 13 |
                       (value & 0x00000400U) << 11 |
                       (value & 0x00000800U) << 09 |
                       (value & 0x00001000U) << 07 |
                       (value & 0x00002000U) << 05 |
                       (value & 0x00004000U) << 03 |
                       (value & 0x00008000U) << 01 |
                       (value & 0x00010000U) >> 01 |
                       (value & 0x00020000U) >> 03 |
                       (value & 0x00040000U) >> 05 |
                       (value & 0x00080000U) >> 07 |
                       (value & 0x00100000U) >> 09 |
                       (value & 0x00200000U) >> 11 |
                       (value & 0x00400000U) >> 13 |
                       (value & 0x00800000U) >> 15 |
                       (value & 0x01000000U) >> 17 |
                       (value & 0x02000000U) >> 19 |
                       (value & 0x04000000U) >> 21 |
                       (value & 0x08000000U) >> 23 |
                       (value & 0x10000000U) >> 25 |
                       (value & 0x20000000U) >> 27 |
                       (value & 0x40000000U) >> 29 |
                       (value & 0x80000000U) >> 31);
            }
            else
                return
                    (uint) ((value & 0x000000FF) << 24) |
                           ((value & 0x0000FF00) << 08) |
                           ((value & 0x00FF0000) >> 08) |
                           ((value & 0xFF000000) >> 24);
        }

        public static ushort EndianChange(this ushort value, Endianness start, Endianness end, bool bitEndianness = false)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            if (bitEndianness)
            {
                return (ushort) ((value & 0x0001U) << 15 |
                       (value & 0x0002U) << 13 |
                       (value & 0x0004U) << 11 |
                       (value & 0x0008U) << 09 |
                       (value & 0x0010U) << 07 |
                       (value & 0x0020U) << 05 |
                       (value & 0x0040U) << 03 |
                       (value & 0x0080U) << 01 |
                       (value & 0x0100U) >> 01 |
                       (value & 0x0200U) >> 03 |
                       (value & 0x0400U) >> 05 |
                       (value & 0x0800U) >> 07 |
                       (value & 0x1000U) >> 09 |
                       (value & 0x2000U) >> 11 |
                       (value & 0x4000U) >> 13 |
                       (value & 0x8000U) >> 15);
            }
            else
                return (ushort) ((value & 0x00FF) << 8 | (value & 0xFF00) >> 8);
        }
        public static short EndianChange(this short value, Endianness start, Endianness end, bool bitEndianness = false)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            if (bitEndianness)
            {
                return (short) ((value & 0x0001U) << 15 |
                       (value & 0x0002U) << 13 |
                       (value & 0x0004U) << 11 |
                       (value & 0x0008U) << 09 |
                       (value & 0x0010U) << 07 |
                       (value & 0x0020U) << 05 |
                       (value & 0x0040U) << 03 |
                       (value & 0x0080U) << 01 |
                       (value & 0x0100U) >> 01 |
                       (value & 0x0200U) >> 03 |
                       (value & 0x0400U) >> 05 |
                       (value & 0x0800U) >> 07 |
                       (value & 0x1000U) >> 09 |
                       (value & 0x2000U) >> 11 |
                       (value & 0x4000U) >> 13 |
                       (value & 0x8000U) >> 15);
            }
            else
                return (short) ((value & 0x00FF) << 8 | (value & 0xFF00) >> 8);
        }
        /// <summary>
        /// Changes the bit endianness of the <paramref name="value"/> from
        /// the <paramref name="start"/> <see cref="Endianness"/>
        /// to the <paramref name="end"/> <see cref="Endianness"/>.
        /// </summary>
        /// <param name="value">The <see cref="Byte"/> value to reverse the bits of.</param>
        /// <param name="start">The originating <see cref="Endianness"/> of <paramref name="value"/>.</param>
        /// <param name="end">The ending <see cref="Endianness"/> of <paramref name="value"/>.</param>
        /// <returns>A <see cref="Byte"/> value which has the bits reversed based off of
        /// <paramref name="start"/> and <paramref name="end"/></returns>
        public static byte EndianChange(this byte value, Endianness start, Endianness end)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            return (byte) ((value & 0x01U) << 7 |
                    (value & 0x02U) << 5 |
                    (value & 0x04U) << 3 |
                    (value & 0x08U) << 1 |
                    (value & 0x10U) >> 1 |
                    (value & 0x20U) >> 3 |
                    (value & 0x40U) >> 5 |
                    (value & 0x80U) >> 7);
        }
        /// <summary>
        /// Changes the bit endianness of the <paramref name="value"/> from
        /// the <paramref name="start"/> <see cref="Endianness"/>
        /// to the <paramref name="end"/> <see cref="Endianness"/>.
        /// </summary>
        /// <param name="value">The <see cref="SByte"/> value to reverse the bits of.</param>
        /// <param name="start">The originating <see cref="Endianness"/> of <paramref name="value"/>.</param>
        /// <param name="end">The ending <see cref="Endianness"/> of <paramref name="value"/>.</param>
        /// <returns>A <see cref="SByte"/> value which has the bits reversed based off of
        /// <paramref name="start"/> and <paramref name="end"/></returns>
        public static sbyte EndianChange(this sbyte value, Endianness start, Endianness end)
        {
            if (start != Endianness.LittleEndian && start != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("start");
            if (end != Endianness.LittleEndian && end != Endianness.BigEndian)
                throw new ArgumentOutOfRangeException("end");
            if (start == end)
                return value;
            return (sbyte) ((value & 0x01U) << 7 |
                    (value & 0x02U) << 5 |
                    (value & 0x04U) << 3 |
                    (value & 0x08U) << 1 |
                    (value & 0x10U) >> 1 |
                    (value & 0x20U) >> 3 |
                    (value & 0x40U) >> 5 |
                    (value & 0x80U) >> 7);
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

    }
}
