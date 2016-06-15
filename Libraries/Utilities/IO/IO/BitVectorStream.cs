using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AllenCopeland.Abstraction.Utilities.Collections;
#if x86
using SlotType = System.UInt32;
#elif x64
using SlotType = System.UInt64;
#endif

namespace AllenCopeland.Abstraction.IO
{
    public class BitVectorStream :
        Stream
    {
#if x86
        private const int SlotShiftIndex = 0x5;
        private const uint SlotShiftMask = ~0U;
        private const uint SlotFlagValue = 0x1;
        private const int SlotByteCountShiftIndex = 0x3;
#elif x64
        private const int SlotShiftIndex = 0x6;
        private const SlotType SlotShiftMask = ~0UL;
        private const SlotType SlotFlagValue = 0x1U;
        private const int SlotByteCountShiftIndex = 0x4;
#endif
        private byte _bitOffset;
        private const int ModulusSlotBitSize = (int) SlotBitSize - 1;
        private const uint SlotBitSize = (uint) SlotFlagValue << SlotShiftIndex;
        private const byte byteShiftIndex = 3;
        private const ulong bitCountPerByte = 1 << byteShiftIndex;
        private BitVector source;
        private bool writeable;
        private long position;
        public BitVectorStream(BitVector source, bool writeable)
        {
            this.source = source;
            this.writeable = writeable;
        }
        /// <summary>
        /// Returns whether the <see cref="BitVectorStream"/> can be read from.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Returns whether the <see cref="BitVectorStream"/> can have its position
        /// changed.
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        /// Returns whether the <see cref="BitVectorStream"/> can be written to.
        /// </summary>
        public override bool CanWrite
        {
            get { return this.writeable; }
        }

        public override void Flush()
        {
            return;
        }

        public override long Length
        {
            get { return (long) ((this.source.Length + bitCountPerByte - 1) >> byteShiftIndex); }
        }
        /// <summary>
        /// Returns/sets the <see cref="Int64"/> value which denotes
        /// the bit offset within the <see cref="BitVectorStream"/>.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public byte BitOffset
        {
            get
            {
                return this._bitOffset;
            }
            set
            {
                int modulus = value & ModulusSlotBitSize;
                if (modulus != value)
                    this.position += (value >> SlotByteCountShiftIndex);
                this._bitOffset = (byte) modulus;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.Position + (long) count > this.Length)
                throw new ArgumentOutOfRangeException("count");
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (offset < 0 || offset >= buffer.Length)
                throw new ArgumentOutOfRangeException("offset");
            for (int i = offset; i < offset + count; i++)
                buffer[i] = (byte) this.ReadByte();
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.position = offset;
                    break;
                case SeekOrigin.Current:
                    this.position += offset;
                    break;
                case SeekOrigin.End:
                    this.position = this.Length - (offset + 1);
                    break;
            }
            return (long) this.position;
        }

        public override void SetLength(long value)
        {
            this.source.Length = (((ulong) value) << byteShiftIndex);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (this.Position + (long) count > this.Length)
                throw new ArgumentOutOfRangeException("count");
            if (offset < 0 || offset >= buffer.Length)
                throw new ArgumentOutOfRangeException("offset");
            for (int i = offset; i < offset + count; i++)
                this.WriteByte(buffer[i]);
            this.position += count;
        }

        public override int ReadByte()
        {
            byte result = this.source.GetByte(this.BitAwarePosition);
            this.position++;
            return result;
        }

        private ulong BitAwarePosition
        {
            get
            {
                return ((ulong) this.position << byteShiftIndex) + this.BitOffset;
            }
        }

        public override void WriteByte(byte value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position++;
        }

        public void Write(Byte value)
        {
            this.WriteByte(value);
        }

        public void Write(Boolean value)
        {
        }

        public void Write(UInt16 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position += 2;
        }

        public void Write(UInt32 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position += 4;
        }

        public void Write(UInt64 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position += 8;
        }

        public void Write(SByte value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position++;
        }

        public void Write(Int16 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position += 2;
        }

        public void Write(Int32 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position += 4;
        }

        public void Write(Int64 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position += 8;
        }

        public void Write(String value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            var items = value.ToCharArray();
            this.WriteEncoded((uint)items.Length);
            for (int i = 0; i < items.Length; i++)
            {
                this.source.Set(this.BitAwarePosition, (ushort) items[i]);
                this.position += 2;
            }
            //this.source.Set(this.BitAwarePosition, value);
            //this.position += 2;
        }

        public string ReadString()
        {
            uint len = this.ReadEncodedUInt32();
            char[] result = new char[len];
            for (int i = 0; i < len; i++)
            {
                result[i] = (char)this.source.GetUInt16(this.BitAwarePosition);
                this.Position += 2;
            }
            return new string(result, 0, (int) len);
        }

        public void WriteEncoded(UInt16 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            const int mask = 0xFF00;
            if ((value & mask) == 0)
            {
                this.source[this.BitAwarePosition] = false;
                this.source.Set(BitAwarePosition + 1, (byte) value & 0xFF);
                this.BitOffset++;
                this.position++;
            }
            else
            {
                this.source[this.BitAwarePosition] = false;
                this.source.Set(BitAwarePosition + 1, value);
                this.BitOffset++;
                this.position += 2;
            }

        }

        public void WriteEncoded(UInt32 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            const uint mask1 = 0xFFFFFF00;
            const uint mask2 = 0xFFFF0000;
            const uint mask3 = 0xFF000000;
            if ((value & mask1) == 0)
            {
                this.source.Set(BitAwarePosition, 0, 1);
                this.BitOffset++;
                this.source.Set(BitAwarePosition, (byte) value & 0xFF);
                this.position++;
            }
            else if ((value & mask2) == 0)
            {
                this.source.Set(this.BitAwarePosition, (byte) 0x1, 2);
                this.BitOffset += 2;
                this.source.Set(BitAwarePosition, (ushort) (value & 0xFFFF));
                this.position += 2;
            }
            else if ((value & mask3) == 0)
            {
                this.source.Set(this.BitAwarePosition, (byte) 0x3, 3);
                this.BitOffset += 3;
                this.source.Set(BitAwarePosition, value, 24);
                this.position += 3;
            }
            else
            {
                this.source.Set(this.BitAwarePosition, (byte) 0x7, 3);
                this.BitOffset += 3;
                this.source.Set(BitAwarePosition, value);
                this.position += 4;
            }
        }

        public void WriteEncoded(UInt64 value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
            this.source.Set(this.BitAwarePosition, value);
            this.position += 8;
        }

        public void WriteEncoded(String value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
        }

        public void WriteBit(Boolean value)
        {
            if (!this.writeable)
                throw new InvalidOperationException();
        }

        public uint ReadEncodedUInt32()
        {
            uint result;
            byte mask = this.source.GetBits(this.BitAwarePosition, (byte)0x7);
            if ((mask & 0x1) == 0)
            {
                this.BitOffset += 1;
                result = this.source.GetByte(this.BitAwarePosition);
                this.Position++;
            }
            else if ((mask & 0x3) == 1)
            {
                this.BitOffset += 2;
                result = this.source.GetUInt16(this.BitAwarePosition);
                this.Position += 2;
            }
            else if ((mask & 0x7) == 0x3)
            {
                this.BitOffset += 3;
                result = this.source.GetUInt32(this.BitAwarePosition) & 0xFFFFFF;
                this.Position += 3;
            }
            else if ((mask & 0x7) == 0x7)
            {
                this.BitOffset += 3;
                result = this.source.GetUInt32(this.BitAwarePosition);
                this.Position += 4;
            }
            else
                throw new InvalidOperationException("Not an encoded value");
            return result;
        }

    }
}
