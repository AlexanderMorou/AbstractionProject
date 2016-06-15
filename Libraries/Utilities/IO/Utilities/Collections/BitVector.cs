using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using AllenCopeland.Abstraction.Numerics;
#if x86
using SlotType = System.UInt32;
#elif x64
using SlotType = System.UInt64;
#endif
namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public class BitVector
    {
#if x86
        private const int SlotShiftIndex = 0x5;
        private const uint SlotShiftMask = ~0U;
        private const uint SlotFlagValue = 0x1;
#elif x64
        private const int SlotShiftIndex = 0x6;
        private const SlotType SlotShiftMask = ~0UL;
        private const SlotType SlotFlagValue = 0x1U;
#endif
        private const int ByteShiftIndex = 0x3;
        private const int ModulusByteAlignment = 0x8 - 1;
        private const int ModulusSlotBitSize = (int) SlotBitSize - 1;
        private SlotType[] data;
        private const uint SlotBitSize = (uint) SlotFlagValue << SlotShiftIndex;
        private ulong length;

        public ulong TrueCount()
        {
            ulong bitCount = 0;
            for (int i = 0; i < this.data.Length; i++)
            {
                SlotType value = this.data[i];
#if x86
                /* *
                 * Select out the hi and lo words used to retrieve
                 * information from the cache.
                 * */
                ushort loword = (ushort) (value & 0xFFFF);
                ushort hiword = (ushort) ((value >> (sizeof(ushort) * 8)));
                bitCount += (ulong)(NumericExtensions.bitCounts[loword] + NumericExtensions.bitCounts[hiword]);
#elif x64
                //Select out the data in uints
                uint loDWord = (uint) (value & 0xFFFFFFFF);
                uint hiDWord = (uint) (value >> 32);
                /* *
                 * Select out the individual words used to retrieve
                 * information from the cache.
                 * */
                ushort loDWordLoWord = (ushort) (loDWord & 0xFFFF);
                ushort loDWordHiWord = (ushort) ((loDWord >> (sizeof(ushort) * 8)));
                ushort hiDWordLoWord = (ushort) (hiDWord & 0xFFFF);
                ushort hiDWordHiWord = (ushort) ((hiDWord >> (sizeof(ushort) * 8)));
                bitCount += (ulong) (NumericExtensions.bitCounts[loDWordLoWord] + NumericExtensions.bitCounts[loDWordHiWord] + NumericExtensions.bitCounts[hiDWordLoWord] + NumericExtensions.bitCounts[hiDWordHiWord]);
#endif
            }
            return bitCount;
        }

        /// <summary>
        /// Creates a new <see cref="BitVector"/> instance which
        /// contains the <paramref name="bitSize"/> provided.
        /// </summary>
        /// <param name="bitSize">The size of the <see cref="BitVector"/>
        /// in bits.</param>
        public BitVector(ulong bitSize)
        {
            InitializeWithBitSize(bitSize);
        }

        private void InitializeWithBitSize(ulong bitSize)
        {
            this.length = bitSize;
            this.data = new SlotType[(length + SlotBitSize - 1) >> SlotShiftIndex];
        }

        public unsafe BitVector(byte[] bits)
        {
            this.InitializeWithBitSize((ulong) bits.Length * (1 << ByteShiftIndex));
            fixed (byte* bitsPtr = bits)
            {
                int length;
                if ((bits.Length & 7) == 0)
                {
                    length = bits.Length >> 3;
                    fixed (SlotType* slotPtr = this.data)
                    {
                        ulong* actualSlotPtr = (ulong*) slotPtr;
                        ulong* actualBitsPtr = (ulong*) bitsPtr;
                        for (int i = 0; i < length; i++)
                        {
                            *actualSlotPtr = *actualBitsPtr;
                            actualSlotPtr++;
                            actualBitsPtr++;
                        }
                    }
                }
                else if ((bits.Length & 3) == 0)
                {
                    length = bits.Length >> 2;
                    fixed (SlotType* slotPtr = this.data)
                    {
                        uint* actualSlotPtr = (uint*) slotPtr;
                        uint* actualBitsPtr = (uint*) bitsPtr;
                        for (int i = 0; i < length; i++)
                        {
                            *actualSlotPtr = *actualBitsPtr;
                            actualSlotPtr++;
                            actualBitsPtr++;
                        }
                    }
                }
                else if ((bits.Length & 1) == 0)
                {
                    length = bits.Length >> 1;
                    fixed (SlotType* slotPtr = this.data)
                    {
                        ushort* actualSlotPtr = (ushort*) slotPtr;
                        ushort* actualBitsPtr = (ushort*) bitsPtr;
                        for (int i = 0; i < length; i++)
                        {
                            *actualSlotPtr = *actualBitsPtr;
                            actualSlotPtr++;
                            actualBitsPtr++;
                        }
                    }
                }
                else
                {
                    fixed (SlotType* slotPtr = this.data)
                    {
                        length = bits.Length;
                        byte* actualSlotPtr = (byte*) slotPtr;
                        byte* actualBitsPtr = bitsPtr;
                        for (int i = 0; i < length; i++)
                        {
                            *actualSlotPtr = *actualBitsPtr;
                            actualSlotPtr++;
                            actualBitsPtr++;
                        }
                    }
                }
            }
        }



        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="SByte"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (8), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(ulong offset, sbyte value)
        {
            this.Set(offset, (byte) value);
        }
        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="SByte"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (8), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, sbyte value)
        {
            this.Set((ulong) offset, (byte) value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Int32"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (32), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, int value)
        {
            this.Set((ulong) offset, (uint) value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Int32"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (32), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(ulong offset, int value)
        {
            this.Set(offset, (uint) value);
        }
        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="UInt16"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (16), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, ushort value)
        {
            this.Set((ulong) offset, (ushort) value);
        }
        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Int16"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (16), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(ulong offset, short value)
        {
            this.Set((ulong) offset, (ushort) value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Int16"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (16), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, short value)
        {
            this.Set((ulong) offset, (short) value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="UInt32"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (32), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, uint value)
        {
            this.Set((ulong) offset, value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Int64"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (64), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(ulong offset, long value)
        {
            this.Set(offset, (ulong) value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="UInt64"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (64), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, ulong value)
        {
            this.Set((ulong) offset, value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Int64"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (64), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, long value)
        {
            this.Set((ulong) offset, (ulong) value);
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Int64"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (64), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe void Set(ulong offset, ulong value)
        {
            const int dataBitSize = sizeof(ulong) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            /* *
             * Aligned to an actual byte.
             * */
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    ulong* uLongPtr = (ulong*) realPtr;
                    *uLongPtr = value;
                }
            }
            else
            {
#if x86
                /* *
                 * When the size of the data is larger than
                 * the slots, it's simpler to code as two reads.
                 * */
                this.Set(offset, (uint) (
                    /* LoDWord */ value & 0x00000000FFFFFFFFUL));
                this.Set(offset + SlotBitSize, (uint) ((
                    /* HiDWord */ value & 0xFFFFFFFF00000000UL) >> 32));
#elif x64
                /* *
                 * When the size of the data is just right,
                 * 
                 * */
                ulong realOffset = offset >> SlotShiftIndex;
                var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                /* *
                 * Setup masks to zero out the bits that are currently
                 * set, which overlap the data of the incoming value.
                 * */
                var leftMask = ~0UL << (int) overflow;
                var rightMask = ~0UL >> dataBitSize - (int) overflow;
                data[realOffset] &= rightMask;
                data[realOffset + 1] &= leftMask;
                /* *
                 * Or in the value.
                 * */
                data[realOffset] |= value << (int) SlotBitSize - (dataBitSize - (int) overflow);
                data[realOffset + 1] |= value >> (int) (dataBitSize - overflow);
#endif
            }
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="UInt32"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (32), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe void Set(ulong offset, uint value)
        {
            const int dataBitSize = sizeof(uint) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    uint* uIntPtr = (uint*) realPtr;
                    *uIntPtr = value;
                }
            }
            else
            {
#if x86
                ulong realOffset = offset >> SlotShiftIndex;
                var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                var leftMask = SlotShiftMask << (int) overflow;
                var rightMask = SlotShiftMask >> dataBitSize - (int) overflow;
                data[realOffset] &= rightMask;
                data[realOffset + 1] &= leftMask;
                data[realOffset] |= (SlotType) ((SlotType) value) << (int) SlotBitSize - (dataBitSize - (int) overflow);
                data[realOffset + 1] |= (SlotType) value >> (int) (dataBitSize - overflow);
#elif x64
                ulong realOffset = offset >> SlotShiftIndex;
                ulong realEnd = (realOffset * SlotBitSize) + SlotBitSize;

                if (offset + dataBitSize > realEnd)
                {
                    var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                    var leftMask = SlotShiftMask << (int) overflow;
                    var rightMask = SlotShiftMask >> dataBitSize - (int) overflow;
                    data[realOffset] &= rightMask;
                    data[realOffset + 1] &= leftMask;
                    data[realOffset] |= (SlotType) ((SlotType) value) << (int) SlotBitSize - (dataBitSize - (int) overflow);
                    data[realOffset + 1] |= (SlotType) value >> (int) (dataBitSize - overflow);
                }
                else
                {
                    int shift = (int) (offset - (realOffset * SlotBitSize));
                    SlotType uIntMask = (SlotType) SlotShiftMask << shift;
                    data[realOffset] &= ~uIntMask;
                    data[realOffset] |= (((SlotType) value) << shift);
                }
#endif
            }
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="UInt16"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (16), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe void Set(ulong offset, ushort value)
        {
            const int dataBitSize = sizeof(ushort) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    ushort* uShortPtr = (ushort*) realPtr;
                    *uShortPtr = value;
                }
            }
            else
            {

                ulong realOffset = offset >> SlotShiftIndex;
                ulong realEnd = (realOffset * SlotBitSize) + SlotBitSize;

                if (offset + dataBitSize > realEnd)
                {
                    var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                    var leftMask = SlotShiftMask << (int) overflow;
                    var rightMask = SlotShiftMask >> dataBitSize - (int) overflow;
                    data[realOffset] &= rightMask;
                    data[realOffset + 1] &= leftMask;
                    data[realOffset] |= (SlotType) ((SlotType) value) << (int) SlotBitSize - (dataBitSize - (int) overflow);
                    data[realOffset + 1] |= (SlotType) value >> (int) (dataBitSize - overflow);
                }
                else
                {
                    int shift = (int) (offset - (realOffset * SlotBitSize));
                    SlotType ushortMask = (SlotType) unchecked((ushort) SlotShiftMask) << shift;
                    data[realOffset] &= ~ushortMask;
                    data[realOffset] |= (((SlotType) value) << shift);
                }
            }
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the
        /// <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/>
        /// value denoting where within the <see cref="BitVector"/>
        /// to store the <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="Byte"/>
        /// to store at the <paramref name="offset"/>
        /// provided.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of <paramref name="value"/> (8), is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public void Set(uint offset, byte value)
        {
            this.Set((ulong) offset, value);
        }

        public unsafe void Set(ulong offset, byte value)
        {
            const int dataBitSize = sizeof(byte) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    *realPtr = value;
                }
            }
            else
            {

                ulong realOffset = offset >> SlotShiftIndex;
                ulong realEnd = (realOffset * SlotBitSize) + SlotBitSize;

                if (offset + dataBitSize > realEnd)
                {
                    var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                    var leftMask = SlotShiftMask << (int) overflow;
                    var rightMask = SlotShiftMask >> dataBitSize - (int) overflow;
                    data[realOffset] &= rightMask;
                    data[realOffset + 1] &= leftMask;
                    data[realOffset] |= (SlotType) ((SlotType) value) << (int) SlotBitSize - (dataBitSize - (int) overflow);
                    data[realOffset + 1] |= (SlotType) value >> (int) (dataBitSize - overflow);
                }
                else
                {
                    int shift = (int) (offset - (realOffset * SlotBitSize));
                    SlotType ushortMask = (SlotType) unchecked((byte) SlotShiftMask) << shift;
                    data[realOffset] &= ~ushortMask;
                    data[realOffset] |= (((SlotType) value) << shift);
                }
            }
        }

        public void Set(ulong offset, uint value, byte bitCount)
        {
            if (bitCount > 32)
                throw new ArgumentOutOfRangeException("bitCount");
            this.SetInternal(offset, value, bitCount);
        }

        public void Set(ulong offset, int value, byte bitCount)
        {
            if (bitCount > 32)
                throw new ArgumentOutOfRangeException("bitCount");
            this.SetInternal(offset, (uint) value, bitCount);
        }

        public void Set(ulong offset, ushort value, byte bitCount)
        {
            if (bitCount > 16)
                throw new ArgumentOutOfRangeException("bitCount");
            this.SetInternal(offset, value, bitCount);
        }

        public void Set(ulong offset, short value, byte bitCount)
        {
            if (bitCount > 16)
                throw new ArgumentOutOfRangeException("bitCount");
            this.SetInternal(offset, (ushort) value, bitCount);
        }

        public void Set(ulong offset, byte value, byte bitCount)
        {
            if (bitCount > 8)
                throw new ArgumentOutOfRangeException("bitCount");
            this.SetInternal(offset, value, bitCount);
        }

        public void Set(ulong offset, sbyte value, byte bitCount)
        {
            if (bitCount > 8)
                throw new ArgumentOutOfRangeException("bitCount");
            this.SetInternal(offset, (byte) value, bitCount);
        }

        private unsafe void SetInternal(ulong offset, uint value, byte bitCount)
        {
            if (bitCount < 0 || bitCount > 32)
                throw new ArgumentOutOfRangeException("bitCount");
            if (offset + bitCount > length)
                throw new ArgumentOutOfRangeException("offset");

            ulong realOffset = offset >> SlotShiftIndex;
            ulong realEnd = (realOffset * SlotBitSize) + SlotBitSize;

            if (offset + bitCount > realEnd)
            {
                SlotType realValue = value & ((SlotFlagValue << bitCount) - 1);
                var overflow = (offset + bitCount) & ModulusSlotBitSize;
                var leftMask = SlotShiftMask << (int) overflow;
                var rightMask = SlotShiftMask >> bitCount - (int) overflow;
                data[realOffset] &= rightMask;
                data[realOffset + 1] &= leftMask;
                data[realOffset] |= realValue << (int) SlotBitSize - (bitCount - (int) overflow);
                data[realOffset + 1] |= realValue >> (int) (bitCount - overflow);
            }
            else
            {
                SlotType mask = ((SlotFlagValue << bitCount) - 1);
                SlotType realValue = value & mask;
                int shift = (int) (offset - (realOffset * SlotBitSize));
                SlotType shiftMask = (SlotType) mask << shift;
                data[realOffset] &= ~shiftMask;
                data[realOffset] |= realValue << shift;
            }
        }

        public unsafe void SetNibble(ulong offset, byte value)
        {
            this.Set(offset, value, 4);
        }

        /// <summary>
        /// Obtains the first-four bits of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>A <see cref="Byte"/> containing the four bits
        /// at the <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (4),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe byte GetNibble(ulong offset)
        {
            return GetByteNibbits(offset, 4);
        }

        public unsafe byte GetByteNibbits(ulong offset, byte count)
        {
            if (count >= 8)
                throw new ArgumentOutOfRangeException("count");
            if (offset + count > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    return (byte) ((*realPtr) & ((1 << count) - 1));
                }
            }
            else
            {
                ulong realOffset = offset >> SlotShiftIndex;
                ulong realEnd = (realOffset + 1) * SlotBitSize;
                if (offset + count > realEnd)
                {
                    var overflow = (offset + count) & ModulusSlotBitSize;
                    return (byte) (((this.data[realOffset] >> (int) (SlotBitSize - overflow)) | (this.data[realOffset + 1] << (int) (count - overflow))) & (SlotType) ((1 << count) - 1));
                }
                else
                    return (byte) ((this.data[realOffset] >> (int) (offset & ModulusSlotBitSize)) & (SlotType) ((1 << count) - 1));
            }
        }

        public unsafe ushort GetUInt16Nibbits(ulong offset, byte count)
        {
            if (count > 16)
                throw new ArgumentOutOfRangeException("count");
            if (offset + count > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    ushort* uint16Ptr = (ushort*) realPtr;
                    return (ushort) ((*uint16Ptr) & ((1 << count) - 1));
                }
            }
            else
            {
                ulong realOffset = offset >> SlotShiftIndex;
                ulong realEnd = (realOffset + 1) * SlotBitSize;
                if (offset + count > realEnd)
                {
                    var overflow = (offset + count) & ModulusSlotBitSize;
                    return (ushort) (((this.data[realOffset] >> (int) (SlotBitSize - overflow)) | (this.data[realOffset + 1] << (int) (count - overflow))) & (SlotType) ((1 << count) - 1));
                }
                else
                    return (ushort) ((this.data[realOffset] >> (int) (offset & ModulusSlotBitSize)) & (SlotType) ((1 << count) - 1));
            }
        }

        public unsafe uint GetUInt32Nibbits(ulong offset, byte count)
        {
            if (count >= 32)
                throw new ArgumentOutOfRangeException("count");
            if (offset + count > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    uint* uint32Ptr = (uint*) realPtr;
                    return (uint) ((*uint32Ptr) & ((1 << count) - 1));
                }
            }
            else
            {
                ulong realOffset = offset >> SlotShiftIndex;
                ulong realEnd = (realOffset + 1) * SlotBitSize;
                if (offset + count > realEnd)
                {
                    var overflow = (offset + count) & ModulusSlotBitSize;
                    return (uint) (((this.data[realOffset] >> (int) (SlotBitSize - count + overflow)) | (this.data[realOffset + 1] << (int) (count - overflow))) & (SlotType) ((1 << count) - 1));
                }
                else
                    return (uint) ((this.data[realOffset] >> (int) (offset & ModulusSlotBitSize)) & (SlotType) ((1 << count) - 1));
            }
        }

        /// <summary>
        /// Obtains a <see cref="Byte"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="Byte"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (8),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public sbyte GetSByte(uint offset)
        {
            return (sbyte) this.GetByte((ulong) offset);
        }

        /// <summary>
        /// Obtains an <see cref="SByte"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="SByte"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (8),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public sbyte GetSByte(ulong offset)
        {
            return (sbyte) this.GetByte(offset);
        }

        /// <summary>
        /// Obtains an <see cref="UInt32"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="UInt32"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (8),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public byte GetByte(uint offset)
        {
            return this.GetByte((ulong) offset);
        }

        /// <summary>
        /// Obtains an <see cref="UInt64"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="UInt64"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (8),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe byte GetByte(ulong offset)
        {
            const int dataBitSize = sizeof(byte) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    return (byte) *realPtr;
                }
            }
            else
            {
                ulong realOffset = offset >> SlotShiftIndex;
                ulong realEnd = (realOffset + 1) * SlotBitSize;
                if (offset + dataBitSize > realEnd)
                {
                    var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                    return (byte) (((this.data[realOffset] >> (int) (SlotBitSize - dataBitSize + overflow)) | (this.data[realOffset + 1] << (int) (dataBitSize - overflow))) & 0xFF);
                }
                else
                    return (byte) ((this.data[realOffset] >> (int) (offset & ModulusSlotBitSize)) & 0xFF);
            }
        }

        /// <summary>
        /// Obtains an <see cref="UInt16"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="UInt16"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (16),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe ushort GetUInt16(ulong offset)
        {
            const int dataBitSize = sizeof(ushort) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    ushort* uInt16Ptr = (ushort*) realPtr;
                    return (ushort) *uInt16Ptr;
                }
            }
            else
            {
                int realOffset = (int) (offset >> SlotShiftIndex);
                ulong realEnd = (ulong) (realOffset + 1) * SlotBitSize;
                if (offset + dataBitSize > realEnd)
                {
                    var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                    return (ushort) (((this.data[realOffset] >> (int) (SlotBitSize - dataBitSize + overflow)) | (this.data[realOffset + 1] << (int) (dataBitSize - overflow))) & 0xFFFF);
                }
                else
                    return (ushort) ((this.data[realOffset] >> (int) (offset & ModulusSlotBitSize)) & 0xFFFF);
            }
        }

        /// <summary>
        /// Obtains an <see cref="Int16"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="Int16"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (16),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe short GetInt16(ulong offset)
        {
            return (short) GetUInt16(offset);
        }

        /// <summary>
        /// Obtains an <see cref="UInt32"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="UInt32"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (32),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe uint GetUInt32(ulong offset)
        {
            const int dataBitSize = sizeof(uint) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    uint* uInt16Ptr = (uint*) realPtr;
                    return (uint) *uInt16Ptr;
                }
            }
            else
            {
#if x86
                int realOffset = (int) (offset >> SlotShiftIndex);
                var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                return (uint) ((this.data[realOffset] >> (int) overflow) | (this.data[realOffset + 1] << (int) (dataBitSize - overflow)));
#elif x64
                int realOffset = (int) (offset >> SlotShiftIndex);
                ulong realEnd = (ulong) (realOffset + 1) * SlotBitSize;
                if (offset + dataBitSize > realEnd)
                {
                    var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                    return (uint) ((((this.data[realOffset] >> (int) (SlotBitSize - dataBitSize + overflow)) | (this.data[realOffset + 1] << (int) (dataBitSize - overflow))))) & 0xFFFFFFFF;
                }
                else
                    return (uint) (((this.data[realOffset] >> (int) (offset & ModulusSlotBitSize)))) & 0xFFFFFFFF;
#endif
            }
        }

        /// <summary>
        /// Obtains an <see cref="Int32"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="Int32"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (32),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe int GetInt32(ulong offset)
        {
            return (int) this.GetUInt32(offset);
        }

        /// <summary>
        /// Obtains an <see cref="UInt64"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="UInt64"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (64),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public unsafe ulong GetUInt64(ulong offset)
        {
#if x86
            const int dataBitSize = sizeof(uint) * 8;
            return GetUInt32(offset) | ((ulong) GetUInt32(offset + dataBitSize)) << dataBitSize;
#elif x64
            const int dataBitSize = sizeof(ulong) * 8;
            if (offset + dataBitSize > length)
                throw new ArgumentOutOfRangeException("offset");
            if ((offset & ModulusByteAlignment) == 0)
            {
                ulong realOffset = offset >> ByteShiftIndex;
                fixed (SlotType* ptr = data)
                {
                    byte* realPtr = (byte*) ptr;
                    realPtr += realOffset;
                    ulong* uInt64Ptr = (ulong*) realPtr;
                    return (ulong) *uInt64Ptr;
                }
            }
            else
            {
                int realOffset = (int) (offset >> SlotShiftIndex);
                var overflow = (offset + dataBitSize) & ModulusSlotBitSize;
                return (ulong) ((this.data[realOffset] >> (int) overflow) | (this.data[realOffset + 1] << (int) (dataBitSize - overflow)));
            }
#endif
        }

        /// <summary>
        /// Obtains an <see cref="Int64"/> of the <paramref name="BitVector"/>
        /// at the <paramref name="offset"/> provided.
        /// </summary>
        /// <param name="offset">The <see cref="UInt64"/> value denoting
        /// where within the <see cref="BitVector"/> to retrieve the value.</param>
        /// <returns>The <see cref="Int64"/> at the 
        /// <paramref name="offset"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="offset"/>, plus the bit-length of value to return (64),
        /// is greater than the <see cref="Length"/> of the 
        /// <see cref="BitVector"/>.</exception>
        public long GetInt64(ulong offset)
        {
            return (long) this.GetUInt64(offset);
        }

        public bool this[ulong offset]
        {
            get
            {
                if (offset >= this.length)
                    throw new ArgumentOutOfRangeException("offset");
                int realIndex = (int) (offset >> SlotShiftIndex);
                return ((this.data[realIndex] & (1UL << (int) (offset & ModulusSlotBitSize))) != 0);
            }
            set
            {
                if (offset >= this.length)
                    throw new ArgumentOutOfRangeException("offset");
                int realIndex = (int) (offset >> SlotShiftIndex);
                if (value)
                    this.data[realIndex] |= (SlotFlagValue << (int) (offset & ModulusSlotBitSize));
                else
                    this.data[realIndex] &= ~(SlotFlagValue << (int) (offset & ModulusSlotBitSize));
            }
        }

        public IEnumerable<byte> GetByteEnumerator(ulong startOffset, ulong endOffset)
        {
            const int dataBitSize = sizeof(byte) * 8;
            if (startOffset + dataBitSize > this.length)
                throw new ArgumentOutOfRangeException("startOffset");
            if (endOffset + dataBitSize > this.length)
                throw new ArgumentOutOfRangeException("endOffset");
            ulong position;
            for (position = startOffset; position <= endOffset - dataBitSize; position += dataBitSize)
                yield return this.GetByte(position);
            if (position != endOffset)
            {
                int remainingBits = (int) (endOffset - position);
                int mask = (int) (unchecked((byte) (SlotShiftMask))) >> (dataBitSize - remainingBits);
                yield return (byte) (this.GetByte(position) & mask);
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt64"/> value denoting the
        /// length of the <see cref="BitVector"/> in bits.
        /// </summary>
        public ulong Length
        {
            get
            {
                return this.length;
            }
            set
            {
                int newCount = (int)((value + SlotBitSize - 1) >> SlotShiftIndex);
                if (newCount > this.data.Length)
                {
                    SlotType[] newSet = new SlotType[newCount];
                    this.data.CopyTo(newSet, 0);
                    this.length = value;
                    this.data = newSet;
                }
            }
        }

        public unsafe byte[] ToByteArray()
        {
            byte[] result = new byte[this.data.Length * (SlotBitSize / 8)];
            fixed (SlotType* dataPtr = this.data)
            {
                fixed (byte* resultPtr = result)
                {
                    SlotType* resultProperPtr = (SlotType*) resultPtr;
                    SlotType* dataProperPtr = dataPtr;
                    for (int i = 0; i < data.Length; i++)
                    {
                        *resultProperPtr = *dataProperPtr;
                        resultProperPtr++;
                        dataProperPtr++;
                    }
                }
            }
            return result;
        }

        public unsafe ushort[] ToUInt16Array()
        {
            ushort[] result = new ushort[this.data.Length * (SlotBitSize / (sizeof(ushort) * 8))];
            fixed (SlotType* dataPtr = this.data)
            {
                fixed (ushort* resultPtr = result)
                {
                    SlotType* resultProperPtr = (SlotType*) resultPtr;
                    SlotType* dataProperPtr = dataPtr;
                    for (int i = 0; i < data.Length; i++)
                    {
                        *resultProperPtr = *dataProperPtr;
                        resultProperPtr++;
                        dataProperPtr++;
                    }
                }
            }
            return result;
        }

        public unsafe uint[] ToUInt32Array()
        {
            uint[] result = new uint[this.data.Length * (SlotBitSize / (sizeof(uint) * 8))];
            fixed (SlotType* dataPtr = this.data)
            {
                fixed (uint* resultPtr = result)
                {
                    SlotType* resultProperPtr = (SlotType*) resultPtr;
                    SlotType* dataProperPtr = dataPtr;
                    for (int i = 0; i < data.Length; i++)
                    {
                        *resultProperPtr = *dataProperPtr;
                        resultProperPtr++;
                        dataProperPtr++;
                    }
                }
            }
            return result;
        }

        public unsafe int[] ToInt32Array()
        {
            int[] result = new int[this.data.Length * (SlotBitSize / (sizeof(int) * 8))];
            fixed (SlotType* dataPtr = this.data)
            {
                fixed (int* resultPtr = result)
                {
                    SlotType* resultProperPtr = (SlotType*) resultPtr;
                    SlotType* dataProperPtr = dataPtr;
                    for (int i = 0; i < data.Length; i++)
                    {
                        *resultProperPtr = *dataProperPtr;
                        resultProperPtr++;
                        dataProperPtr++;
                    }
                }
            }
            return result;
        }

        public byte GetBits(ulong offset, byte mask)
        {
            return (byte) (this.GetByte(offset) & mask);
        }

        public ushort GetBits(ulong offset, ushort mask)
        {
            return (ushort) (this.GetUInt16(offset) & mask);
        }

        public uint GetBits(ulong offset, uint mask)
        {
            return (uint) (this.GetUInt16(offset) & mask);
        }

        public ulong GetBits(ulong offset, ulong mask)
        {
            return (ulong) (this.GetUInt16(offset) & mask);
        }

        public sbyte GetBits(ulong offset, sbyte mask)
        {
            return (sbyte) (this.GetSByte(offset) & mask);
        }

        public short GetBits(ulong offset, short mask)
        {
            return (short) (this.GetInt16(offset) & mask);
        }

        public int GetBits(ulong offset, int mask)
        {
            return (int) (this.GetInt32(offset) & mask);
        }

        public long GetBits(ulong offset, long mask)
        {
            return (long) (this.GetInt64(offset) & mask);
        }

        /* *
         * Used for the Common Type System.
         * */
        internal SlotType[] Data { get { return this.data; } }
    }
}
