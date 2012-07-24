using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public struct CoffHeader
    {
        public const Endianness TargetEndianness = Endianness.LittleEndian;
        public const uint peSignature = 0x4550;
        private uint signature;
        private PEImageMachine machine;
        private ushort sectionCount;
        private uint secondsElapsedSinceEpoc;
        private RVAndSize symbolTableRVAndSize;
        private ushort optionalHeaderSize;
        private static readonly DateTime buildEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private CoffStandardCharacteristics characteristics;

        public PEImageMachine Machine
        {
            get
            {
                return this.machine;
            }
        }

        public ushort SectionCount
        {
            get
            {
                return this.sectionCount;
            }
        }

        public DateTime BuildStamp
        {
            get
            {
                var span = TimeSpan.FromSeconds(this.secondsElapsedSinceEpoc);
                return (buildEpoch + span).ToLocalTime();
            }
        }

        public RVAndSize SymbolTable
        {
            get
            {
                return this.symbolTableRVAndSize;
            }
        }

        public ushort OptionalHeaderSize
        {
            get
            {
                return this.optionalHeaderSize;
            }
        }

        public CoffStandardCharacteristics Characteristics
        {
            get
            {
                return this.characteristics;
            }
        }

        internal void Read(EndianAwareBinaryReader reader)
        {
            this.signature = reader.ReadUInt32();
            if (signature != peSignature)
                throw new BadImageFormatException();
            this.machine = (PEImageMachine) reader.ReadUInt16();
            this.sectionCount = reader.ReadUInt16();
            this.secondsElapsedSinceEpoc = reader.ReadUInt32();
            this.symbolTableRVAndSize.Read(reader);
            this.optionalHeaderSize = reader.ReadUInt16();
            this.characteristics = (CoffStandardCharacteristics) reader.ReadUInt16();
        }

        internal void Write(EndianAwareBinaryWriter writer)
        {
            writer.Write(this.signature);
            writer.Write((ushort)this.machine);
            writer.Write(this.sectionCount);
            writer.Write(this.secondsElapsedSinceEpoc);
            symbolTableRVAndSize.Write(writer);
            writer.Write(optionalHeaderSize);
            writer.Write((ushort)this.characteristics);
        }
    }

}
