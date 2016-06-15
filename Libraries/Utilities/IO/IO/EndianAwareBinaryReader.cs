using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.IO
{
    public sealed class EndianAwareBinaryReader :
        BinaryReader
    {
        private Endianness targetEndianness;
        private bool bitEndianness;
        public EndianAwareBinaryReader(Stream input, Endianness targetEndianness, bool bitEndianness, bool leaveOpen = false)
            : base(input, Encoding.UTF8, leaveOpen)
        {
            this.targetEndianness = targetEndianness;
            this.bitEndianness = bitEndianness;
        }

        public Endianness TargetEndianess { get { return this.targetEndianness; } }
        public bool BitEndianness { get { return this.bitEndianness; } }

        public override byte ReadByte()
        {
            if (bitEndianness)
                return base.ReadByte().EndianChange(IOExtensions.SystemEndianness, targetEndianness);
            return base.ReadByte();
        }

        public override sbyte ReadSByte()
        {
            if (bitEndianness)
                return base.ReadSByte().EndianChange(IOExtensions.SystemEndianness, targetEndianness);
            return base.ReadSByte();
        }

        public override byte[] ReadBytes(int count)
        {
            if (bitEndianness)
            {
                byte[] result = base.ReadBytes(count);
                for (int i = 0; i < result.Length; i++)
                    result[i] = result[i].EndianChange(IOExtensions.SystemEndianness, targetEndianness);
                return result;
            }
            else
                return base.ReadBytes(count);
        }

        public override char ReadChar()
        {
            return (char)(((ushort) base.ReadChar()).EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness));
        }

        public override short ReadInt16()
        {
            return base.ReadInt16().EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
        }

        public override int ReadInt32()
        {
            return base.ReadInt32().EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
        }

        public override long ReadInt64()
        {
            return base.ReadInt64().EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
        }

        public override ushort ReadUInt16()
        {
            return base.ReadUInt16().EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
        }

        public override uint ReadUInt32()
        {
            return base.ReadUInt32().EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
        }

        public override ulong ReadUInt64()
        {
            return base.ReadUInt64().EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
        }

        public override double ReadDouble()
        {
            double result = base.ReadDouble();
            ulong resultD = BitConverter.ToUInt64(BitConverter.GetBytes(result), 0);
            resultD.EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
            return BitConverter.ToDouble(BitConverter.GetBytes(resultD), 0);
        }

        public override float ReadSingle()
        {
            var result = base.ReadSingle();
            uint resultD = BitConverter.ToUInt32(BitConverter.GetBytes(result), 0);
            resultD.EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
            return BitConverter.ToSingle(BitConverter.GetBytes(resultD), 0);
        }

        public override int Read(byte[] buffer, int index, int count)
        {
            int result = base.Read(buffer, index, count);
            if (bitEndianness)
                for (int i = index; i < index + count; i++)
                    buffer[i] = buffer[i].EndianChange(IOExtensions.SystemEndianness, targetEndianness);
            return result;
        }

        public override char[] ReadChars(int count)
        {
            var result = base.ReadChars(count);
            for (int i = 0; i < result.Length; i++)
                result[i] = (char) ((uint) result[i]).EndianChange(IOExtensions.SystemEndianness, targetEndianness, bitEndianness);
            return result;
        }


        internal int PeekByte()
        {
            long position = this.BaseStream.Position;
            if (position >= this.BaseStream.Length)
                return -1;
            int result = this.ReadByte();
            this.BaseStream.Position = position;
            return result;
        }

    }
}
