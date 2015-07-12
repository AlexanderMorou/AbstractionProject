using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Numerics;

namespace AllenCopeland.Abstraction.IO
{
    public class EndianAwareBinaryWriter :
        BinaryWriter
    {
        private Endianness targetEndianness;
        private bool bitEndianness;

        public EndianAwareBinaryWriter(Stream stream, Endianness targetEndianness, bool bitEndianness = false)
            : base(stream)
        {
            this.targetEndianness = targetEndianness;
            this.bitEndianness = bitEndianness;
        }

        public override void Write(byte value)
        {
            if (bitEndianness && targetEndianness != NumericExtensions.SystemEndianness)
                base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness));
            else
                base.Write(value);
        }

        public override void Write(char ch)
        {
            base.Write((char)(((ushort)ch).EndianChange(NumericExtensions.SystemEndianness, bitEndianness)));
        }

        public override void Write(byte[] buffer)
        {
            if (targetEndianness != NumericExtensions.SystemEndianness)
                for (int i = 0; i < buffer.Length; i++)
                    buffer[i] = buffer[i].EndianChange(NumericExtensions.SystemEndianness, targetEndianness);
            else
                base.Write(buffer);
        }

        public override void Write(char[] chars)
        {
            if (targetEndianness != NumericExtensions.SystemEndianness)
                for (int i = 0; i < chars.Length; i++)
                    chars[i] = (char)(((ushort)chars[i]).EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness));
            else
                base.Write(chars);
        }

        public override void Write(double value)
        {
            ulong resultD = BitConverter.ToUInt64(BitConverter.GetBytes(value), 0);
            resultD.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness);
            base.Write(BitConverter.ToDouble(BitConverter.GetBytes(resultD), 0));
        }

        public override void Write(float value)
        {
            uint resultD = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            resultD.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness);
            base.Write(BitConverter.ToSingle(BitConverter.GetBytes(resultD), 0));
        }

        public override void Write(int value)
        {
            base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness));
        }

        public override void Write(long value)
        {
            base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness));
        }

        public override void Write(sbyte value)
        {
            base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness));
        }

        public override void Write(ushort value)
        {
            base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness));
        }

        public override void Write(uint value)
        {
            base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness));
        }

        public override void Write(ulong value)
        {
            base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness));
        }

        public override void Write(short value)
        {
            base.Write(value.EndianChange(NumericExtensions.SystemEndianness, targetEndianness, bitEndianness));
        }
    }
}
